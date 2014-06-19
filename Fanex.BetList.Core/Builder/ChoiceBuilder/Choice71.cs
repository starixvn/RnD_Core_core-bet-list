namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Entities;

    /// <summary>
    /// Casino Games.
    /// </summary>
    public class Choice71 : Choice1
    {
        /// <summary>
        /// Builds the choice.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <param name="ticketData">The ticket data.</param>
        protected override void BuildChoice(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            BuildBetType(ticket, ticketHelper, ticketData);

            Template.Score = null;
            Template.Handicap.handicap = null;

            UpdateAllMatchMemberToNull();

            UpdateAllLeagueMemberToNull();

            Template.betTeam = GetBetTeam();
            Template.betTeamClassName = Favorite;
        }

        protected virtual string GetBetTeam()
        {
            return CoreBetList.casinoGame;
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.BetType.betTypeName = null;
        }
    }
}