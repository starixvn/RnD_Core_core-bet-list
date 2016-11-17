namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using System;
    using Entities;

    public class Status1011 : Status1
    {
        protected override void BuildStatusResult(ITicket ticket)
        {
            if (IsShowStatusResult(ticket))
            {
                base.BuildStatusResult(ticket);
                Template.StatusResult.betId = $"{ticket.BetId}, '{ticket.BetCheck}', {ticket.CustId}";
            }
            else
            {
                Template.StatusResult = null;
            }
        }

        private bool IsShowStatusResult(ITicket ticket)
        {
            return !IsSummaryTicket(ticket.BetCheck) &&
                 (ticket.Status.Equals(BetStatus.Won, StringComparison.OrdinalIgnoreCase)
                || ticket.Status.Equals(BetStatus.Draw, StringComparison.OrdinalIgnoreCase)
                || ticket.Status.Equals(BetStatus.Lose, StringComparison.OrdinalIgnoreCase));
        }

        private bool IsSummaryTicket(string betCheck)
        {
            return string.IsNullOrEmpty(betCheck);
        }
    }
}