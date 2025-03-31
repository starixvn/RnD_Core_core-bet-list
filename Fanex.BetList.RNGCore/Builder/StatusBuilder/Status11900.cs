namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Fanex.BetList.Core.Entities;

    public class Status11900 : StatusRNG
    {
        protected override bool IsHideRNGResult(ITicket ticket)
        => string.IsNullOrEmpty(ticket.BetCheck);
    }
}