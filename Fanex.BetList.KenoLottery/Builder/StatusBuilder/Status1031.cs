namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Fanex.BetList.Core.Entities;

    public class Status1031 : Status1
    {
        protected override void BuildStatusResult(ITicket ticket)
            => Template.StatusResult.Hide();
    }
}