namespace Fanex.BetList.Core.Utils.TicketExt
{
    using Fanex.BetList.Core.Entities;
    using System;

    public static class ITicketExtensions
    {
        private const string JackpotInfo = "jp=1";

        public static bool TicketHasJackpotInfo(this ITicket ticket)
            => !string.IsNullOrEmpty(ticket.JackpotInfo) && ticket.JackpotInfo.ToLowerInvariant().Contains(JackpotInfo);

        public static bool IsWon(this ITicket ticket)
        {
            var statusId = ticket.StatusId != null ? ticket.StatusId.ToString() : string.Empty;
            var status = ticket.Status ?? string.Empty;

            status = status.Trim().ToLowerInvariant();

            var isWon = statusId == BetStatusId.Won || status == BetStatus.Won || status == BetStatus.HalfWon;

            return isWon;
        }

        public static bool IsRunningWaiting(this ITicket ticket)
        {
            var statusId = ticket.StatusId != null ? ticket.StatusId.ToString() : string.Empty;
            var status = ticket.Status ?? string.Empty;

            status = status.Trim().ToLowerInvariant();

            var isRunning = statusId == BetStatusId.Running || status == BetStatus.Running;            
            var isWaiting = statusId == BetStatusId.Waiting || status == BetStatus.Waiting;

            return isWaiting || isRunning;
        }

        public static bool IsSettled(this ITicket ticket)
        {
            var statusId = ticket.StatusId != null ? ticket.StatusId.ToString() : string.Empty;
            var status = ticket.Status ?? string.Empty;

            status = status.Trim().ToLowerInvariant();

            var isWon = statusId == BetStatusId.Won || status == BetStatus.Won || status == BetStatus.HalfWon;
            var isLose = statusId == BetStatusId.Lost || status == BetStatus.Lose || status == BetStatus.HalfLose;
            var isDraw = statusId == BetStatusId.Draw || status == BetStatus.Draw;

            return isDraw || isWon || isLose;
        }

        public static bool IsRefunded(this ITicket ticket)
        {
            var statusId = ticket.StatusId != null ? ticket.StatusId.ToString() : string.Empty;
            var status = ticket.Status ?? string.Empty;

            status = status.Trim().ToLowerInvariant();

            return statusId == BetStatusId.Refund || status == BetStatus.Refund;
        }

        public static bool IsDraw(this ITicket ticket)
        {
            var statusId = ticket.StatusId?.ToString();
            var status = ticket.Status?.Trim().ToLowerInvariant();

            return BetStatusId.Draw == statusId || BetStatus.Draw == status;
        }

        public static bool IsSportsbook(this ITicket ticket)
            => IsSportsbook(ticket.SportTypeId);

        public static bool IsSportsbook(this int sportTypeId)
        {
            const int soccer = 1;
            const int otherSports = 99;
            const int horseRacingFixedOdds = 154;

            return (sportTypeId >= soccer && sportTypeId <= otherSports)
                || sportTypeId == horseRacingFixedOdds;
        }

        public static string GetTransactionType(this ITicket ticket)
        {
            if (ticket.WinlostDate.Date >= DateTime.Today.AddDays(-1))
            {
                switch (ticket.Status.ToLowerInvariant())
                {
                    case BetStatus.Won:
                    case BetStatus.Refund:
                    case BetStatus.Lose:
                    case BetStatus.Draw:                    
                        return "1";

                    case BetStatus.Void:
                    case BetStatus.Reject:                    
                        return "2";

                    case BetStatus.Running:
                    case BetStatus.Waiting:
                        return "3";

                    default:
                        return "0";
                }
            }

            return "0";
        }

        public static bool IsTicketHide3rdResultLink(this ITicket ticket, int daySupport3rdResultPage)
        {
            var minDate = DateTime.Today.AddDays(-daySupport3rdResultPage);

            return ticket.WinlostDate < minDate;
        }

        
        public static bool IsTicketHide3rdResultLinkByMonths(this ITicket ticket, int monthsSupport3rdResultPage)
        {
            var today = DateTime.Today;
            var ticketDate = ticket.WinlostDate.Date;
            var firstDayOfMonth = new DateTime(today.Year, today.Month, 1);
            
            var startDate = firstDayOfMonth.AddMonths(-monthsSupport3rdResultPage);
            var endDate = firstDayOfMonth.AddMonths(1).AddDays(-1).Date;

            return ticketDate < startDate || ticketDate > endDate;
        }
    }
}