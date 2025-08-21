namespace Fanex.BetList.Core.Utils
{
    using Fanex.BetList.Core.Entities;
    using System;

    public static class TransDescParser
    {
        public static bool IsSummaryTicket(this ITicket ticket)
            => string.IsNullOrWhiteSpace(ticket.TransDesc);

        public static bool IsNotRunningTicket(this ITicket ticket)
            => !string.IsNullOrWhiteSpace(ticket.Status)
            && !ticket.Status.Equals(BetStatus.Running, StringComparison.OrdinalIgnoreCase);
    }
}
