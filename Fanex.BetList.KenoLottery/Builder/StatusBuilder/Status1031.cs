namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Entities;

    public class Status1031 : Status1
    {
        protected override void BuildStatusResult(ITicket ticket)
        {
            base.BuildStatusResult(ticket);
            Template.StatusResult.refNo = ticket.RefNo;
            Template.StatusResult.betId = $"{ticket.BetId ?? 0}, {ticket.CustId}";
        }
    }
}