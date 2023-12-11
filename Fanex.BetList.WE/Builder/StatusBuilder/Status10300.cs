namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using System;
    using Fanex.BetList.Core.Entities;

    public class Status10300 : StatusLC
    {
        private const int SupportedResultDays = -30;

        public override bool IsHideStatusResult(ITicket ticket)
        => base.IsHideStatusResult(ticket) || !IsSupportViewResultLink(ticket);

        private static bool IsSupportViewResultLink(ITicket ticket)
        {
            var minDate = DateTime.Today.AddDays(SupportedResultDays);

            return ticket.WinlostDate >= minDate;
        }
    }
}