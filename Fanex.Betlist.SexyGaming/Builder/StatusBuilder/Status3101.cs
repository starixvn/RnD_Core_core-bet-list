namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Fanex.BetList.Core.Entities;

    public class Status3101 : StatusLC
    {
        protected override void BuildStatusResult(ITicket ticket)
        {
            base.BuildStatusResult(ticket);

            if (string.IsNullOrEmpty(ticket.TransDesc))
            {
                Template.StatusResult.Hide();
            }
            else
            {
                var externalLink = string.Format("{0}, '{1}', {2}, '{3}'", ticket.BetId ?? 0, ticket.MatchCode, ticket.CustId, ticket.TransDesc);

                Template.StatusResult.betId = externalLink;
            }
        }
    }
}