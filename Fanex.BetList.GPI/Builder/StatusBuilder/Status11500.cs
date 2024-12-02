namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using System;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils.TicketExt;

    public class Status11500 : StatusLC
    {
        public override bool IsHideStatusResult(ITicket ticket)
        {
            const int daySupportResultLink = 45;

            return base.IsHideStatusResult(ticket) || ticket.IsTicketHide3rdResultLink(daySupportResultLink);
        }
    }
}