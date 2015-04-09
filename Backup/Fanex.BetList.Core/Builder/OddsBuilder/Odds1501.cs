namespace Fanex.BetList.Core.Builder.OddsBuilder
{
    using System.Collections.Generic;
    using Entities;
    using NPOI.SS.UserModel;
    using Templates;
    using Utils;

    /// <summary>
    /// Keno games.
    /// </summary>
    public class Odds1501 : Odds1
    {
        public override Odds_Template Render(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName)
        {
            object oddsType = string.IsNullOrEmpty(ticket.OddsType) ? (object)0 : ticket.OddsType;

            Template.odds = ConvertByBetType.Odds(ticket.Odds, ticket.BetTypeId, oddsType);
            Template.oddsType = string.Empty;

            return Template;
        }

        protected override IRichTextString BuildRTFOdds(RTFHelper rtfHelper)
        {
            string odds = Template.odds;
            var rtfNumberRenderer = new RTFNumber(rtfHelper.RTFRenderer, rtfHelper.PosFont, rtfHelper.NegFont);

            rtfNumberRenderer.Render(odds);

            var rtfOdds = rtfHelper.RTFRenderer.Render();
            rtfHelper.RTFRenderer.Clear();
            return rtfOdds;
        }
    }
}