// ---------------------------------------------------------------------------------
// <copyright file="CoreToVerifyExcel.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------------
// <history>
//     <change who="Marc.Bui" date="2014.02.25">Create</change>
// </history>
// ---------------------------------------------------------------------------------

namespace Fanex.BetList.CoreToVerify.Wrap
{
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using Fanex.BetList.Core.Builder;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Factories;
    using Fanex.BetList.Core.TicketHelper;
    using Fanex.BetList.CoreToVerify.Wrap.DTO;
    using Newtonsoft.Json;
    using NPOI.SS.UserModel;

    /// <summary>
    /// Class CoreToVerifyExcel.
    /// </summary>
    public class CoreToVerifyExcel
    {
        private static readonly List<int> WinplaceBetTypesId = new List<int> { 33, 1233 };
        private BetListExcelBuilder _builder;
        private ITicketHelper _ticketHelper;
        private List<ITicket> _tickets;
        private List<ITicketData> _ticketData;
        private GetCachePropertyById _funcGetCachePropById;

        public CoreToVerifyExcel(DataSet datasource)
        {
            _tickets = this.BindDataToTicketList(datasource.Tables[0]);
            _ticketData = this.BindDataToTicketDataList(datasource.Tables[1]);

            var factory = new BetListExcelFactory();
            _builder = factory.CreateBuilder();
            _ticketHelper = new TicketHelper();
            _funcGetCachePropById = CacheRef.GetOddsTypeName;
        }

        public CoreToVerifyExcel(DataTable datasource)
        {
            _tickets = this.BindDataToTicketList(datasource);
            _ticketData = null;

            var factory = new BetListExcelFactory();
            _builder = factory.CreateBuilder();
            _ticketHelper = new TicketHelper();
            _funcGetCachePropById = CacheRef.GetOddsTypeName;
        }

        public int TicketCount
        {
            get
            {
                return _tickets.Count;
            }
        }

        public string GetTicketJsonByIndex(int index)
        {
            ITicket ticket = _tickets[index];
            string json = JsonConvert.SerializeObject(ticket);
            return json;
        }

        public void InitializeWorksheet(short numberOfColumns)
        {
            _builder.BuildWorksheet("BetList", numberOfColumns);
        }

        /// <summary>
        /// Finalizes the build process and save.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        public void FinalizeAndSave(string filePath)
        {
            using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    IWorkbook resultWorkbook = _builder.GetBetList();
                    resultWorkbook.Write(ms);
                    fs.Write(ms.GetBuffer(), 0, (int)ms.Length);
                }
            }            
        }

        public IRow GetLastRow()
        {
            return _builder.GetBetList().GetSheet("BetList").GetRow(_builder.LastContentRow);
        }

        public CoreToVerifyExcel AddChoice(int dataIndex, bool isShowScoreMap)
        {
            ITicket ticket = _tickets[dataIndex];
            List<ITicketData> ticketData = _ticketData ?? CreateWinPlaceTicketData(ticket);

            _builder.AddChoice(ticket, _ticketHelper, ticketData);
            return this;
        }

        public CoreToVerifyExcel AddOdds(int dataIndex)
        {
            ITicket ticket = _tickets[dataIndex];
            GetCachePropertyById funcGetCachePropById = CacheRef.GetOddsTypeName;
            List<ITicketData> ticketData = _ticketData ?? CreateWinPlaceTicketData(ticket);

            _builder.AddOdds(ticket, ticketData, funcGetCachePropById);
            return this;
        }

        public CoreToVerifyExcel AddStake(int dataIndex)
        {
            ITicket ticket = _tickets[dataIndex];
            _builder.AddStake(ticket);
            return this;
        }

        public CoreToVerifyExcel AddStatus(int dataIndex, bool isShowVNIP)
        {
            ITicket ticket = _tickets[dataIndex];
            List<ITicketData> ticketData = _ticketData ?? CreateWinPlaceTicketData(ticket);

            _builder.AddStatus(ticket, _ticketHelper, ticketData, isShowVNIP);
            return this;
        }

        public CoreToVerifyExcel AddTrans(int dataIndex)
        {
            ITicket ticket = _tickets[dataIndex];
            _builder.AddTrans(ticket, _ticketHelper);
            return this;
        }
        
        private List<ITicket> BindDataToTicketList(DataTable data)
        {
            return TicketTransformer.Transform(data);
        }

        private List<ITicketData> BindDataToTicketDataList(DataTable data)
        {
            return TicketDataTransformer.Transform(data);
        }

        /// <summary>
        /// Creates the ticket data for Win/Place bet-types.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <returns>Return tickets.</returns>
        private List<ITicketData> CreateWinPlaceTicketData(ITicket ticket)
        {
            if (WinplaceBetTypesId.Contains(ticket.BetTypeId))
            {
                return new List<ITicketData> 
                { 
                    new TicketData
                    {
                        // Win
                        RefNo = ticket.RefNo,
                        Status = ticket.Status,
                        BetTeam = ticket.BetTeam,
                        BetTypeId = ticket.BetTypeId == 33 ? 31 : 1231,
                        Odds = ticket.Handicap1                           
                    },
                    new TicketData
                    {
                        // Place
                        RefNo = ticket.RefNo,   
                        Status = ticket.Status,
                        BetTypeId = ticket.BetTypeId == 33 ? 32 : 1232,
                        Odds = ticket.Handicap2
                    }
                };
            }

            return null;
        }
    }
}
