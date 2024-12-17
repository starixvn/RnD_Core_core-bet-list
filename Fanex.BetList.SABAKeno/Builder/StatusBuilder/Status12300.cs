namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using System.Globalization;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils;
    using Fanex.BetList.Core.Utils.TicketExt;

    public class Status12300 : StatusRNG
    {
        protected override bool IsHideRNGResult(ITicket ticket)
        {
            const int daySupportResultLink = 10;

            return string.IsNullOrEmpty(ticket.BetCheck) ||
                ticket.IsTicketHide3rdResultLink(daySupportResultLink);
        }
    }
}