namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using System.Collections.Generic;
    using System.Globalization;
    using App_GlobalResources;
    using Entities;
    using NPOI.SS.UserModel;
    using Templates;
    using Utils;

    /// <summary>
    /// Build Status cell.
    /// </summary>
    public class Status1 : IStatus
    {
        public Status1()
        {
            Template = new Status_Template();
        }

        public Status_Template Template { get; set; }

        public virtual Status_Template Render(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowVNIP)
        {
            BuildResult(ticket, ticketData);

            BuildStatusResult(ticket);

            BuildIP(ticket, ticketHelper, isShowVNIP);

            return Template;
        }

        /// <summary>
        /// Renders the RTF.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <param name="ticketData">The ticket data.</param>
        /// <param name="isShowVNIP">If set to <c>true</c> [VNIP is shown].</param>
        /// <param name="rtfHelper">The RTF helper.</param>
        /// <returns>IRichTextString represents the Status block.</returns>
        public virtual IRichTextString RenderRTF(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowVNIP, RTFHelper rtfHelper)
        {
            Render(ticket, ticketHelper, ticketData, isShowVNIP);

            string status = Template.result;

            BuildResultLRF(status, rtfHelper);

            if (Template.ShowIP != null && Template.ShowIP.Visible)
            {
                status = string.Join(null, new string[] { "\n", Template.ShowIP.betIp });
                rtfHelper.RTFRenderer.AddText(status);
            }

            IRichTextString rtfStatus = rtfHelper.RTFRenderer.Render();
            rtfHelper.RTFRenderer.Clear();

            return rtfStatus;
        }

        /// <summary>
        /// Gets the result string.
        /// </summary>
        /// <param name="ticketStatus">The ticket status.</param>
        /// <returns>System.String: result.</returns>
        protected static string GetResultString(string ticketStatus)
        {
            string result = string.Empty;

            switch (ticketStatus.ToLower())
            {
                case BetStatus.Running:
                    result = CoreBetList.running;
                    break;

                case BetStatus.Reject:
                    result = CoreBetList.reject;
                    break;

                case BetStatus.Won:
                    result = CoreBetList.won;
                    break;

                case BetStatus.Lose:
                    result = CoreBetList.lose;
                    break;

                case BetStatus.Draw:
                    result = CoreBetList.draw;
                    break;

                case BetStatus.Waiting:
                    result = CoreBetList.waiting;
                    break;

                case BetStatus.Void:
                    result = CoreBetList.voided;
                    break;

                case BetStatus.Refund:
                    result = CoreBetList.refund;
                    break;
            }

            return result;
        }

        protected virtual void BuildResult(ITicket ticket, List<ITicketData> ticketData)
        {
            Template.result = ticket.Status.ToLower(CultureInfo.InvariantCulture);
            string result = GetResultString(ticket.Status);

            if (!string.IsNullOrEmpty(result))
            {
                Template.result = result;
            }
        }

        /// <summary>
        /// Builds the status result.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        protected virtual void BuildStatusResult(ITicket ticket)
        {
            var result = ticket.Status.ToLower(CultureInfo.InvariantCulture);

            if (result.Equals(BetStatus.Running)
                || result.Equals(BetStatus.Waiting)
                || result.Equals(BetStatus.Void)
                || result.Equals(BetStatus.Reject)
                || result.Equals(BetStatus.Refund))
            {
                Template.StatusResult.Hide();
            }
            else
            {
                Template.StatusResult.matchId = ticket.MatchId.ToString(CultureInfo.InvariantCulture);
                Template.StatusResult.betType = ticket.BetTypeId.ToString(CultureInfo.InvariantCulture);
                Template.StatusResult.race = string.IsNullOrEmpty(ticket.Race) ? "0" : ticket.Race;
                Template.StatusResult.refNo = ticket.TransId.ToString(CultureInfo.InvariantCulture);
                Template.StatusResult.sportType = ticket.SportTypeId.ToString(CultureInfo.InvariantCulture);
                Template.StatusResult.league = ticket.LeagueId.ToString(CultureInfo.InvariantCulture);
                Template.StatusResult.userName = ticket.UserName;
                Template.StatusResult.winlostDate = Formatter.FullDateFormat(ticket.WinlostDate);
            }
        }

        /// <summary>
        /// Builds the IP.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <param name="isShowVNIP">If set to <c>true</c> [is show VNIP].</param>
        protected virtual void BuildIP(ITicket ticket, ITicketHelper ticketHelper, bool isShowVNIP)
        {
            if (isShowVNIP)
            {
                Template.ShowIP.betIp = ticket.IP;
            }
            else
            {
                if (ticketHelper.IsVnIp(ticket.IP))
                {
                    Template.ShowIP.Hide();
                }
                else
                {
                    Template.ShowIP.betIp = ticket.IP;
                }
            }
        }

        protected virtual void BuildResultLRF(string status, RTFHelper rtfHelper)
        {
            rtfHelper.RTFRenderer.AddText(status, rtfHelper.PosFont);
        }
    }
}