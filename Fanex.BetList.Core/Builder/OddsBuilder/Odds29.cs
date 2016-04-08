namespace Fanex.BetList.Core.Builder.OddsBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;

    /// <summary>
    /// Combination Mix Parlay.
    /// </summary>
    public class Odds29 : Odds1
    {
        private const string DashOdds = "-";

        public override Odds_Template Render(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName)
        {
            Template.odds = DashOdds;
            Template.oddsType = string.Empty;

            return Template;
        }
    }
}