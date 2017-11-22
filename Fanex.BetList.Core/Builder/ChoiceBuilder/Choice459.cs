namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Entities;

    /// <summary>
    /// PH AIO Phase 3 - 1H Asian 1X2.
    /// </summary>
    public class Choice459 : Choice458
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.betTeam = $"{CoreBetList.HtAsian}{CoreBetList.Seperator1X2}{ticket.BetTeam?.ToUpperInvariant()}";
        }
    }
}