namespace Fanex.BetList.Core.Builder.OddsBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using App_GlobalResources;
    using Entities;
    using NPOI.HSSF.UserModel;
    using NPOI.SS.UserModel;
    using Templates;
    using Utils;

    /// <summary>
    /// Win/Place bet type.
    /// </summary>
    public class Odds33 : Odds1
    {
        public Odds33()
        {
            Template = new Odds_Template();
            WinBetId = 31;
            PlaceBetId = 32;
        }

        protected int WinBetId { get; set; }

        protected int PlaceBetId { get; set; }

        /// <summary>
        /// Renders the specified ticket.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketData">The ticket data.</param>
        /// <param name="funcGetOddsTypeName">Name of the function get odds type.</param>
        /// <returns>Odds_Template object.</returns>
        public override Odds_Template Render(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName)
        {
            if (ticketData != null && ticketData.Count > 0)
            {
                List<ITicketData> foundTicketData = GetReferenceData(ticket, ticketData);

                if (foundTicketData != null && foundTicketData.Count > 1)
                {
                    ITicketData winTicket = foundTicketData.Find(item => item.BetTypeId == WinBetId);
                    ITicketData placeTicket = foundTicketData.Find(item => item.BetTypeId == PlaceBetId);

                    string oddsWin = GetOddsWin(winTicket);
                    string oddsPlace = GetOddsPlace(placeTicket);

                    var oddsWinMarkup = string.Join(null, new[] { CoreBetList.win, ": ", "<b>", oddsWin, "</b>" });
                    var oddsPlaceMarkup = string.Join(null, new[] { CoreBetList.place, ": ", "<b>", oddsPlace, "</b>" });

                    string status = ticket.Status.ToLowerInvariant();
                    if (status.Equals(BetStatus.Won) || status.Equals(BetStatus.Lose))
                    {
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
            }
            else
            {
                Template.odds = "-";
            }

            Template.oddsType = funcGetOddsTypeName(ConvertByBetType.OddsType(ticket.BetTypeId, ticket.OddsType));
            return Template;
        }

        protected virtual List<ITicketData> GetReferenceData(ITicket ticket, List<ITicketData> ticketData)
        {
            return ticketData.FindAll(item => item.RefNo.Equals(ticket.TransId.ToString()));
        }

        /// <summary>
        /// Builds the RTF odds.
        /// </summary>
        /// <param name="rtfHelper">The RTF helper.</param>
        /// <returns>IRichTextString object.</returns>
        protected override IRichTextString BuildRTFOdds(RTFHelper rtfHelper)
        {
            if (Template.odds == "-")
            {
                return new HSSFRichTextString("-");
            }

            int breakPos = Template.odds.IndexOf("<br>", StringComparison.InvariantCulture);

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

        /// <summary>
        /// Extract string value between tags from a source string.
        /// </summary>
        /// <param name="tag">Name of the tags.</param>
        /// <param name="source">Source string.</param>
        /// <returns>Value string.</returns>
        private static string ExtractStringBetweenTag(string tag, string source)
        {
            string startTag = string.Join(null, new[] { "<", tag, ">" });
            string endTag = string.Join(null, new[] { "</", tag, ">" });

            int startIndex = source.IndexOf(startTag, StringComparison.InvariantCulture) + startTag.Length;
            int endIndex = source.IndexOf(endTag, startIndex, StringComparison.InvariantCulture);
            return source.Substring(startIndex, endIndex - startIndex);
        }

        /// <summary>
        /// Gets the odds win.
        /// </summary>
        /// <param name="winTicket">The win ticket.</param>
        /// <returns>Odds of win.</returns>
        private static string GetOddsWin(ITicketData winTicket)
        {
            string winStatus = winTicket.Status.ToLowerInvariant();
            string oddsWin;

            oddsWin = !winStatus.Equals(BetStatus.Won) && !winStatus.Equals(BetStatus.Lose)
                ? "-"
                : Formatter.FormatNumber3(winTicket.Odds);

            return oddsWin;
        }

        /// <summary>
        /// Gets the odds place.
        /// </summary>
        /// <param name="placeTicket">The place ticket.</param>
        /// <returns>Odds of place.</returns>
        private static string GetOddsPlace(ITicketData placeTicket)
        {
            string placeStatus = placeTicket.Status.ToLowerInvariant();

            string oddsPlace;

            oddsPlace = !placeStatus.Equals(BetStatus.Won) && !placeStatus.Equals(BetStatus.Lose)
                ? "-"
                : Formatter.FormatNumber3(placeTicket.Odds);

            return oddsPlace;
        }
    }
}