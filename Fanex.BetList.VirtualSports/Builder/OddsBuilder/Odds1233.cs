namespace Fanex.BetList.Core.Builder.OddsBuilder
{
    using System;
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;
    using NPOI.HSSF.UserModel;
    using NPOI.SS.UserModel;
    using Templates;
    using Utils;

    /// <summary>
    /// Virtual Horse Racing - Win/Place.
    /// </summary>
    public class Odds1233 : Odds1
    {
        public override Odds_Template Render(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName)
        {
            if (ticketData != null && ticketData.Count > 0)
            {
                List<ITicketData> foundTicketData = GetReferenceData(ticket, ticketData);

                if (foundTicketData != null && foundTicketData.Count > 1)
                {
                    ITicketData winTicket = foundTicketData.Find(item => item.BetTypeId == InspireVirtualRacingBetTypes.Win);
                    ITicketData placeTicket = foundTicketData.Find(item => item.BetTypeId == InspireVirtualRacingBetTypes.Place);

                    string oddsWin = Formatter.FormatNumber3(winTicket.Odds);
                    string oddsPlace = Formatter.FormatNumber3(placeTicket.Odds);

                    var oddsWinMarkup = string.Join(null, new[] { CoreBetList.win, ": ", "<b>", oddsWin, "</b>" });
                    var oddsPlaceMarkup = string.Join(null, new[] { CoreBetList.place, ": ", "<b>", oddsPlace, "</b>" });

                    var oddsWinContent = string.Join(null, new[] { "<span class='blue' style='font-weight:normal;'>", oddsWinMarkup, "</span>" });
                    var oddsPlaceContent = string.Join(null, new[] { "<span class='' style='font-weight:normal;'>", oddsPlaceMarkup, "</span>" });

                    Template.odds = string.Join(null, new[] { oddsWinContent, "<br>", oddsPlaceContent });
                }
                else
                {
                    Template.odds = "-";
                }
            }
            else
            {
                Template.odds = "-";
            }

            Template.oddsType = funcGetOddsTypeName(ConvertByBetType.OddsType(ticket.BetTypeId, ticket.OddsType));

            return Template;
        }

        protected static List<ITicketData> GetReferenceData(ITicket ticket, List<ITicketData> ticketData)
        {
            return ticketData.FindAll(item => item.RefNo.Equals(ticket.RefNo));
        }

        protected override IRichTextString BuildRTFOdds(RTFHelper rtfHelper)
        {
            if (Template.odds == "-")
            {
                return new HSSFRichTextString("-");
            }

            int breakPos = Template.odds.IndexOf("<br>", StringComparison.CurrentCulture);

            string oddsWinString = Template.odds.Substring(0, breakPos);
            string oddsPlaceString = Template.odds.Replace(oddsWinString, string.Empty);

            string oddsWinVal = ExtractStringBetweenTag("b", oddsWinString);
            string oddsPlaceVal = ExtractStringBetweenTag("b", oddsPlaceString);

            var rtfNumberRenderer = new RTFNumber(rtfHelper.RTFRenderer, rtfHelper.PosFont, rtfHelper.NegFont);

            // Add Win
            rtfHelper.RTFRenderer.AddText(string.Join(null, new[] { CoreBetList.win, ": " }), rtfHelper.PosFont);
            rtfNumberRenderer.Render(oddsWinVal);

            // Add Place
            rtfHelper.RTFRenderer.AddText(string.Join(null, new[] { "\n", CoreBetList.place, ": " }), rtfHelper.PosFont);
            rtfNumberRenderer.Render(oddsPlaceVal);

            var rtfOdds = rtfHelper.RTFRenderer.Render();
            rtfHelper.RTFRenderer.Clear();
            return rtfOdds;
        }

        private static string ExtractStringBetweenTag(string tag, string source)
        {
            string startTag = string.Join(null, new[] { "<", tag, ">" });
            string endTag = string.Join(null, new[] { "</", tag, ">" });

            int startIndex = source.IndexOf(startTag, StringComparison.CurrentCulture) + startTag.Length;
            int endIndex = source.IndexOf(endTag, startIndex, StringComparison.CurrentCulture);
            return source.Substring(startIndex, endIndex - startIndex);
        }
    }
}