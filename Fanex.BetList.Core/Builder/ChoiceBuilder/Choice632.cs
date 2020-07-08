namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Entities;

    /// <summary>
    /// Bet Type: Correct Quarter Odd/Even.
    /// </summary>
    public class Choice632 : Choice1
    {

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var betteams = string.IsNullOrWhiteSpace(ticket.BetTeam) ? new char[0] : ticket.BetTeam.ToLowerInvariant().ToCharArray();
            
            Template.betTeam = string.Join("/", betteams).Replace("o", CoreBetList.odd).Replace("e", CoreBetList.even);
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = Favorite;
        }
    }
}