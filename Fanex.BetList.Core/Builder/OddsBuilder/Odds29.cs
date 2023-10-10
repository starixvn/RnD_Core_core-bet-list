namespace Fanex.BetList.Core.Builder.OddsBuilder
{
    using BetListNPOI.SS.UserModel;
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;
    using Fanex.BetList.Core.Utils;
    using Fanex.BetList.Core.Utils.StringExt;
    using Fanex.BetList.Core.Utils.TicketExt;

    public class Odds29 : Odds1
    {
        private const string OddsValueOpenTag = "<div class=\"underdog{oddsClassName}\">";
        protected const string StrikeThrough = " strikethrough";
        private const int OddsSpreadForBoots = 2;

        private const string SportsbookTag = "p1";
        private const string BoostRateAttribute = "bbp";
        private const string BoostOddsAttribute = "bba";


        public override Odds_Template Render(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName)
        {
            if (IsMixParlay(ticket))
            {
                base.Render(ticket, ticketData, funcGetOddsTypeName);
                var oddsType = string.IsNullOrEmpty(ticket.OddsType) ? (object)0 : ticket.OddsType;
                Template.odds = ConvertByBetType.Odds4(ticket.Odds, ticket.BetTypeId, ticket.SportTypeId, oddsType);
                if (IsParlayBoost(ticket))
                {
                    var rateText = GetBoostRate(ticket);
                    var boostOdds = GetBoostOdds(ticket);

                    Template.oddsClassName = StrikeThrough;
                    Template.oddsType = $"<span class='underdog'>{boostOdds}</span><br/>";
                    Template.oddsType += $"<span class='boostrate'><span class='icon-boost'></span>{rateText}</span>";
                }
            }
            else
            {
                Template.odds = "-";
                Template.oddsType = string.Empty;
            }

            return Template;
        }

        public override string RenderOddsValue(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName)
        {
            var template = Render(ticket, ticketData, funcGetOddsTypeName);
            var oddsValueOpenTag = IsParlayBoost(ticket) ?
            OddsValueOpenTag.Replace("{oddsClassName}", StrikeThrough) :
            OddsValueOpenTag.Replace("{oddsClassName}", string.Empty);

            return $"{oddsValueOpenTag}{template.odds}{CloseTag}";
        }

        protected override IRichTextString BuildRTFOdds(RTFHelper rtfHelper)
        {
            if (IsParlayBoost(Template.oddsClassName))
            {
                rtfHelper.RTFRenderer.AddText(Template.odds, rtfHelper.PosFontCrossed);
                rtfHelper.RTFRenderer.AddText("\n" + Template.oddsType.Replace("<br/>", "\n").StripHTML(), rtfHelper.PosFont);
            }
            else
            {
                rtfHelper.RTFRenderer.AddText(Template.odds, rtfHelper.PosFont);
            }

            var rtfOdds = rtfHelper.RTFRenderer.Render();
            rtfHelper.RTFRenderer.Clear();
            return rtfOdds;

            bool IsParlayBoost(string cssClassName)
                => cssClassName == StrikeThrough;
        }

        public override IRichTextString RenderOddsTypeRTF(
            ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName, RTFHelper rtfHelper)
        {
            if (IsParlayBoost(ticket))
            {
                rtfHelper.RTFRenderer.AddText("\n" + GetBoostOdds(ticket), rtfHelper.PosFont);
                rtfHelper.RTFRenderer.AddText("\n" + GetBoostRate(ticket), rtfHelper.PosFont);
                var oddsTypeExcel = rtfHelper.RTFRenderer.Render();
                rtfHelper.RTFRenderer.Clear();

                return oddsTypeExcel;
            }

            return base.RenderOddsTypeRTF(ticket, ticketData, funcGetOddsTypeName, rtfHelper);
        }

        public override IRichTextString RenderOddsValueRTF(
            ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName, RTFHelper rtfHelper)
        {
            if (IsParlayBoost(ticket))
            {
                var template = Render(ticket, ticketData, funcGetOddsTypeName);
                rtfHelper.RTFRenderer.AddText(template.odds, rtfHelper.PosFontCrossed);
                var oddsTypeExcel = rtfHelper.RTFRenderer.Render();
                rtfHelper.RTFRenderer.Clear();

                return oddsTypeExcel;
            }

            return base.RenderOddsValueRTF(ticket, ticketData, funcGetOddsTypeName, rtfHelper);
        }

        private string GetBoostOdds(ITicket ticket)
        => string.Format(Formatter.DECIMALFORMAT4, ticket.MasterOdds);

        private string GetBoostRate(ITicket ticket)
        {
            var rate = GetRate(ticket);

            return ConvertByBetType.FormatBoostRate(rate);
        }

        decimal GetRate(ITicket ticket)
        {
            if (ticket.IsRunningWaiting())
            {
                var rate = ticket.ExtraInfo.HasAttributeInXmlTag(SportsbookTag, BoostRateAttribute) ?
                ticket.ExtraInfo.GetXmlTagAttributeValue(SportsbookTag, BoostRateAttribute) :
                "0";

                return Cast.AsDecimal(rate);
            }

            return Cast.AsDecimal(ticket.TransDesc);
        }

        bool IsParlayBoost(ITicket ticket)
        {
            if (IsMixParlay(ticket) && ticket.OddsSpread == OddsSpreadForBoots)
            {
                var bba = ticket.ExtraInfo.HasAttributeInXmlTag(SportsbookTag, BoostOddsAttribute) ?
                ticket.ExtraInfo.GetXmlTagAttributeValue(SportsbookTag, BoostOddsAttribute) :
                "0";

                return ticket.IsRunningWaiting() || (ticket.IsWon() && Cast.AsDecimal(bba) > 0);
            }

            return false;
        }

        bool IsMixParlay(ITicket ticket) => ticket.BetTeam == "1";
    }
}