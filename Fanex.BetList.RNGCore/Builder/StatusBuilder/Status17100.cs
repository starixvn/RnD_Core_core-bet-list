namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils.TicketExt;

    public class Status17100 : StatusRNGExpireDate
    {
        public override int ExpireDateResultLinkByMonths => 2;

        protected override bool IsHideRNGResult(ITicket ticket)
            => string.IsNullOrEmpty(ticket.BetCheck) || ticket.IsTicketHide3rdResultLinkByMonths(ExpireDateResultLinkByMonths);
    }
}