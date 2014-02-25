// ---------------------------------------------------------------------------------
// <copyright file="CoreToVerifyHTML.cs" company="Nexcel Solutions Vietnam">
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
    using Fanex.BetList.Core.Builder;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Factories;
    using Fanex.BetList.Core.Tests.Integration.Interfaces;
    using Fanex.BetList.Core.TicketHelper;
    using Fanex.BetList.CoreToVerify.Wrap.DTO;
    using Newtonsoft.Json;

    /// <summary>
    /// Class Core-ToVerify HTML.
    /// </summary>
    public class CoreToVerifyHTML : ICoreHTMLBuilder
    {
        private BetListHTMLBuilder _builder;
        private ITicketHelper _ticketHelper;
        private List<ITicket> _tickets;
        private List<ITicketData> _ticketData;
        private GetCachePropertyById _funcGetCachePropById;

        public CoreToVerifyHTML(DataSet datasource)
        {
            _tickets = this.BindDataToTicketList(datasource.Tables[0]);
            _ticketData = this.BindDataToTicketDataList(datasource.Tables[1]);

            var factory = new BetListHTMLFactory();
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

        public string GetTicketDataJson()
        {
            string json = JsonConvert.SerializeObject(_ticketData);
            return json;
        }        

        public string BuildChoice(int dataIndex, bool isShowScoreMap)
        {
            ITicket ticket = _tickets[dataIndex];
            string result = _builder.BuildChoice(ticket, _ticketHelper, _ticketData, isShowScoreMap);
            return result;
        }

        public string BuildOdds(int dataIndex)
        {
            ITicket ticket = _tickets[dataIndex];
            GetCachePropertyById funcGetCachePropById = CacheRef.GetOddsTypeName;
            string result = _builder.BuildOdds(ticket, _ticketData, funcGetCachePropById);
            return result;
        }

        public string BuildStake(int dataIndex)
        {
            ITicket ticket = _tickets[dataIndex];
            string result = _builder.BuildStake(ticket);
            return result;
        }

        public string BuildStatus(int dataIndex, bool isShowVNIP)
        {
            ITicket ticket = _tickets[dataIndex];
            string result = _builder.BuildStatus(ticket, _ticketHelper, _ticketData, isShowVNIP);
            return result;
        }

        public string BuildTrans(int dataIndex)
        {
            ITicket ticket = _tickets[dataIndex];
            string result = _builder.BuildTrans(ticket);
            return result;
        }

        public string BuildSystemParlayDetail()
        {
            return string.Empty;
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
