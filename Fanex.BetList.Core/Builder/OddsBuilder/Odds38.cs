namespace Fanex.BetList.Core.Builder.OddsBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;

    /// <summary>
    /// Single Match Parlay
    /// </summary>
    public class Odds38 : Odds1
    {
        public override Odds_Template Render(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName)
        {
            var oddsTemplate =  base.Render(ticket, ticketData, funcGetOddsTypeName);
            oddsTemplate.oddsType = string.Empty;

            return oddsTemplate;
        }
    }
}