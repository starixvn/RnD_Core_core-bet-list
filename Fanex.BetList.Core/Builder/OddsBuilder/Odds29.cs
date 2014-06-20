namespace Fanex.BetList.Core.Builder.OddsBuilder
{
    using System.Collections.Generic;
    using Entities;
    using Templates;
    using Utils;

    /// <summary>
    /// Combination Mix Parlay.
    /// </summary>
    public class Odds29 : Odds1
    {
        public override Odds_Template Render(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName)
        {
            var status = ticket.Status.ToLower();
            Template.odds = status.Equals(BetStatus.Running) ? "-" : ConvertByBetType.Odds(ticket.MasterOdds);
            Template.oddsType = funcGetOddsTypeName(ConvertByBetType.OddsType(ticket.BetTypeId, ticket.OddsType));

            return Template;
        }
    }
}