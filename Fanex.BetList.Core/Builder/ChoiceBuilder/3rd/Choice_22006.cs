namespace Fanex.BetList.Core.Builder.ChoiceBuilder._3rd
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Entities;

    /// <summary>
    /// Play Tech Casino.
    /// </summary>
    public class Choice_22006 : Choice1
    {
        /// <summary>
        /// Builds the choice.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <param name="ticketData">The ticket data.</param>
        protected override void BuildChoice(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.Handicap = null;
            Template.Match = null;
            Template.League = null;
            Template.Score = null;

            Template.BetType.betTypeName = CoreBetList.playtechcasino;

            Template.betTeam = ticket.BetTeam;
            Template.betTeamClassName = Favorite;
        }
    }
}