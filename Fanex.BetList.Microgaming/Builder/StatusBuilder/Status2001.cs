namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using System;
    using Entities;

    public class Status2001 : Status1
    {
        protected override void BuildStatusResult(ITicket ticket)
        {
            base.BuildStatusResult(ticket);

            var externalLink = string.Format("{0},'{1}',{2}", ticket.BetId, ticket.BetCheck, ticket.CustId);

            Template.StatusResult.betId = externalLink;
        }
    }
}
