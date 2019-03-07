namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Fanex.BetList.Core.Entities;

    public class Status4300 : Status1
    {
        protected override void BuildStatusResult(ITicket ticket)
        {
            base.BuildStatusResult(ticket);

            var status = ticket.Status.ToLowerInvariant();
            var ticketIsSettled = status.Equals(BetStatus.Won) || status.Equals(BetStatus.Lose) || status.Equals(BetStatus.Draw);

            if (ticketIsSettled)
            {
                Template.StatusResult.Hide();
            }
        }
    }
}