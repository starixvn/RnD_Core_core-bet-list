namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Fanex.BetList.Core.Entities;

    public class Status7701 : Status1
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

                Template.StatusResult.betId = $"{ticket.MatchCode}, '{ticket.BetTeam}', {ticket.CustId}, '{ticket.BetCheck}'";
            }
        }

        private static bool IsSummaryTicket(string betCheck)
                => string.IsNullOrEmpty(betCheck);
    }
}