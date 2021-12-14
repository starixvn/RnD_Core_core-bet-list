namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils.TicketExt;

    public class Status5701 : Status1
    {
        protected override void BuildStatusResult(ITicket ticket)
        {
            base.BuildStatusResult(ticket);
            const int daySupportResultLink = 60;

            if (string.IsNullOrEmpty(ticket.TransDesc) ||
                ticket.IsTicketHide3rdResultLink(daySupportResultLink))
            {
                Template.StatusResult.Hide();
            }
            else
            {
                Template.StatusResult.league = ticket.BetTeam;
                Template.StatusResult.betId = $"{ticket.CurrencyId}, '{ticket.MatchCode}', {ticket.CustId}, '{ticket.BetCheck}', '{ticket.SiteId}'";
            }
        }
    }
}