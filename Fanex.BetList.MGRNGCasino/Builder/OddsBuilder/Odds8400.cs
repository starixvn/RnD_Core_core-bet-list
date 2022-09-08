namespace Fanex.BetList.Core.Builder.OddsBuilder
{
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;
    using System.Collections.Generic;

    public class Odds8400 : Odds1
    {
        public override Odds_Template Render(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName)
        {
            Template.odds = "-";
            Template.oddsType = string.Empty;

            return Template;
        }
    }
}
