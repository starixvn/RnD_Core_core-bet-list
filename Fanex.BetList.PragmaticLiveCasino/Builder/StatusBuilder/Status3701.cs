namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils.TicketExt;

    public class Status3701 : StatusLC
    {
        public override bool IsHideStatusResult(ITicket ticket)
        {            
            const int daySupportResultLink = 30;

            return string.IsNullOrEmpty(ticket.BetCheck) ||
                ticket.IsTicketHide3rdResultLink(daySupportResultLink);
        }
    }
}