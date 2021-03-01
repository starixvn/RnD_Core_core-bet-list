namespace Fanex.BetList.Core.Utils.TicketExt
{
    using Fanex.BetList.Core.Entities;
    using System;

    public static class ITicketExtensions
    {
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

        public static bool IsSportsbook(this ITicket ticket)
        {
            const int soccer = 1;
            const int otherSports = 99;
            const int horseRacingFixedOdds = 154;

            return (ticket.SportTypeId >= soccer && ticket.SportTypeId <= otherSports)
                || ticket.SportTypeId == horseRacingFixedOdds;
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
    }
}