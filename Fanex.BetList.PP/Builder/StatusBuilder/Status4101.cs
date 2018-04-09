namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Entities;

    public class Status4101 : Status1
    {
        protected override void BuildStatusResult(ITicket ticket)
        {
            if (string.IsNullOrEmpty(ticket.BetCheck))
            {
                Template.StatusResult.Hide();
            }
            else
            {
                base.BuildStatusResult(ticket);
                Template.StatusResult.betId = $"{ticket.BetId}, '{ticket.BetCheck}', {ticket.CustId}";
            }
        }
    }
}