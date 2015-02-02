namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;

    /// <summary>
    /// Bet Type: First Home Team Odd/Even - 136.
    /// </summary>
    public class Choice136 : Choice2
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.betTeam = (ticket.BetTeam.ToLowerInvariant() == BetTeamValue.O) ? CoreBetList.odd : CoreBetList.even;
        }
    }
}