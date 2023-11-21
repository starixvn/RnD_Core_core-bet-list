namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Fanex.BetList.Core.Entities;

    public class StatusLC : Status1
    {
        protected override void BuildStatusResult(ITicket ticket)
        {
            base.BuildStatusResult(ticket);

            if (IsHideStatusResult(ticket))
            {
                Template.StatusResult.Hide();
            }
            else
            {
                var externalLink = string.Format("{0}, '{1}', {2}, '{3}'", ticket.BetId ?? 0, ticket.MatchCode, ticket.CustId, ticket.TransDesc);

                Template.StatusResult.betId = externalLink;
            }
        }

        public virtual bool IsHideStatusResult(ITicket ticket){
            return string.IsNullOrEmpty(ticket.TransDesc);
        }
    }
}