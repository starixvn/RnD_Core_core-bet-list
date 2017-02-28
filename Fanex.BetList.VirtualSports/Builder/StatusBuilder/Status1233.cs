namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using App_GlobalResources;
    using Entities;
    using NPOI.SS.UserModel;
    using System.Collections.Generic;
    using System.Globalization;
    using System;

    /// <summary>
    /// Virtual Horse Racing - Win/Place.
    /// </summary>
    public class Status1233 : Status1
    {
        public Status1233()
        {
            WinBetId = 1231;
            PlaceBetId = 1232;
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
                int breakPos = Template.result.IndexOf("<br/>", StringComparison.CurrentCulture);

                var status_WinString = Template.result.Substring(0, breakPos);
                var status_PlaceString = Template.result.Replace(status_WinString, string.Empty);

                var status_Win = ExtractStringBetweenTag("b", status_WinString);
                var status_Place = ExtractStringBetweenTag("b", status_PlaceString);
                var status = new string[] { CoreBetList.win, ": ", status_Win, "\n", CoreBetList.place, ": ", status_Place };
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
                    var winStatus = foundTicketData.Find(item => item.BetTypeId == WinBetId).Status.ToLower(CultureInfo.CurrentCulture);
                    var placeStatus = foundTicketData.Find(item => item.BetTypeId == PlaceBetId).Status.ToLower(CultureInfo.CurrentCulture);

                    var contextWinStatus = GetResultString(winStatus);
                    var contextPlaceStatus = GetResultString(placeStatus);

                    var tmpWinStatus = new string[] { "<span class='blue' style='font-weight:normal;'>", CoreBetList.win, ": ", "<b>", contextWinStatus, "</b>", "</span>" };

                    var winStatusMarkup = string.Join(null, tmpWinStatus);

                    var tmpPlaceStatus = new string[] { "<span class='' style='font-weight:normal;'>", CoreBetList.place, ": ", "<b>", contextPlaceStatus, "</b>", "</span>" };

                    var placeStatusMarkup = string.Join(null, tmpPlaceStatus);

                    var ticketBetStatus = ticket.Status.ToLower(CultureInfo.InvariantCulture);

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

        protected List<ITicketData> GetReferenceData(ITicket ticket, List<ITicketData> ticketData)
        {
            return ticketData.FindAll(item => item.RefNo.Equals(ticket.RefNo));
        }

        private string ExtractStringBetweenTag(string tag, string source)
        {
            string startTag = string.Join(null, new string[] { "<", tag, ">" });
            string endTag = string.Join(null, new string[] { "</", tag, ">" });

            int startIndex = source.IndexOf(startTag, StringComparison.CurrentCulture) + startTag.Length;
            int endIndex = source.IndexOf(endTag, startIndex, StringComparison.CurrentCulture);
            return source.Substring(startIndex, endIndex - startIndex);
        }
    }
}