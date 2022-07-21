namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Fanex.BetList.Core.Entities;

    public class Status3601 : Status1
    {
        protected override void BuildStatusResult(ITicket ticket)
        {
            base.BuildStatusResult(ticket);
            Template.StatusResult.refNo = $"{ticket.RefNo}";
            Template.StatusResult.betId = $"{ticket.CurrencyId}, '{ticket.RefNo}', {ticket.CustId}, '{ticket.BetCheck}', '{ticket.SiteId}'";
        }
    }
}