namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using System;
    using Fanex.BetList.Core.Entities;

    public class Status1612 : Status1
    {
        private const int dayAgoSupportResultPage = -10;

        protected override void BuildStatusResult(ITicket ticket)
        {
            if (ShowResultLink(ticket))
            {
                base.BuildStatusResult(ticket);
                Template.StatusResult.refNo = $"'{ticket.BetCheck}'";
                Template.StatusResult.betId = $"{ticket.BetId ?? 0}, '{ticket.MatchCode}', {ticket.CustId}, '{ticket.BetCheck}'";                
            }
            else
            {
                Template.StatusResult.Hide();
            }
        }

        private static bool ShowResultLink(ITicket ticket)
        {
            var minDate = DateTime.Today.AddDays(dayAgoSupportResultPage);

            return ticket.WinlostDate >= minDate && !string.IsNullOrWhiteSpace(ticket.BetCheck);
        }
    }
}