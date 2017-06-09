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
                Template.StatusResult.betId = $"{ticket.BetId ?? 0}, '{ticket.BetCheck}', {ticket.CustId}";
            }
            else
            {
                Template.StatusResult = null;
            }
        }

        private static bool IsShowStatusResult(ITicket ticket)
            => !IsSummaryTicket(ticket.BetCheck) &&
                 (ticket.Status.Equals(BetStatus.Won, StringComparison.OrdinalIgnoreCase)
                || ticket.Status.Equals(BetStatus.Draw, StringComparison.OrdinalIgnoreCase)
                || ticket.Status.Equals(BetStatus.Lose, StringComparison.OrdinalIgnoreCase));

        private static bool IsSummaryTicket(string betCheck) => string.IsNullOrEmpty(betCheck);
    }
}