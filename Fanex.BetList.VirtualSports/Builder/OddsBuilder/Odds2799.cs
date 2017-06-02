namespace Fanex.BetList.Core.Builder.OddsBuilder
{
    using System.Collections.Generic;
    using Entities;
    using Templates;

    public class Odds2799 : Odds1
    {
        public override Odds_Template Render(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName)
        {
            Template.odds = "-";
            Template.oddsType = string.Empty;

            return Template;
        }
    }
}