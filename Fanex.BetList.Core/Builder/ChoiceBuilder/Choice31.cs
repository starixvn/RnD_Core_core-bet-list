namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;

    /// <summary>
    /// Bet Type: Win.
    /// </summary>
    public class Choice31 : Choice1
    {
        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
        {
            Template.Match.VS = null;
            Template.Match.home_firstGoal_lastGoal = null;
            Template.Match.away_firstGoal_lastGoal = null;
            Template.Match.awayTeam = null;
            Template.Match.homeTeam = string.Join(null, new string[] { CoreBetList.race, HtmlCharacters.NoneBreakingSpace, ticket.Race });
        }

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.betTeam = string.Join(null, new string[] { ticket.MatchCode, " - ", ticketHelper.GetHorseTeamNameById(ticket.HomeId, ticket.AwayId) });
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = Favorite;
        }
    }
}