namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils.TicketExt;

    public class Status5100 : StatusRNG
    {        protected override void BuildStatusResult(ITicket ticket)
        {
            if (IsHideRNGResult(ticket))
            {
                Template.StatusResult.Hide();
            }
            else
            {
                base.BuildStatusResult(ticket);
                Template.StatusResult.betId = $"{ticket.CurrencyId}, '{ticket.BetTeam}', {ticket.CustId}, '{ticket.BetCheck}', '{ticket.SiteId}', '{ticket.TransDesc}'";
            }
        }

        protected override bool IsHideRNGResult(ITicket ticket)
        {
            const int daySupportResultLink = 30;

            return string.IsNullOrEmpty(ticket.BetCheck) ||
                ticket.IsTicketHide3rdResultLink(daySupportResultLink);
        }
    }
}
