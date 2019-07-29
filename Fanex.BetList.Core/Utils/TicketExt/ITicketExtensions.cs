namespace Fanex.BetList.Core.Utils.TicketExt
{
    using Fanex.BetList.Core.Entities;

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
    }
}