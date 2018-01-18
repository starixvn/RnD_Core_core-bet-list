namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Entities;

    public class Status1004 : Status1
    {
        protected override void BuildStatusResult(ITicket ticket)
        {
            base.BuildStatusResult(ticket);
            var extraParameters = $"{ticket.BetId}, {ticket.CustId}";

            Template.StatusResult.betId = extraParameters;
            Template.StatusResult.refNo = ticket.RefNo;
        }
    }
}