namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Entities;
    using NPOI.SS.UserModel;

    /// <summary>
    /// Bet Type: Win/Place.
    /// </summary>
    public class Status33 : Status31
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Status33" /> class.
        /// </summary>
        public Status33()
        {
            WinBetId = 31;
            PlaceBetId = 32;
        }

        protected int WinBetId { get; set; }

        protected int PlaceBetId { get; set; }

        /// <summary>
        /// Renders the RTF.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <param name="ticketData">The ticket data.</param>
        /// <param name="isShowVNIP">If set to <c>true</c> [VNIP is shown].</param>
        /// <param name="rtfHelper">The RTF helper.</param>
        /// <returns>
        /// IRichTextString represents the Status block.
        /// </returns>
        public override IRichTextString RenderRTF(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowVNIP, RTFHelper rtfHelper)
        {
            Render(ticket, ticketHelper, ticketData, isShowVNIP);

            // Status != Win/Lose
            if (Template.result.Length < 80)
            {
                rtfHelper.RTFRenderer.AddText(Template.result, rtfHelper.PosFont);
            }
            else
            {
                int breakPos = Template.result.IndexOf("<br/>");

                string status_WinString = Template.result.Substring(0, breakPos);
                string status_PlaceString = Template.result.Replace(status_WinString, string.Empty);

                string status_Win = ExtractStringBetweenTag("b", status_WinString);
                string status_Place = ExtractStringBetweenTag("b", status_PlaceString);
                string[] status = new string[] { CoreBetList.win, ": ", status_Win, "\n", CoreBetList.place, ": ", status_Place };
                rtfHelper.RTFRenderer.AddText(string.Join(null, status), rtfHelper.PosFont);
            }

            if (Template.ShowIP.Visible)
            {
                string status = string.Join(null, new string[] { "\n", Template.ShowIP.betIp });
                rtfHelper.RTFRenderer.AddText(status);
            }

            var rtfStatus = rtfHelper.RTFRenderer.Render();
            rtfHelper.RTFRenderer.Clear();

            return rtfStatus;
        }

        /// <summary>
        /// Builds the result.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketData">The ticket data.</param>
        protected override void BuildResult(ITicket ticket, List<ITicketData> ticketData)
        {
            base.BuildResult(ticket, ticketData);

            // Custom Status generated from base class if ticketData has matched items for the current ticket
            if (ticketData != null && ticketData.Count > 0)
            {
                List<ITicketData> foundTicketData = GetReferenceData(ticket, ticketData);

                if (foundTicketData != null && foundTicketData.Count > 1)
                {
                    string winStatus = foundTicketData.Find(item => item.BetTypeId == WinBetId).Status.ToLower();
                    string placeStatus = foundTicketData.Find(item => item.BetTypeId == PlaceBetId).Status.ToLower();

                    string contextWinStatus = GetResultString(winStatus);
                    string contextPlaceStatus = GetResultString(placeStatus);

                    string[] tmpWinStatus = new string[] { "<span class='blue' style='font-weight:normal;'>", CoreBetList.win, ": ", "<b>", contextWinStatus, "</b>", "</span>" };

                    string winStatusMarkup = string.Join(null, tmpWinStatus);

                    string[] tmpPlaceStatus = new string[] { "<span class='' style='font-weight:normal;'>", CoreBetList.place, ": ", "<b>", contextPlaceStatus, "</b>", "</span>" };

                    string placeStatusMarkup = string.Join(null, tmpPlaceStatus);

                    string ticketBetStatus = ticket.Status.ToLower();

                    switch (ticketBetStatus)
                    {
                        case BetStatus.Running:
                        case BetStatus.Waiting:
                        case BetStatus.Void:
                        case BetStatus.Reject:
                        case BetStatus.Refund:
                            break;
                        default:
                            Template.result = string.Join(null, new string[] { winStatusMarkup, "<br/>", placeStatusMarkup });
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the reference data.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketData">The ticket data.</param>
        /// <returns>List of ITicketData.</returns>
        protected virtual List<ITicketData> GetReferenceData(ITicket ticket, List<ITicketData> ticketData)
        {
            return ticketData.FindAll(item => item.RefNo.Equals(ticket.TransId.ToString()));
        }

        /// <summary>
        /// Extract string value between tags from a source string.
        /// </summary>
        /// <param name="tag">Name of the tag.</param>
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
    }
}