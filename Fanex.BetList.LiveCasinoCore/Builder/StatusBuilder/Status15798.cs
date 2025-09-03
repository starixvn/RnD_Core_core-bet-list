using Fanex.BetList.Core.Entities;
using System.Collections.Generic;

namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    /// <summary>Playtech Live with Jackpot Contribution</summary>
    public class Status15798 : Status15799
    {
        protected override void BuildResult(ITicket ticket, List<ITicketData> ticketData)
            => Template.result = "-";

    }
}