namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using System.Collections.Generic;
    using System.Globalization;
    using App_GlobalResources;
    using Entities;
    using NPOI.SS.UserModel;
    using Templates;
    using Utils;
    using Utils.StringExt;

    /// <summary>
    /// Sportsbook: Handicap.
    /// </summary>
    public class Status1 : IStatus
    {
        private const string CashOutXmlTag = "p25";

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

            BuildCashOutHistory(ticket, ticketHelper);

            return Template;
        }

        public virtual IRichTextString RenderRTF(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowVNIP, RTFHelper rtfHelper)
        {
            Render(ticket, ticketHelper, ticketData, isShowVNIP);

            string status = Template.result;

            BuildResultLRF(status, rtfHelper);

            if (Template.ShowIP != null && Template.ShowIP.Visible)
            {
                status = string.Join(null, new[] { "\n", Template.ShowIP.betIp });
                rtfHelper.RTFRenderer.AddText(status);
            }

            IRichTextString rtfStatus = rtfHelper.RTFRenderer.Render();
            rtfHelper.RTFRenderer.Clear();

            return rtfStatus;
        }

        protected static string GetResultString(string ticketStatus)
            => GetResultString(ticketStatus, 0);

#pragma warning disable S1541 // Methods and properties should not be too complex

        protected static string GetResultString(string ticketStatus, byte settlementStatus)
        {
            string result = string.Empty;

            switch (ticketStatus.ToLowerInvariant())
            {
                case BetStatus.Running:
                    result = DetectRealRunningStatus(settlementStatus);
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

                case BetStatus.HalfWon:
                    result = CoreBetList.halfwon;
                    break;

                case BetStatus.HalfLose:
                    result = CoreBetList.halflose;
                    break;

                default:
                    result = string.Empty;
                    break;
            }

            return result;
        }

#pragma warning restore S1541 // Methods and properties should not be too complex

        protected virtual void BuildResult(ITicket ticket, List<ITicketData> ticketData)
        {
            var result = ticket.Status.ToLower(CultureInfo.InvariantCulture);
            var resultText = GetResultString(ticket.Status, ticket.SettlementStatus);

            Template.result = result;

            if (!string.IsNullOrEmpty(result))
            {
                Template.result = resultText;
            }

            if (IsCashOutOriginalTicket(ticket) && IsCashOutAll(ticket) && IsStatusNotVoidAndRefund(result))
            {
                Template.result = CoreBetList.CashOut;
            }
        }

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

        protected virtual void BuildIP(ITicket ticket, ITicketHelper ticketHelper, bool isShowVNIP)
        {
            if (!isShowVNIP && (ticketHelper.IsVnIp != null && ticketHelper.IsVnIp(ticket.IP)))
            {
                Template.ShowIP.Hide();
                return;
            }

            Template.ShowIP.betIp = ticket.IP;
        }

        protected virtual void BuildResultLRF(string status, RTFHelper rtfHelper)
        {
            rtfHelper.RTFRenderer.AddText(status, rtfHelper.PosFont);
        }

        protected virtual void BuildCashOutHistory(ITicket ticket, ITicketHelper ticketHelper)
        {
            if (ticketHelper.HideStatusCashOutHistory || !IsCashOutTicket(ticket))
            {
                Template.CashOutHistory.Hide();
                return;
            }

            Template.CashOutHistory.transId = ticket.TransId.ToString();
            Template.CashOutHistory.refNo = ticket.RefNo;
            Template.CashOutHistory.betType = ticket.BetTypeId.ToString();
            Template.CashOutHistory.betdaqId = ticket.BetdaqId.ToString();
        }

        private static bool IsCashOutTicket(ITicket ticket)
            => (ticket.BetdaqId == 1 && !IsBetTradeTicket(ticket.BetTypeId) && IsSportsbook(ticket.SportTypeId)) ||
            ticket.ExtraInfo.HasAttributeInXmlTag(CashOutXmlTag, "s");

        private static bool IsCashOutOriginalTicket(ITicket ticket)
            => ticket.BetdaqId == 0 && ticket.ExtraInfo.HasAttributeInXmlTag(CashOutXmlTag, "s");

        private static bool IsBetTradeTicket(int betTypeId)
            => betTypeId == 901 || betTypeId == 902;

        private static bool IsStatusNotVoidAndRefund(string status) => !status.Equals(BetStatus.Void) && !status.Equals(BetStatus.Refund);

        private static bool IsCashOutAll(ITicket ticket) => ticket.Stake == 0 && StakeIsZeroBeforeConvertCurrency(ticket);

        private static bool StakeIsZeroBeforeConvertCurrency(ITicket ticket) => ticket.OriginalBetType != -1;

        private static bool IsSportsbook(int sportTypeId) => sportTypeId <= 99 || sportTypeId == 154;

        private static string DetectRealRunningStatus(byte settlementStatus)
        {
            const byte pendingStatus = 1;
            const byte underSettlementStatus = 4;
            const byte systemSettlementStatus = 8;

            if ((settlementStatus & pendingStatus) == pendingStatus)
            {
                return CoreBetList.ResultPending;
            }

            if ((settlementStatus & underSettlementStatus) == underSettlementStatus
                || (settlementStatus & systemSettlementStatus) == systemSettlementStatus)
            {
                return CoreBetList.UnderSettlement;
            }

            return CoreBetList.running;
        }
    }
}