namespace Fanex.BetList.Core.Builder.OddsBuilder
{
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;

    public class Odds4601 : Odds1
    {
        public override Odds_Template Render(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName)
        {
            var noOdds = new List<int>
            {
                VirtualGamesBetTypes.SoccerTowers,
                VirtualGamesBetTypes.BasketballTowers,
                VirtualGamesBetTypes.CricketTowers
            };

            if (ticket.Status.ToLowerInvariant().Equals(BetStatus.Draw) ||
                noOdds.Any(i => i == ticket.BetTypeId))
            {
                Template.odds = "-";
                Template.oddsType = string.Empty;

                return Template;
            }

            return base.Render(ticket, ticketData, funcGetOddsTypeName);
        }
    }
}
