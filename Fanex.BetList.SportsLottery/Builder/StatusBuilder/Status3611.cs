namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Fanex.BetList.Core.Entities;

    public class Status3611 : Status1
    {
        protected override void BuildStatusResult(ITicket ticket)
        {
            base.BuildStatusResult(ticket);

            Template.StatusResult.betId = $"{ticket.BetId ?? 0}, '', {ticket.CustId}, '{ticket.BetCheck}'";
        }
    }
}