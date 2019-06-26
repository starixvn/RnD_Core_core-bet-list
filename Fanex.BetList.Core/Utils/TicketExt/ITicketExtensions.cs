namespace Fanex.BetList.Core.Utils.TicketExt
{
    using Fanex.BetList.Core.Entities;

    public static class ITicketExtensions
    {
        public static bool IsSettled(this ITicket ticket)
        {
            string statusId = ticket.StatusId != null ? ticket.StatusId.ToString() : string.Empty;
            string status = ticket.Status ?? string.Empty;

            status = status.Trim().ToLowerInvariant();

            bool isWin = statusId == BetStatusId.Won || status == BetStatus.Won;
            bool isLoss = statusId == BetStatusId.Lost || status == BetStatus.Lose;
            bool isDraw = statusId == BetStatusId.Draw || status == BetStatus.Draw;

            return isDraw || isWin || isLoss;
        }
    }
}