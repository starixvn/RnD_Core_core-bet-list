namespace Fanex.BetList.Core.Builder.OddsBuilder
{
    using System.Collections.Generic;
    using Entities;
    using BetListNPOI.SS.UserModel;
    using Templates;
    using Utils;

    public class Odds1 : IOdds
    {
        private const string OddsValueOpenTag = "<div class=\"underdog\">";
        private const string OddsTypeOpenTag = "<div class=\"oddstype\">";
        private const string CloseTag = "</div>";

        public Odds1()
        {
            Template = new Odds_Template();
        }

        public Odds_Template Template { get; set; }

        public virtual Odds_Template Render(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName)
        {
            object oddsType = string.IsNullOrEmpty(ticket.OddsType) ? (object)0 : ticket.OddsType;

            Template.odds = ConvertByBetType.Odds(ticket.Odds, ticket.BetTypeId, ticket.SportTypeId, oddsType);
            Template.oddsType = funcGetOddsTypeName(ConvertByBetType.OddsType(ticket.BetTypeId, ticket.SportTypeId, oddsType));

            return Template;
        }

        public virtual string RenderOddsValue(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName)
        {
            var template = Render(ticket, ticketData, funcGetOddsTypeName);

            return $"{OddsValueOpenTag}{template.odds}{CloseTag}";
        }

        public virtual string RenderOddsType(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName)
        {
            var template = Render(ticket, ticketData, funcGetOddsTypeName);
            var oddsTypeName = template.oddsType;

            if (template.odds != "-" && string.IsNullOrEmpty(oddsTypeName))
            {
                var oddsType = string.IsNullOrEmpty(ticket.OddsType) ? "0" : ticket.OddsType;
                oddsTypeName = funcGetOddsTypeName(oddsType);
            }

            return $"{OddsTypeOpenTag}{oddsTypeName}{CloseTag}";
        }

        public virtual IRichTextString RenderRTF(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName, RTFHelper rtfHelper)
        {
            Render(ticket, ticketData, funcGetOddsTypeName);

            return BuildRTFOdds(rtfHelper);
        }

        public virtual IRichTextString RenderOddsValueRTF(
            ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName, RTFHelper rtfHelper)
        {
            var oddsValue = RenderOddsValue(ticket, ticketData, funcGetOddsTypeName)
                .Replace(OddsValueOpenTag, string.Empty)
                .Replace(CloseTag, string.Empty);

            var rtfNumberRenderer = new RTFNumber(rtfHelper.RTFRenderer, rtfHelper.PosFont, rtfHelper.NegFont);
            rtfNumberRenderer.Render(oddsValue);

            var oddsValueExcel = rtfHelper.RTFRenderer.Render();
            rtfHelper.RTFRenderer.Clear();

            return oddsValueExcel;
        }

        public virtual IRichTextString RenderOddsTypeRTF(
            ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName, RTFHelper rtfHelper)
        {
            var oddsType = RenderOddsType(ticket, ticketData, funcGetOddsTypeName)
                .Replace(OddsTypeOpenTag, string.Empty)
                .Replace(CloseTag, string.Empty);

            rtfHelper.RTFRenderer.AddText(oddsType, rtfHelper.PosFont);

            var oddsTypeExcel = rtfHelper.RTFRenderer.Render();
            rtfHelper.RTFRenderer.Clear();

            return oddsTypeExcel;
        }

        protected virtual IRichTextString BuildRTFOdds(RTFHelper rtfHelper)
        {
            string odds = Template.odds;
            var rtfNumberRenderer = new RTFNumber(rtfHelper.RTFRenderer, rtfHelper.PosFont, rtfHelper.NegFont);
            rtfNumberRenderer.Render(odds);

            if (!string.IsNullOrEmpty(Template.oddsType))
            {
                odds = "\n" + Template.oddsType;
                rtfHelper.RTFRenderer.AddText(odds, rtfHelper.PosFont);
            }

            var rtfOdds = rtfHelper.RTFRenderer.Render();
            rtfHelper.RTFRenderer.Clear();
            return rtfOdds;
        }
    }
}