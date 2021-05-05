namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;

    /// <summary>
    /// Jackpot Contribution
    /// </summary>
    public class Status3898 : Status1
    {
        private const string Hyphen = "-";

        protected override void BuildStatusResult(ITicket ticket)
            => Template.StatusResult.Hide();

        protected override void BuildResult(ITicket ticket, List<ITicketData> ticketData)
            => Template.result = Hyphen;
    }
}