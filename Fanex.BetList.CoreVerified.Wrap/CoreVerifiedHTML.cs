// ---------------------------------------------------------------------------------
// <copyright file="CoreVerifiedHTML.cs" company="Nexcel Solutions Vietnam">
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
    using Fanex.BetList.Core.Builder;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Factories;
    using Fanex.BetList.Core.Tests.Integration.Interfaces;
    using Fanex.BetList.Core.TicketHelper;
    using Fanex.BetList.CoreVerified.Wrap.DTO;
    using Newtonsoft.Json;

    /// <summary>
    /// Class Core Verified HTML.
    /// </summary>
    public class CoreVerifiedHTML : ICoreHTMLBuilder
    {
        private static readonly List<int> WinplaceBetTypesId = new List<int> { 33, 1233 };
        private BetListHTMLBuilder _builder;
        private ITicketHelper _ticketHelper;
        private List<ITicket> _tickets;
        private List<ITicketData> _ticketData;
        private GetCachePropertyById _funcGetCachePropById;

        public CoreVerifiedHTML(DataSet datasource)
        {
            _tickets = this.BindDataToTicketList(datasource.Tables[0]);
            _ticketData = this.BindDataToTicketDataList(datasource.Tables[1]);

            var factory = new BetListHTMLFactory();
            _builder = factory.CreateBuilder();
            _ticketHelper = new TicketHelper();
            _funcGetCachePropById = CacheRef.GetOddsTypeName;
        }

        public CoreVerifiedHTML(DataTable datasource)
        {
            _tickets = this.BindDataToTicketList(datasource);
            _ticketData = null;

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
            List<ITicketData> ticketData = _ticketData ?? CreateWinPlaceTicketData(ticket);

            string result = _builder.BuildChoice(ticket, _ticketHelper, ticketData, isShowScoreMap);
            return result;
        }

        public string BuildOdds(int dataIndex)
        {
            ITicket ticket = _tickets[dataIndex];
            GetCachePropertyById funcGetCachePropById = CacheRef.GetOddsTypeName;            
            List<ITicketData> ticketData = _ticketData ?? CreateWinPlaceTicketData(ticket);

            string result = _builder.BuildOdds(ticket, ticketData, funcGetCachePropById);
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
            List<ITicketData> ticketData = _ticketData ?? CreateWinPlaceTicketData(ticket);

            string result = _builder.BuildStatus(ticket, _ticketHelper, ticketData, isShowVNIP);
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
