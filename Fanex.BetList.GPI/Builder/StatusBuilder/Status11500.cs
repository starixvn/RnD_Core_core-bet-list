namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils;
    using Fanex.BetList.Core.Utils.TicketExt;

    public class Status11500 : StatusLC
    {
        protected override void BuildStatusResult(ITicket ticket)
        {
            if (IsHideStatusResult(ticket))
            {
                Template.StatusResult.Hide();
            }
            else
            {
                base.BuildStatusResult(ticket);
                var transDate3rd = Formatter.FullDateFormat(ticket.EventDate.Value);

                var externalLink = $"{ticket.CurrencyId}, '{ticket.MatchCode}', {ticket.CustId}, '{ticket.BetCheck}', '{ticket.SiteId}'";
                Template.StatusResult.winlostDate = transDate3rd;
                Template.StatusResult.betId = externalLink;
            }
       }

        public override bool IsHideStatusResult(ITicket ticket)
        {
            const int daySupportResultLink = 15;

            return base.IsHideStatusResult(ticket) || ticket.IsTicketHide3rdResultLink(daySupportResultLink);
        }
    }
}