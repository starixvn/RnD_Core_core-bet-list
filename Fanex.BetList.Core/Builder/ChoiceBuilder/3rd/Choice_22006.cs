namespace Fanex.BetList.Core.Builder.ChoiceBuilder._3rd
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;

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
            this.Template.Handicap = null;
            this.Template.Match = null;
            this.Template.League = null;
            this.Template.Score = null;

            this.Template.BetType.betTypeName = App_GlobalResources.CoreBetList.playtechcasino;

            this.Template.betTeam = ticket.BetTeam;
            this.Template.betTeamClassName = BLFAVORITE;
        }
    }
}