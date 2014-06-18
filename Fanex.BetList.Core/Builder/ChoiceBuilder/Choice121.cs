namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;

    /// <summary>
    /// Home no bet.
    /// </summary>
    public class Choice121 : Choice1
    {
        /// <summary>
        /// Builds the choice.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <param name="ticketData">The ticket data.</param>
        protected override void BuildChoice(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            BuildScore(ticket);

            BuildBetType(ticket, ticketHelper);

            BuildBetTeamAndMatch(ticket, ticketHelper);

            BuildBetTeamClassNameAndHandicap(ticket);

            BuildLeague(ticket, ticketHelper);
        }

        protected override void BuildBetTeamAndMatch(ITicket ticket, ITicketHelper ticketHelper)
        {
            string homeName = GetHomeTeamName(ticket, ticketHelper);
            string awayName = ticketHelper.GetTeamNameById(ticket.AwayId);

            Template.betTeam = GetBetTeam(ticket, ticketHelper);

            Template.Match.homeTeam = homeName;
            Template.Match.home_firstGoal_lastGoal = string.Empty;
            Template.Match.awayTeam = awayName;
            Template.Match.away_firstGoal_lastGoal = string.Empty;

            CalculateFG_LG(ticket.EventStatus, ref Template.Match.home_firstGoal_lastGoal, ref Template.Match.away_firstGoal_lastGoal);
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = Underdog;
        }

        protected virtual string GetBetTeam(ITicket ticket, ITicketHelper ticketHelper)
        {
            string awayName = ticketHelper.GetTeamNameById(ticket.AwayId);
            bool betHomeTeam = ticket.BetTeam != null && ticket.BetTeam.Contains(BetTeamValue.X);
            var betTeam = betHomeTeam ? CoreLang.lblDraw : awayName;

            return betTeam;
        }
    }
}