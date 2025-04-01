namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils.TicketExt;

    public class Status13900 : StatusLC
    {
        public override bool IsHideStatusResult(ITicket ticket)
        {
            const int daySupportResultLink = 28;

            return base.IsHideStatusResult(ticket) || ticket.IsTicketHide3rdResultLink(daySupportResultLink);
        }
    }
}