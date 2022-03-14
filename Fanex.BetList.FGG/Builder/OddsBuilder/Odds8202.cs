namespace Fanex.BetList.FGG.Builder.OddsBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Builder.OddsBuilder;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;

    public class Odd8202 : Odds1
    {
        public override Odds_Template Render(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName)
        {
            Template.odds = "-";
            Template.oddsType = string.Empty;

            return Template;
        }
    }
}
