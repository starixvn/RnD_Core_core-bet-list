using Fanex.BetList.Core.Entities;
using Fanex.BetList.Core.Utils.TicketExt;

namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    public class StatusRNGExpireDate : StatusRNG
    {
        public virtual int ExpireDateResultLink => 0;

        protected override bool IsHideRNGResult(ITicket ticket)
            => base.IsHideRNGResult(ticket) || ticket.IsTicketHide3rdResultLink(ExpireDateResultLink);
    }
}