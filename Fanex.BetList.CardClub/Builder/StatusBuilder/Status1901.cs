namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils.TicketExt;

    /// <summary>
    /// Video Poker
    /// </summary>
    public class Status1901 : Status1
    {
        protected override void BuildStatusResult(ITicket ticket)
        {
            const int daySupportResultLink = 90;
            if (string.IsNullOrWhiteSpace(ticket.BetCheck) ||
                ticket.IsTicketHide3rdResultLink(daySupportResultLink))
            {
                Template.StatusResult.Hide();
            }
            else
            {
                base.BuildStatusResult(ticket);
                Template.StatusResult.refNo = $"{ticket.BetCheck}";
                Template.StatusResult.betId = $"{ticket.CurrencyId}, '{ticket.BetTeam}', {ticket.CustId}, '{ticket.BetCheck}', '{ticket.SiteId}'";
            }
        }
    }
}