namespace Fanex.BetList.Core.Builder.OddsBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;
    using Fanex.BetList.Core.Utils;

    /// <summary>
    /// PHAIO 5.1: Penalty Shootout Commbo (First 10)
    /// </summary>
    public class Odds376 : Odds1
    {
        public override Odds_Template Render(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName)
        {
            object oddsType = string.IsNullOrEmpty(ticket.OddsType) ? (object)0 : ticket.OddsType;

            Template.odds = ConvertByBetType.Odds4(ticket.Odds, ticket.BetTypeId, oddsType);
            Template.oddsType = funcGetOddsTypeName(ConvertByBetType.OddsType(ticket.BetTypeId, oddsType));

            return Template;
        }
    }
}