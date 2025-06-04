using Fanex.BetList.Core.Entities;

namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    public class Status16200 : StatusRNGExpireDate
    {
        protected override bool IsHideRNGResult(ITicket ticket)
        {
            return string.IsNullOrEmpty(ticket.BetCheck);
        }
    }
} 