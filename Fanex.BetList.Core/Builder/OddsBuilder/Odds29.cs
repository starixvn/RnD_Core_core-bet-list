namespace Fanex.BetList.Core.Builder.OddsBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Xml.Linq;
    using BetListNPOI.SS.UserModel;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;
    using Fanex.BetList.Core.Utils;
    using Fanex.BetList.Core.Utils.StringExt;
    using Fanex.BetList.Core.Utils.TicketExt;

    public class Odds29 : Odds1
    {
        protected const string StrikeThrough = "strikethrough";
        private const int OddsSpreadForBoots = 2;
        private const string SportsbookTag = "p1";
        private const string BonusOdds = "bbo";
        private const string OriginalOdds = "o";

        public override Odds_Template Render(
            ITicket ticket,
            List<ITicketData> ticketData,
            GetCachePropertyById funcGetOddsTypeName
        )
        {
            if (!IsMixParlay(ticket) || ticket.IsVoidReject())
            {
                Template.odds = "-";
                Template.oddsType = string.Empty;

                return Template;
            }

            var oddsType = string.IsNullOrEmpty(ticket.OddsType) ? (object)0 : ticket.OddsType;
            var firstOdds = GetFirstOdds(ticket);
            var secondOdds = GetSecondOdds(ticket);
            Template.odds = ConvertByBetType.Odds4(
                firstOdds,
                ticket.BetTypeId,
                ticket.SportTypeId,
                oddsType
            );
            Template.oddsType = string.Empty;
            if (ConvertByBetType.IsShowSecondOdds(firstOdds, secondOdds))
            {
                var oddsFormat = ConvertByBetType.Odds4(
                    secondOdds,
                    ticket.BetTypeId,
                    ticket.SportTypeId,
                    oddsType
                );
                Template.odds = $"<br><span class='{StrikeThrough}'>{Template.odds}</span>";
                Template.odds += $"<br><span class='underdog'>{oddsFormat}</span>";

                if (IsShowBoostRate(ticket))
                {
                    Template.odds +=
                        $"<br><span class='boostrate'><span class='icon-boost'></span>{FormatBoostRate(ticket)}</span>";
                }
            }

            return Template;
        }

        public override string RenderOddsType(
            ITicket ticket,
            List<ITicketData> ticketData,
            GetCachePropertyById funcGetOddsTypeName
        ) => string.Empty;

        protected override IRichTextString BuildRTFOdds(RTFHelper rtfHelper)
        {
            var items = SplitHTMLIntoRows(Template.odds);
            if (items.Length >= 2)
            {
                return BuildOddValue(Template.odds, rtfHelper);
            }

            return base.BuildRTFOdds(rtfHelper);
        }

        public override IRichTextString RenderOddsValueRTF(
            ITicket ticket,
            List<ITicketData> ticketData,
            GetCachePropertyById funcGetOddsTypeName,
            RTFHelper rtfHelper
        )
        {
            var oddsValue = RenderOddsValue(ticket, ticketData, funcGetOddsTypeName);
            var items = SplitHTMLIntoRows(Template.odds);
            if (items.Length >= 2)
            {
                return BuildOddValue(oddsValue, rtfHelper);
            }

            return base.RenderOddsValueRTF(ticket, ticketData, funcGetOddsTypeName, rtfHelper);
        }

        private IRichTextString BuildOddValue(string html, RTFHelper rtfHelper)
        {
            var items = SplitHTMLIntoRows(Template.odds);
            if (items.Length >= 2)
            {
                rtfHelper.RTFRenderer.AddText("\n" + items[0], rtfHelper.PosFontCrossed);
                rtfHelper.RTFRenderer.AddText("\n" + items[1], rtfHelper.PosFont);
                if (items.Length == 3)
                {
                    rtfHelper.RTFRenderer.AddText("\n" + items[2], rtfHelper.PosFont);
                }
                var oddsTypeExcel = rtfHelper.RTFRenderer.Render();
                rtfHelper.RTFRenderer.Clear();

                return oddsTypeExcel;
            }

            return rtfHelper.RTFRenderer.Render();
        }

        private decimal GetFirstOdds(ITicket ticket) =>
            ticket.ExtraInfo.HasAttributeInXmlTag(SportsbookTag, OriginalOdds)
                ? Cast.AsDecimal(
                    ticket.ExtraInfo.GetXmlTagAttributeValue(SportsbookTag, OriginalOdds)
                )
                : ticket.Odds;

        private decimal GetSecondOdds(ITicket ticket) =>
            ticket.IsRunningWaiting()
                ? Cast.AsDecimal(ticket.ExtraInfo.GetXmlTagAttributeValue(SportsbookTag, BonusOdds))
                : ticket.MasterOdds;

        private string FormatBoostRate(ITicket ticket)
        {
            var rate = GetBoostRate();

            return ConvertByBetType.FormatBoostRate(rate);

            decimal GetBoostRate()
            {
                var rateString = ticket.TransDesc;
                if (ticket.IsRunningWaiting())
                {
                    const string boostRateAttribute = "bbp";
                    rateString = ticket.ExtraInfo.HasAttributeInXmlTag(
                        SportsbookTag,
                        boostRateAttribute
                    )
                        ? ticket.ExtraInfo.GetXmlTagAttributeValue(
                            SportsbookTag,
                            boostRateAttribute
                        )
                        : "0";
                }

                return Cast.AsDecimal(rateString);
            }
        }

        bool IsShowBoostRate(ITicket ticket)
        {
            if (IsMixParlay(ticket) && ticket.OddsSpread == OddsSpreadForBoots)
            {
                const string hasBoostTicket = "bba";
                var bba = ticket.ExtraInfo.HasAttributeInXmlTag(SportsbookTag, hasBoostTicket)
                    ? ticket.ExtraInfo.GetXmlTagAttributeValue(SportsbookTag, hasBoostTicket)
                    : "0";

                return ticket.IsRunningWaiting() || (ticket.IsWon() && Cast.AsDecimal(bba) > 0);
            }

            return false;
        }

        bool IsMixParlay(ITicket ticket) => ticket.BetTeam == "1";

        private static string[] SplitHTMLIntoRows(string html)
        {
            const string splitter = "</span>";
            var htmlTagMatching = new Regex("</?[^<>]*>");

            return Regex
                .Split(html, splitter)
                .Select(row => htmlTagMatching.Replace(row, string.Empty))
                .Where(row => row != string.Empty)
                .ToArray();
        }
    }
}
