namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils;
    using Fanex.BetList.Core.Utils.TicketExt;

    public class Status10450 : StatusRNG
    {
        protected override bool IsHideRNGResult(ITicket ticket)
        {
            const int daySupportResultLink = 60;

            return base.IsHideRNGResult(ticket) || ticket.IsTicketHide3rdResultLink(daySupportResultLink);
        }
    }
}