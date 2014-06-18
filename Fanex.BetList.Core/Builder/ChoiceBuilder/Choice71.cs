namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;

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
            this.BuildBetType(ticket, ticketHelper);

            this.Template.Score = null;
            this.Template.Handicap.handicap = null;

            this.UpdateAllMatchMemberToNull();

            this.UpdateAllLeagueMemberToNull();

            this.Template.betTeam = this.GetBetTeam();
            this.Template.betTeamClassName = BLFAVORITE;
        }

        protected virtual string GetBetTeam()
        {
            return CoreBetList.casinoGame;
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper)
        {
            this.Template.BetType.betTypeName = null;
        }
    }
}