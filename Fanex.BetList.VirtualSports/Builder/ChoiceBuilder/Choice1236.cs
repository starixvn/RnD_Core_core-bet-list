namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Entities;

    /// <summary>
    /// Virtual Tennis - Total Points.
    /// </summary>
    public class Choice1236 : Choice1220
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.betTeam = string.Join(null, new[] { ticket.BetTeam, " ", CoreBetList.points });
        }
    }
}