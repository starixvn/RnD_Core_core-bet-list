namespace Fanex.BetList.Core.Builder.OddsBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;

    public class OddsLC : Odds1
    {
        public override Odds_Template Render(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName)
        {
            if (IsHiddenOdds(ticket))
            {
                Template.odds = "-";
                Template.oddsType = string.Empty;

                return Template;
            }

            return base.Render(ticket, ticketData, funcGetOddsTypeName);
        }

        public virtual bool IsHiddenOdds(ITicket ticket)
        {
            return string.IsNullOrEmpty(ticket.TransDesc) || ticket.Odds == 0;
        }
    }
}