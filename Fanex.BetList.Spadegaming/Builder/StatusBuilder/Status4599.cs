namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;

    public class Status4599 : Status4600
    {
        private const string Hyphen = "-";

        protected override void BuildResult(ITicket ticket, List<ITicketData> ticketData)
            => Template.result = Hyphen;
    }
}