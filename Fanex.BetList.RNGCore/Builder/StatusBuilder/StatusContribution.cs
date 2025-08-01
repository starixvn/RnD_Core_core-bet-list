using System.Collections.Generic;
using Fanex.BetList.Core.Entities;

namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    public class StatusContribution : StatusJackpot
    {
        protected override void BuildResult(ITicket ticket, List<ITicketData> ticketData)
            => Template.result = "-";
    }
}