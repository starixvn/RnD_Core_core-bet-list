namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Entities;

    public class Status221: Status1
    {
        protected override void BuildStatusResult(ITicket ticket)
        {
            base.BuildStatusResult(ticket);
            Template.StatusResult.betId = ticket.Handicap1.ToString();
        }
    }
}