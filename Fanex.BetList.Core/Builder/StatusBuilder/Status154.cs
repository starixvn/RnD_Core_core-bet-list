namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Entities;

    /// <summary>
    /// Set x Winner.
    /// </summary>
    public class Status154 : Status1
    {
        protected override void BuildStatusResult(ITicket ticket)
        {
            base.BuildStatusResult(ticket);

            var betId = ticket.BetId ?? 0;

            Template.StatusResult.betId = betId.ToString();
        }
    }
}