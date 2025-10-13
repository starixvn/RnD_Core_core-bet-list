namespace Fanex.BetList.Core.Builder.OddsBuilder
{
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;
    using System.Collections.Generic;

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
                noOdds.Exists(i => i == ticket.BetTypeId))
            {
                Template.odds = "-";
                Template.oddsType = string.Empty;

                return Template;
            }

            return base.Render(ticket, ticketData, funcGetOddsTypeName);
        }
    }
}
