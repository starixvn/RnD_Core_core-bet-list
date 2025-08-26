using Fanex.BetList.Core.Entities;

namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    public class Status15799 : Status15798
    {
        protected override void BuildStatusResult(ITicket ticket)
            => Template.StatusResult.Hide();
    }
}