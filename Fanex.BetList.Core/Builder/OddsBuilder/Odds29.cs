namespace Fanex.BetList.Core.Builder.OddsBuilder
{
    using BetListNPOI.SS.UserModel;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;
    using Fanex.BetList.Core.Utils;
    using Fanex.BetList.Core.Utils.StringExt;
    using Fanex.BetList.Core.Utils.TicketExt;
    using Org.BouncyCastle.Tls.Crypto.Impl.BC;
    using System.Collections.Generic;

    public class Odds29 : Odds1
    {
        private const string OddsValueOpenTag = "<div class=\"underdog{oddsClassName}\">";
        protected const string StrikeThrough = " strikethrough";
        protected const string ChangeOdds = " strikethrough change-odds";
        private const int OddsSpreadForBoots = 2;
        private const int OddsSpreadForBetBuilderPlus = 3;
        private const string SportsbookTag = "p1";
        private const string BoostRateAttribute = "bbp";
        private const string BoostOddsAttribute = "bba";
        private const string OddsBetBuilderPlusAttribute = "o";


        public override Odds_Template Render(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName)
        {
            if(IsBetBuilderPlus(ticket))
            {
                base.Render(ticket, ticketData, funcGetOddsTypeName);
                var oddsType = string.IsNullOrEmpty(ticket.OddsType) ? (object)0 : ticket.OddsType;
                Template.odds = ConvertByBetType.Odds4(GetOddsBetBuilderPlus(ticket), ticket.BetTypeId, ticket.SportTypeId, oddsType);
                Template.oddsType = string.Empty;
                if (IsChangedOdds(ticket) && ticket.MasterOdds != 0)
                {
                    Template.oddsClassName = ChangeOdds;
                    Template.oddsType = $"<span class='underdog'>{ConvertByBetType.Odds4(ticket.MasterOdds, ticket.BetTypeId, ticket.SportTypeId, oddsType)}</span>";
                }
            }
            else if (IsMixParlay(ticket) && IsShowOdds(ticket))
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
            if(IsChangeOddsBetBuilderPlus(Template.oddsClassName))
            {
                rtfHelper.RTFRenderer.AddText(Template.odds, rtfHelper.PosFontCrossed);
                rtfHelper.RTFRenderer.AddText("\n" + Template.oddsType.Replace("<br/>", "\n").StripHTML(), rtfHelper.PosFont);
            }
            else if (IsParlayBoost(Template.oddsClassName))
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

            bool IsChangeOddsBetBuilderPlus(string cssClassName)
                => cssClassName == ChangeOdds;
        }

        public override IRichTextString RenderOddsTypeRTF(
            ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName, RTFHelper rtfHelper)
        {
            if(IsBetBuilderPlus(ticket))
            {
                var oddsTypeExcel = rtfHelper.RTFRenderer.Render();
                rtfHelper.RTFRenderer.Clear();

                return oddsTypeExcel;
            }

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
            if (IsParlayBoost(ticket) || IsChangedOdds(ticket))
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

        decimal GetOddsBetBuilderPlus(ITicket ticket)
        {
            var oddsBetBuilderPlus = ticket.ExtraInfo.HasAttributeInXmlTag(SportsbookTag, OddsBetBuilderPlusAttribute) ?
            ticket.ExtraInfo.GetXmlTagAttributeValue(SportsbookTag, OddsBetBuilderPlusAttribute) :
            "0";

            return Cast.AsDecimal(oddsBetBuilderPlus);
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

        bool IsChangedOdds(ITicket ticket)
        {
            if(ticket.IsSettled() && IsBetBuilderPlus(ticket) && !ticket.IsDraw())
            {
                var odds = GetOddsBetBuilderPlus(ticket);

                return odds != ticket.MasterOdds;
            }

            return false;
        }

        bool IsMixParlay(ITicket ticket) => ticket.BetTeam == "1";

        bool IsBetBuilderPlus(ITicket ticket) => ticket.OddsSpread == OddsSpreadForBetBuilderPlus;

        bool IsShowOdds(ITicket ticket ) => ticket.IsRunningWaiting() || ticket.IsSettled();
    }
}