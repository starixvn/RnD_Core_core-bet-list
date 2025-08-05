using Fanex.BetList.Core.Entities;

namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    public class StatusJackpot : Status1
    {
        protected override void BuildStatusResult(ITicket ticket)
            => Template.StatusResult.Hide();
    }
}