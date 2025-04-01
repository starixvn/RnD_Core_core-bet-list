namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Fanex.BetList.Core.Entities;

    public class StatusRNG : Status1
    {
        protected override void BuildStatusResult(ITicket ticket)
        {
            if (IsHideRNGResult(ticket))
            {
                Template.StatusResult.Hide();
            }
            else
            {
                base.BuildStatusResult(ticket);
                Template.StatusResult.betId = $"{ticket.CurrencyId}, '{ticket.BetTeam}', {ticket.CustId}, '{ticket.BetCheck}', '{ticket.SiteId}'";
            }
        }

        protected virtual bool IsHideRNGResult(ITicket ticket)
          => string.IsNullOrEmpty(ticket.BetCheck);
    }
}