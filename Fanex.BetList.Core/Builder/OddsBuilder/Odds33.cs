namespace Fanex.BetList.Core.Builder.OddsBuilder
{
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
    public class Odds33 : IOdds
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Odds33" /> class.
        /// </summary>
        public Odds33()
        {
            Template = new Odds_Template();
            WinBetId = 31;
            PlaceBetId = 32;
        }

        public Odds_Template Template { get; set; }

        protected int WinBetId { get; set; }

        protected int PlaceBetId { get; set; }

        /// <summary>
        /// Renders the specified ticket.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketData">The ticket data.</param>
        /// <param name="funcGetOddsTypeName">Name of the function get odds type.</param>
        /// <returns>Odds_Template object.</returns>
        public virtual Odds_Template Render(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName)
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

                    var oddsWinMarkup = string.Join(null, new string[] { CoreBetList.win, ": ", "<b>", oddsWin, "</b>" });
                    var oddsPlaceMarkup = string.Join(null, new string[] { CoreBetList.place, ": ", "<b>", oddsPlace, "</b>" });

                    string status = ticket.Status.ToLower();
                    if (status.Equals(BetStatus.Won) || status.Equals(BetStatus.Lose))
                    {
                        var oddsWinContent = string.Join(null, new string[] { "<span class='blue' style='font-weight:normal;'>", oddsWinMarkup, "</span>" });
                        var oddsPlaceContent = string.Join(null, new string[] { "<span class='' style='font-weight:normal;'>", oddsPlaceMarkup, "</span>" });
                        Template.odds = string.Join(null, new string[] { oddsWinContent, "<br>", oddsPlaceContent });
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
        
        public IRichTextString RenderRTF(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName, RTFHelper rtfHelper)
        {
            Render(ticket, ticketData, funcGetOddsTypeName);
            var rtfOdds = BuildRTFOdds(rtfHelper);

            return rtfOdds;
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
        protected virtual IRichTextString BuildRTFOdds(RTFHelper rtfHelper)
        {
            if (Template.odds == "-")
            {
                return new HSSFRichTextString("-");
            }

            int breakPos = Template.odds.IndexOf("<br>");

            string odds_WinString = Template.odds.Substring(0, breakPos);
            string odds_PlaceString = Template.odds.Replace(odds_WinString, string.Empty);

            string odds_WinVal = ExtractStringBetweenTag("b", odds_WinString);
            string odds_PlaceVal = ExtractStringBetweenTag("b", odds_PlaceString);

            var rtfNumberRenderer = new RTFNumber(rtfHelper.RTFRenderer, rtfHelper.PosFont, rtfHelper.NegFont);

            // Add Win
            rtfHelper.RTFRenderer.AddText(string.Join(null, new string[] { CoreBetList.win, ": " }), rtfHelper.PosFont);
            rtfNumberRenderer.Render(odds_WinVal);

            // Add Place
            rtfHelper.RTFRenderer.AddText(string.Join(null, new string[] { "\n", CoreBetList.place, ": " }), rtfHelper.PosFont);
            rtfNumberRenderer.Render(odds_PlaceVal);

            var rtfOdds = rtfHelper.RTFRenderer.Render();
            rtfHelper.RTFRenderer.Clear();
            return rtfOdds;
        }

        /// <summary>
        /// Gets the place.
        /// </summary>
        /// <param name="odds_place">The odds_place.</param>
        /// <param name="status_place">The status_place.</param>
        /// <param name="foundRows">The found rows.</param>
        /// <param name="i">The index.</param>
        private static void GetPlace(ref string odds_place, ref string status_place, DataRow[] foundRows, int i)
        {
            switch (foundRows[i]["status"].ToString().ToLower())
            {
                case BetStatus.Won:
                    {
                        odds_place += string.Join(null, new string[] { CoreBetList.place, ": ", "<b>", Formatter.FormatNumber20(foundRows[i]["odds"], null), "</b>" });

                        status_place += string.Join(null, new string[] { CoreBetList.place, ": ", "<b>", CoreBetList.won, "</b>" });
                        break;
                    }

                default:
                    {
                        odds_place += string.Join(null, new string[] { CoreBetList.place, ": ", "<b>", "0.00", "</b>" });

                        status_place += string.Join(null, new string[] { CoreBetList.place, ": ", "<b>", CoreBetList.lose, "</b>" });
                        break;
                    }
            }
        }

        /// <summary>
        /// Gets the win.
        /// </summary>
        /// <param name="odds_win">The odds_win.</param>
        /// <param name="status_win">The status_win.</param>
        /// <param name="foundRows">The found rows.</param>
        /// <param name="i">The index.</param>
        private static void GetWin(ref string odds_win, ref string status_win, DataRow[] foundRows, int i)
        {
            switch (foundRows[i]["status"].ToString().ToLower())
            {
                case BetStatus.Won:
                    {
                        odds_win += string.Join(null, new string[] { CoreBetList.win, ": ", "<b>", Formatter.FormatNumber20(foundRows[i]["odds"], null), "</b>" });

                        status_win += string.Join(null, new string[] { CoreBetList.win, ": ", "<b>", CoreBetList.won, "</b>" });

                        break;
                    }

                default:
                    {
                        odds_win += string.Join(null, new string[] { CoreBetList.win, ": ", "<b>", "0.00", "</b>" });

                        status_win += string.Join(null, new string[] { CoreBetList.win, ": ", "<b>", CoreBetList.lose, "</b>" });

                        break;
                    }
            }
        }

        /// <summary>
        /// Sets the odds.
        /// </summary>
        /// <param name="row">The row object.</param>
        /// <param name="odds_win">The odds_win.</param>
        /// <param name="odds_place">The odds_place.</param>
        private void SetOdds(ITicket row, ref string odds_win, ref string odds_place)
        {
            string status = row.Status.ToString().ToLower();
            if (status.Equals(BetStatus.Won) || status.Equals(BetStatus.Lose))
            {
                odds_win = "<span class='blue' style='font-weight:normal;'>" + odds_win + "</span>";
                odds_place = "<span class='' style='font-weight:normal;'>" + odds_place + "</span>";
                Template.odds = odds_win + "<br>" + odds_place;
            }
            else
            {
                Template.odds = "-";
            }
        }

        /// <summary>
        /// Extract string value between tags from a source string.
        /// </summary>
        /// <param name="tag">Name of the tags.</param>
        /// <param name="source">Source string.</param>
        /// <returns>Value string.</returns>
        private string ExtractStringBetweenTag(string tag, string source)
        {
            string startTag = string.Join(null, new string[] { "<", tag, ">" });
            string endTag = string.Join(null, new string[] { "</", tag, ">" });

            int startIndex = source.IndexOf(startTag) + startTag.Length;
            int endIndex = source.IndexOf(endTag, startIndex);
            return source.Substring(startIndex, endIndex - startIndex);
        }

        /// <summary>
        /// Gets the odds win.
        /// </summary>
        /// <param name="winTicket">The win ticket.</param>
        /// <returns>Odds of win.</returns>
        private string GetOddsWin(ITicketData winTicket)
        {
            string winStatus = winTicket.Status.ToLower();
            string oddsWin = string.Empty;

            if (!winStatus.Equals(BetStatus.Won) && !winStatus.Equals(BetStatus.Lose))
            {
                oddsWin = "-";
            }
            else
            {
                oddsWin = Formatter.FormatNumber3(winTicket.Odds);
            }

            return oddsWin;
        }

        /// <summary>
        /// Gets the odds place.
        /// </summary>
        /// <param name="placeTicket">The place ticket.</param>
        /// <returns>Odds of place.</returns>
        private string GetOddsPlace(ITicketData placeTicket)
        {
            string placeStatus = placeTicket.Status.ToLower();

            string oddsPlace = string.Empty;

            if (!placeStatus.Equals(BetStatus.Won) && !placeStatus.Equals(BetStatus.Lose))
            {
                oddsPlace = "-";
            }
            else
            {
                oddsPlace = Formatter.FormatNumber3(placeTicket.Odds);
            }

            return oddsPlace;
        }
    }
}