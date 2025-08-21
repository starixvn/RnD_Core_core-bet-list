namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using App_GlobalResources;
    using Constants;
    using Entities;
    using BetListNPOI.SS.UserModel;
    using Templates;
    using Utils;
    using Utils.StringExt;
    using Utils.TicketExt;
    using Fanex.BetList.Core.Constants;
    
    /// <summary>
    /// Sportsbook: Handicap.
    /// </summary>
    public class Status1 : IStatus
    {
        private const string CashOutXmlTag = "p25";
        private const string AbnormalBetXmlTag = "p1";
        private const string AbnormalBetAttributeName = "ab";

        public Status1()
        {
            Template = new Status_Template();
        }

        public Status_Template Template { get; set; }

        #region RENDER HTML

        public virtual Status_Template Render(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowVNIP)
        {
            BuildResult(ticket, ticketData);

            BuildStatusResult(ticket);

            BuildIP(ticket, ticketHelper, isShowVNIP);

            BuildCashOutHistory(ticket, ticketHelper);

            BuildNewCashOutHistory(ticket);

            BuildNewCashOutAmount(ticket);

            return Template;
        }

        public virtual string RenderStatusResultText(ITicket ticket, List<ITicketData> ticketData)
        {
            BuildResult(ticket, ticketData);
            Template.NewCashOutAmount = null;
            Template.StatusResult = null;
            Template.ShowIP = null;
            Template.CashOutHistory = null;
            Template.NewCashOutHistory = null;

            return Template.ToString();
        }

        protected virtual void BuildResult(ITicket ticket, List<ITicketData> ticketData)
        {
            var status = ticket.Status.ToLowerInvariant();

            if (IsCashOutOriginalTicket(ticket) && IsCashOutAll(ticket) && IsStatusNotVoidAndRefund(status))
            {
                Template.result = CoreBetList.CashOut;
                return;
            }

            if (DetectAbnormalBet(ticket))
            {
                Template.result = CoreBetList.AbnormalBet;
                return;
            }

            var statusText = GetResultString(ticket.Status, ticket.SettlementStatus);
            statusText = GetHalfWonLost(statusText, ticket);
            Template.result = string.IsNullOrEmpty(statusText) ? status : statusText;
        }

        protected virtual bool DetectAbnormalBet(ITicket ticket)
        {
            var status = ticket.Status.ToLowerInvariant();

            return status.Equals(BetStatus.Void, StringComparison.InvariantCulture)
               && ticket.ExtraInfo.GetXmlTagAttributeValue(AbnormalBetXmlTag, AbnormalBetAttributeName) == "1";
        }

        protected virtual void BuildStatusResult(ITicket ticket)
        {
            if (IsNewSettledCashOutTicket(ticket))
            {
                Template.StatusResult.Hide();
            }
            else if (ticket.IsSettled() || IsSportsbookRefundedTicket(ticket))
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
            else
            {
                Template.StatusResult.Hide();
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

        protected virtual void BuildNewCashOutHistory(ITicket ticket)
        {
            if (IsNewCashOutTicket(ticket))
            {
                Template.NewCashOutHistory.transId = ticket.TransId.ToString();
                Template.NewCashOutHistory.custId = ticket.CustId.ToString();
                Template.NewCashOutHistory.winlostdate = Formatter.FullDateFormat(ticket.WinlostDate);
                Template.NewCashOutHistory.currency = ticket.Currency;
                Template.NewCashOutHistory.Show();
            }
            else
            {
                Template.NewCashOutHistory.Hide();
            }
        }

        protected virtual void BuildNewCashOutAmount(ITicket ticket)
        {
            if (IsNewSettledCashOutTicket(ticket))
            {
                var amountText = ticket.IsSettled() ? $": {ConvertByBetType.Winloss(ticket.Winlost + ticket.ActualStake)}" : string.Empty;
                Template.NewCashOutAmount.amount = amountText;
                Template.NewCashOutAmount.Show();
            }
            else
            {
                Template.NewCashOutAmount.Hide();
            }
        }

        #endregion RENDER HTML

        #region RENDER EXCEL

        public virtual IRichTextString RenderRTF(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowVNIP, RTFHelper rtfHelper)
        {
            Render(ticket, ticketHelper, ticketData, isShowVNIP);

            string status = Template.result;

            BuildResultLRF(status, rtfHelper);

            if (Template.NewCashOutAmount != null && Template.NewCashOutAmount.Visible)
            {
                var amountText = ticket.IsSettled() ? $": {ConvertByBetType.Winloss(ticket.Winlost + ticket.ActualStake)}" : string.Empty;
                status = string.Join(null, "\n", $"({CoreBetList.CashOut}{amountText})");
                rtfHelper.RTFRenderer.AddText(status);
            }

            if (Template.ShowIP != null && Template.ShowIP.Visible)
            {
                status = string.Join(null, "\n", Template.ShowIP.betIp);
                rtfHelper.RTFRenderer.AddText(status);
            }

            IRichTextString rtfStatus = rtfHelper.RTFRenderer.Render();
            rtfHelper.RTFRenderer.Clear();

            return rtfStatus;
        }

        protected virtual void BuildResultLRF(string status, RTFHelper rtfHelper)
        {
            rtfHelper.RTFRenderer.AddText(status, rtfHelper.PosFont);
        }

        #endregion RENDER EXCEL

        protected virtual string GetHalfWonLost(string statusText, ITicket ticket)
        {
            var status = ticket.Status.ToLowerInvariant();
            if (status == BetStatus.Won && ticket.IsSportsbook() && ticket.MasterOdds == CommonValue.HalfWonLostValue)
            {
                return CoreBetList.halfwon;
            }
            else if (status == BetStatus.Lose && ticket.IsSportsbook() && ticket.MasterOdds == -1 * CommonValue.HalfWonLostValue)
            {
                return CoreBetList.halflose;
            }
            else
            {
                return statusText;
            }
        }
        
        protected static string GetResultString(string ticketStatus)
          => GetResultString(ticketStatus, 0);

#pragma warning disable S1541 // Methods and properties should not be too complex

        protected static string GetResultString(string ticketStatus, byte settlementStatus)
        {
            switch (ticketStatus.ToLowerInvariant())
            {
                case BetStatus.Running:
                    return DetectRealRunningStatus(settlementStatus);

                case BetStatus.Reject:
                    return CoreBetList.reject;

                case BetStatus.Won:
                    return DetectRealSettledStatus(settlementStatus, CoreBetList.won);

                case BetStatus.Lose:
                    return DetectRealSettledStatus(settlementStatus, CoreBetList.lose);

                case BetStatus.Draw:
                    return DetectRealSettledStatus(settlementStatus, CoreBetList.draw);

                case BetStatus.Waiting:
                    return CoreBetList.waiting;

                case BetStatus.Void:
                    return CoreBetList.voided;

                case BetStatus.Refund:
                    return CoreBetList.refund;

                case BetStatus.HalfWon:
                    return DetectRealSettledStatus(settlementStatus, CoreBetList.halfwon);

                case BetStatus.HalfLose:
                    return DetectRealSettledStatus(settlementStatus, CoreBetList.halflose);

                default:
                    return string.Empty;
            }
        }

#pragma warning restore S1541 // Methods and properties should not be too complex

        #region PRIVATE METHODS

        private static bool IsCashOutTicket(ITicket ticket)
            => (ticket.BetdaqId == 1 && !IsBetTradeTicket(ticket.BetTypeId) && ticket.IsSportsbook()) ||
            ticket.ExtraInfo.HasAttributeInXmlTag(CashOutXmlTag, "s");

        private static bool IsCashOutOriginalTicket(ITicket ticket)
            => ticket.BetdaqId == 0 && ticket.ExtraInfo.HasAttributeInXmlTag(CashOutXmlTag, "s");

        private static bool IsBetTradeTicket(int betTypeId)
            => betTypeId == BetTypes.Sold || betTypeId == BetTypes.Bought;

        private static bool IsStatusNotVoidAndRefund(string status)
            => !status.Equals(BetStatus.Void) && !status.Equals(BetStatus.Refund);

        private static bool IsCashOutAll(ITicket ticket) => ticket.Stake == 0 && StakeIsZeroBeforeConvertCurrency(ticket);

        private static bool StakeIsZeroBeforeConvertCurrency(ITicket ticket) => ticket.OriginalBetType != -1;

        private static string DetectRealRunningStatus(byte settlementStatus)
        {
            const byte pendingStatus = 1;
            const byte underSettlementStatus = 4;
            const byte systemSettlementStatus = 8;

            if ((settlementStatus & pendingStatus) == pendingStatus)
            {
                return CoreBetList.DelayPayout;
            }

            if ((settlementStatus & underSettlementStatus) == underSettlementStatus
                || (settlementStatus & systemSettlementStatus) == systemSettlementStatus)
            {
                return CoreBetList.UnderSettlement;
            }

            return CoreBetList.running;
        }

        private static string DetectRealSettledStatus(byte settlementStatus, string settledStatus)
        {
            const byte pendingStatus = 1;
            const byte abnormalStatus = 255;

            if ((settlementStatus & abnormalStatus) == abnormalStatus)
            {
                return CoreBetList.UnderSettlement;
            }

            if ((settlementStatus & pendingStatus) == pendingStatus)
            {
                return CoreBetList.DelayPayout;
            }

            return settledStatus;
        }

        private static bool IsSportsbookRefundedTicket(ITicket ticket)
            => ticket.IsRefunded() && ticket.IsSportsbook();

        private static bool IsNewSettledCashOutTicket(ITicket ticket)
        {
            const int newCashoutSinal = -2;

            return ticket.BetdaqId == newCashoutSinal;
        }

        private static bool IsNewCashOutTicket(ITicket ticket)
        {
            var CutOffDate = new DateTime(2022, 3, 16, 0, 0, 0);

            return ticket.TransDate >= CutOffDate && ticket.ExtraInfo.HasAttributeInXmlTag(CashOutXmlTag, "selTime");
        }
        #endregion PRIVATE METHODS
    }
}