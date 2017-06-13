namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Entities;

    public class Status1031 : Status1
    {
        protected override void BuildStatusResult(ITicket ticket)
        {
            base.BuildStatusResult(ticket);

            var betId = ticket.BetId ?? 0;

            Template.StatusResult.betId = betId.ToString();
        }
    }
}