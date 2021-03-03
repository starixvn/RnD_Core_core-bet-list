namespace Fanex.BetList.Core.Builder.OddsBuilder
{
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;
    using Fanex.BetList.Core.Utils;
    using System;
    using System.Collections.Generic;

    public class Odds501 : Odds1
    {
        private static readonly DateTime CutOffTime = new DateTime(2021, 4, 18, 0, 0, 0);

        public override Odds_Template Render(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName)
        {
            if (CutOffTime > ticket.TransDate)
            {
                return base.Render(ticket, ticketData, funcGetOddsTypeName);
            }

            Template.odds = ConvertByBetType.Odds(ticket.Odds, ticket.BetTypeId, ticket.OddsType);
            Template.oddsType = string.Empty;

            return Template;
        }
    }
}