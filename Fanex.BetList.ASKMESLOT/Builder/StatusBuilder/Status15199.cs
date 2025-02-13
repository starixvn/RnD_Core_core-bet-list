namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Fanex.BetList.Core.Entities;

    public class Status15199 : Status1
    {
        protected override void BuildStatusResult(ITicket ticket)
        {
            base.BuildStatusResult(ticket);

            Template.StatusResult.Hide();
        }
    }
}