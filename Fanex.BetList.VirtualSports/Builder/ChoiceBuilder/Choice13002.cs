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
            var betTeam = ticket.BetTeam.ToLowerInvariant();
            switch (betTeam)
            {
                case "a":
                    betTeam = ticketHelper.GetTeamName3rd(ticket.AwayId.ToString());
                    break;
                default:
                    betTeam = ticketHelper.GetTeamName3rd(ticket.HomeId.ToString());
                    break;
            }
            
            Template.betTeam = betTeam;
        }
    }
}