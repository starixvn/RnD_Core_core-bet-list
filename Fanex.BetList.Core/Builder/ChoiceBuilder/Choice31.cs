namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;

    /// <summary>
    /// Bet Type: Win.
    /// </summary>
    public class Choice31 : Choice1
    {
        /// <summary>
        /// Builds the choice.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <param name="ticketData">The ticket data.</param>
        protected override void BuildChoice(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            base.BuildChoice(ticket, ticketHelper, ticketData);

            this.Template.Match.VS = null;
            this.Template.Handicap.handicap = null;
            this.Template.Match.home_firstGoal_lastGoal = null;
            this.Template.Match.away_firstGoal_lastGoal = null;
            this.Template.Match.awayTeam = null;

            this.Template.betTeam = string.Join(null, new string[] { ticket.MatchCode, " - ", ticketHelper.GetHorseTeamNameById(ticket.HomeId, ticket.AwayId) });

            this.Template.betTeamClassName = Favorite;

            this.Template.Match.homeTeam = string.Join(null, new string[] { CoreBetList.race, HtmlCharacters.NoneBreakingSpace, ticket.Race });
        }
    }
}