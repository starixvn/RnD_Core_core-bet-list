namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils;

    public class Status3300 : Status1
    {
        protected override void BuildStatusResult(ITicket ticket)
        {
            base.BuildStatusResult(ticket);

            if (ticket.IsSummaryTicket())
            {
                Template.StatusResult.Hide();
            }
            else
            {
                var externalLink = string.Format("{0}, '{1}', {2}, '{3}'", ticket.BetId ?? 0, ticket.BetCheck, ticket.CustId, ticket.TransDesc);
                Template.StatusResult.betId = externalLink;
            }
        }
    }
}
