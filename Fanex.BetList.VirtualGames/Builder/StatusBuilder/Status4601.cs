namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Fanex.BetList.Core.Entities;

    public class Status4601 : Status1
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
                Template.StatusResult.betId = $"{ticket.CurrencyId}, '{ticket.BetCheck}', {ticket.CustId}, '{ticket.RefNo}', '{ticket.SiteId}'";
            }
        }
    }
}