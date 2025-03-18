namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils.TicketExt;

    public class Status15200 : StatusRNG
    {
        protected override bool IsHideRNGResult(ITicket ticket)
        {
            const int daySupportResultLink = 90;

            return string.IsNullOrEmpty(ticket.BetCheck) ||
                ticket.IsTicketHide3rdResultLink(daySupportResultLink);
        }
    }
}