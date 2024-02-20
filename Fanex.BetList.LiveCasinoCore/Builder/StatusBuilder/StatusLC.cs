namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Fanex.BetList.Core.Entities;

    public class StatusLC : Status1
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
                var externalLink = $"{ticket.CurrencyId}, '{ticket.MatchCode}', {ticket.CustId}, '{ticket.BetCheck}', '{ticket.SiteId}'";
                Template.StatusResult.betId = externalLink;
            }
        }

        public virtual bool IsHideStatusResult(ITicket ticket)
        {
            return string.IsNullOrEmpty(ticket.BetCheck);
        }
    }
}