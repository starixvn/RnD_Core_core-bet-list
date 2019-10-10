namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;

    /// <summary>
    /// Badminton AIO: Match Point Odd/Even
    /// </summary>
    public class Choice706 : Choice2
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
            => Template.betTeam = (ticket.BetTeam.ToLowerInvariant() == BetTeamValue.O) ? CoreBetList.odd : CoreBetList.even;
    }
}