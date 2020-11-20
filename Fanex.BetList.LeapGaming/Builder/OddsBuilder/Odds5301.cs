
namespace Fanex.BetList.Core.Builder.OddsBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;

    public class Odds5301 : Odds1
    {
        public override Odds_Template Render(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName)
        {
            if (Choice5301.IsSingleBet(ticket))
            {
                return base.Render(ticket, ticketData, funcGetOddsTypeName);
            }

            Template.odds = "-";
            Template.oddsType = string.Empty;

            return Template;
        }
    }
}
