// ---------------------------------------------------------------------------------
// <copyright file="CoreVerifiedExcel.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------------
// <history>
//     <change who="Marc.Bui" date="2014.02.24">Create</change>
// </history>
// ---------------------------------------------------------------------------------

namespace Fanex.BetList.CoreVerified.Wrap
{
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using Fanex.BetList.Core.Builder;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Factories;
    using Fanex.BetList.Core.TicketHelper;
    using Fanex.BetList.CoreVerified.Wrap.DTO;
    using NPOI.SS.UserModel;

    /// <summary>
    /// Wrapper class of Excel export APIs from verified Core Bet List.
    /// </summary>
    public class CoreVerifiedExcel
    {
        private BetListExcelBuilder _builder;
        private ITicketHelper _ticketHelper;
        private List<ITicket> _tickets;
        private List<ITicketData> _ticketData;
        private GetCachePropertyById _funcGetCachePropById;

        public CoreVerifiedExcel(DataSet datasource)
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

        public void InitializeWorksheet(short numberOfColumns)
        {
            _builder.BuildWorksheet("BetList", numberOfColumns);
        }

        /// <summary>
        /// Save excel file to disk.
        /// </summary>
        /// <param name="filePath">Physical path where the in-memory excel file will reside.</param>
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

        public CoreVerifiedExcel AddChoice(int dataIndex, bool isShowScoreMap)
        {
            ITicket ticket = _tickets[dataIndex];
            _builder.AddChoice(ticket, _ticketHelper, _ticketData);
            return this;
        }

        public CoreVerifiedExcel AddOdds(int dataIndex)
        {
            ITicket ticket = _tickets[dataIndex];
            GetCachePropertyById funcGetCachePropById = CacheRef.GetOddsTypeName;
            _builder.AddOdds(ticket, _ticketData, funcGetCachePropById);
            return this;
        }

        public CoreVerifiedExcel AddStake(int dataIndex)
        {
            ITicket ticket = _tickets[dataIndex];
            _builder.AddStake(ticket);
            return this;
        }

        public CoreVerifiedExcel AddStatus(int dataIndex, bool isShowVNIP)
        {
            ITicket ticket = _tickets[dataIndex];
            _builder.AddStatus(ticket, _ticketHelper, _ticketData, isShowVNIP);
            return this;
        }

        public CoreVerifiedExcel AddTrans(int dataIndex)
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
