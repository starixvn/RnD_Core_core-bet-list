namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Entities;

    /// <summary>
    /// Betby - Match Winner
    /// </summary>
    public class Choice13002 : Choice13000
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.betTeam = ticket.BetTeam.ToLowerInvariant() == "a" ?
            ticketHelper.GetTeamName3rd(ticket.AwayId.ToString()) :
            ticketHelper.GetTeamName3rd(ticket.HomeId.ToString());
        }
    }
}