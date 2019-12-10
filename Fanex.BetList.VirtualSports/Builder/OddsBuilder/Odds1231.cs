namespace Fanex.BetList.Core.Builder.OddsBuilder
{
    using System.Collections.Generic;
    using Entities;
    using Templates;
    using Utils;

    /// <summary>
    /// Virtual Horse Racing - Win.
    /// </summary>
    public class Odds1231 : Odds1
    {
        public override Odds_Template Render(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName)
        {
            object oddsType = string.IsNullOrEmpty(ticket.OddsType) ? (object)0 : ticket.OddsType;

            Template.odds = ConvertByBetType.VirtualHorseRacingOdds(ticket.Odds, ticket.BetTypeId, oddsType);
            Template.oddsType = string.Empty;

            return Template;
        }
    }
}