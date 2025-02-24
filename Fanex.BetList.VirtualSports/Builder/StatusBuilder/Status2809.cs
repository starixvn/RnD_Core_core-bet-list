namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Entities;

    public class Status2809 : Status1231
    {
        protected override void BuildStatusResult(ITicket ticket)
        {
            base.BuildStatusResult(ticket);

            var betId = ticket.BetId ?? 0;
            Template.StatusResult.betId = betId.ToString();
        }
    }
}
