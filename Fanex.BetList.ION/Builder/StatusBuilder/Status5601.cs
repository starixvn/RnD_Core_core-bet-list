namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils.TicketExt;

    public class Status5601 : Status1
    {
        protected override void BuildStatusResult(ITicket ticket)
        {
            const int daySupportResultLink = 60;
            if (string.IsNullOrEmpty(ticket.BetCheck) ||
                ticket.IsTicketHide3rdResultLink(daySupportResultLink))
            {
                Template.StatusResult.Hide();
            }
            else
            {
                base.BuildStatusResult(ticket);
                Template.StatusResult.betId = $"{ticket.CurrencyId}, '{ticket.MatchCode}', {ticket.CustId}, '{ticket.BetCheck}', '{ticket.SiteId}'";
            }
        }            
    }
}