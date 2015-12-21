namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Entities;

    public class Status1003 : Status1
    {
        protected override void BuildStatusResult(ITicket ticket)
        {
            base.BuildStatusResult(ticket);

            Template.StatusResult.Hide();
        }
    }
}
