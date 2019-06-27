namespace Fanex.BetList.Core.Builder.OddsBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;
    using Fanex.BetList.Core.Utils.TicketExt;

    /// <summary>
    /// Half Time/Full Time Exact Total Goals
    /// </summary>
    public class Odds468 : Odds1
    {
        public override Odds_Template Render(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName)
        {
            if (ticket.IsSettled())
            {
                base.Render(ticket, ticketData, funcGetOddsTypeName);
            }
            else
            {
                Template.odds = "-";
                Template.oddsType = string.Empty;
            }

            return Template;
        }
    }
}