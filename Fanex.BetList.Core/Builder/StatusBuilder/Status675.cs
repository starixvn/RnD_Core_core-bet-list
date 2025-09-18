namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Entities;
    public class Status675 : Status1
    {
        protected override void BuildStatusResult(ITicket ticket)
        {
            base.BuildStatusResult(ticket);

            var betId = ticket.BetId?.ToString() + ticket.BetTeam;

            Template.StatusResult.betId = betId;
        }
    }
}