namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils.TicketExt;

    public class Status7701 : Status1
    {
        protected override void BuildStatusResult(ITicket ticket)
        {
            const int daySupportResultLink = 90;
            if (IsSummaryTicket(ticket.BetCheck) || 
                ticket.IsTicketHide3rdResultLink(daySupportResultLink))
            {
                Template.StatusResult.Hide();
            }
            else
            {
                base.BuildStatusResult(ticket);

                Template.StatusResult.betId = $"{ticket.CurrencyId}, '{ticket.BetTeam}', {ticket.CustId}, '{ticket.BetCheck}', '{ticket.SiteId}'";
            }
        }

        private static bool IsSummaryTicket(string betCheck)
                => string.IsNullOrEmpty(betCheck);
    }
}