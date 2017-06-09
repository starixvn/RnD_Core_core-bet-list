namespace Fanex.BetList.Core.Builder.OddsBuilder
{
    using System.Collections.Generic;
    using Entities;
    using NPOI.SS.UserModel;
    using Templates;

    public class Odds911 : Odds1
    {
        public override Odds_Template Render(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName)
        {
            var betTypeId = ticket.BetTypeId;
            ticket.BetTypeId = ticket.OriginalBetType;

            var odds = CreateOdds(ticket.OriginalBetType);
            var template = odds.Render(ticket, ticketData, funcGetOddsTypeName);

            ticket.BetTypeId = betTypeId;

            return template;
        }

        public override IRichTextString RenderRTF(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName, RTFHelper rtfHelper)
        {
            var betTypeId = ticket.BetTypeId;
            ticket.BetTypeId = ticket.OriginalBetType;

            var odds = CreateOdds(ticket.OriginalBetType);
            var template = odds.RenderRTF(ticket, ticketData, funcGetOddsTypeName, rtfHelper);

            ticket.BetTypeId = betTypeId;

            return template;
        }

        private static IOdds CreateOdds(int betTypeId)
        {
            var betListHtmlBuilder = new BetListHTMLBuilder();

            return betListHtmlBuilder.CreateOddsBuilder(betTypeId);
        }
    }
}