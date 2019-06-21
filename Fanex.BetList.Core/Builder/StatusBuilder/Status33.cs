namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text.RegularExpressions;
    using App_GlobalResources;
    using Constants;
    using Entities;
    using NPOI.SS.UserModel;

    /// <summary>
    /// Bet Type: Win/Place.
    /// </summary>
    public class Status33 : Status31
    {
        public override IRichTextString RenderRTF(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowVNIP, RTFHelper rtfHelper)
        {
            Render(ticket, ticketHelper, ticketData, isShowVNIP);

            var htmlTagMatching = new Regex("</?[^<>]*>");
            var breakingLinesMatching = new Regex("<br\\s*/?>");
            var plainResultText = htmlTagMatching.Replace(breakingLinesMatching.Replace(Template.result, "\n"), string.Empty);

            rtfHelper.RTFRenderer.AddText(plainResultText, rtfHelper.PosFont);

            if (Template.ShowIP.Visible)
            {
                string status = string.Join(null, new[] { "\n", Template.ShowIP.betIp });
                rtfHelper.RTFRenderer.AddText(status);
            }

            var rtfStatus = rtfHelper.RTFRenderer.Render();
            rtfHelper.RTFRenderer.Clear();

            return rtfStatus;
        }

        protected override void BuildResult(ITicket ticket, List<ITicketData> ticketData)
        {
            base.BuildResult(ticket, ticketData);

            // Custom Status generated from base class if ticketData has matched items for the current ticket
            if (ticketData != null && ticketData.Count > 0)
            {
                List<ITicketData> foundTicketData = GetReferenceData(ticket, ticketData);

                if (foundTicketData != null && foundTicketData.Count > 1)
                {
                    string winStatus = foundTicketData.Find(item => item.BetTypeId == BetTypes.Win).Status.ToLowerInvariant();
                    string placeStatus = foundTicketData.Find(item => item.BetTypeId == BetTypes.Place).Status.ToLowerInvariant();

                    string contextWinStatus = GetResultString(winStatus);
                    string contextPlaceStatus = GetResultString(placeStatus);

                    string[] tmpWinStatus = { "<span class='blue' style='font-weight:normal;'>", CoreBetList.win, ": ", "<b>", contextWinStatus, "</b>", "</span>" };

                    string winStatusMarkup = string.Join(null, tmpWinStatus);

                    string[] tmpPlaceStatus = { "<span class='' style='font-weight:normal;'>", CoreBetList.place, ": ", "<b>", contextPlaceStatus, "</b>", "</span>" };

                    string placeStatusMarkup = string.Join(null, tmpPlaceStatus);

                    string ticketBetStatus = ticket.Status.ToLower(CultureInfo.InvariantCulture);

                    switch (ticketBetStatus)
                    {
                        case BetStatus.Running:
                        case BetStatus.Waiting:
                        case BetStatus.Void:
                        case BetStatus.Reject:
                        case BetStatus.Refund:
                            break;

                        default:
                            Template.result = string.Join(null, new[] { winStatusMarkup, "<br/>", placeStatusMarkup });
                            break;
                    }
                }
            }
        }

        protected virtual List<ITicketData> GetReferenceData(ITicket ticket, List<ITicketData> ticketData)
        {
            return ticketData.FindAll(item => item.RefNo.Equals(ticket.TransId.ToString()));
        }
    }
}