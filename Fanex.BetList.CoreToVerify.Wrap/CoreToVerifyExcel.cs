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
            _builder.AddChoice(ticket, _ticketHelper, _ticketData);
            return this;
        }

        public CoreToVerifyExcel AddOdds(int dataIndex)
        {
            ITicket ticket = _tickets[dataIndex];
            GetCachePropertyById funcGetCachePropById = CacheRef.GetOddsTypeName;
            _builder.AddOdds(ticket, _ticketData, funcGetCachePropById);
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
            _builder.AddStatus(ticket, _ticketHelper, _ticketData, isShowVNIP);
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
    }
}
