namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Fanex.BetList.Core.Entities;

    public class Status2008 : Status2001
    {
        protected override void BuildStatusResult(ITicket ticket)
        {
            if (string.IsNullOrWhiteSpace(ticket.TransDesc))
            {
                Template.StatusResult.Hide();
            }
            else
            {
                base.BuildStatusResult(ticket);
            }
        }
    }
}