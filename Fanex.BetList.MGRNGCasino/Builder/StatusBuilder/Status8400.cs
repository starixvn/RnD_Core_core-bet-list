namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Fanex.BetList.Core.Entities;

    public class Status8400 : Status1
    {
        protected override void BuildStatusResult(ITicket ticket)
        {
            if (IsSummaryTicket(ticket.BetCheck))
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
