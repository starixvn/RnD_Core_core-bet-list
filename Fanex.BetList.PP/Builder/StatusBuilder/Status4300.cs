namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Fanex.BetList.Core.Entities;

    public class Status4300 : Status4101
    {
        protected override void BuildStatusResult(ITicket ticket)
        {
            base.BuildStatusResult(ticket);

            if (ticket.Status.ToLowerInvariant().Equals(BetStatus.Won))
            {
                Template.StatusResult.Hide();
            }
        }
    }
}