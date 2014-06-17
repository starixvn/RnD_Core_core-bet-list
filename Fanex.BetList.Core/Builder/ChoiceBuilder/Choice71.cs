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
            this.Template.BetType.betTypeName = null;
            this.Template.Match.VS = null;
            this.Template.League.sportTypeName = null;
            this.Template.Score = null;
            this.Template.Handicap.handicap = null;

            this.Template.Match.homeTeam = null;
            this.Template.Match.home_firstGoal_lastGoal = null;
            this.Template.Match.awayTeam = null;
            this.Template.Match.away_firstGoal_lastGoal = null;
            this.Template.League.LeagueName.leagueName = null;

            this.Template.betTeam = CoreBetList.casinoGame;
            this.Template.betTeamClassName = BLFAVORITE;                        
        }
    }
}