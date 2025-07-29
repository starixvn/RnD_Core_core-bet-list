using Fanex.BetList.Core.Entities;

namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    public class Status15400 : StatusRNGExpireDate
    {
        protected override bool IsHideRNGResult(ITicket ticket) => true;
    }
}