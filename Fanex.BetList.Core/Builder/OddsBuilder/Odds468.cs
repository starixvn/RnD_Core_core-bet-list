namespace Fanex.BetList.Core.Builder.OddsBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;
    using Fanex.BetList.Core.Utils;
    using Fanex.BetList.Core.Utils.TicketExt;

    /// <summary>
    /// PHAIO 4A: Score Box Handicap
    /// </summary>
    public class Odds468 : Odds1
    {
        public override Odds_Template Render(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName)
        {
            var odds = ticket.IsSettled() ? ticket.Odds : 0;

            Template.odds = ConvertByBetType.Odds(odds, ticket.BetTypeId, ticket.OddsType);
            Template.oddsType = string.Empty;

            return Template;
        }
    }
}