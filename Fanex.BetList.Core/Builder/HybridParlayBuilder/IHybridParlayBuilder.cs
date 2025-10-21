namespace Fanex.BetList.Core.Builder.HybridParlayBuilder
{
    using Entities;
    using Fanex.BetList.Core.Templates.HybridParlay;
    using System.Collections.Generic;
    public interface IHybridParlayBuilder
    {
        string Build(
            ITicketHelper ticketHelper, 
            IList<ITicket> detailTickets, 
            string transDesc,
            int hdp1,
            int hdp2,
            string betCheck,
            string betTeam,
            string currency,
            decimal stake,
            bool isShowScoreMap);

        HybridParlay_Template BuildMatchTicket(
            ITicketHelper ticketHelper, 
            IList<ITicket> matchTickets, 
            LegOddsInfo mainLegOddsInfo, 
            string extraVoidTicketClass, 
            bool isShowScoreMap);
    }
}