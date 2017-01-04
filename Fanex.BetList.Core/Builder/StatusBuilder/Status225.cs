namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Entities;

    public class Status225 : Status221
    {
        protected override void BuildStatusResult(ITicket ticket)
        {
            base.BuildStatusResult(ticket);
            Template.StatusResult.betType = "221";
        }
    }
}