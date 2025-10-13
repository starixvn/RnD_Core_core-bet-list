namespace Fanex.BetList.Core.Utils
{
    using Fanex.BetList.Core.Entities;

    public static class TransDescParser
    {   
        public static bool IsSummaryTicket(this ITicket ticket)
            => string.IsNullOrWhiteSpace(ticket.BetCheck);
    }
}