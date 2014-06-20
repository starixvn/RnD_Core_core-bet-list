namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Entities;

    /// <summary>
    /// P2P Games.
    /// </summary>
    public class Status1000 : Status1
    {
        protected override void BuildStatusResult(ITicket ticket)
        {
            base.BuildStatusResult(ticket);

            // Use sessionId instead of refNo to call API service
            Template.StatusResult.refNo = ticket.BetCheck;
        }
    }
}