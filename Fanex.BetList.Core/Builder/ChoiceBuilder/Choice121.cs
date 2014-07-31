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
        protected override void BuildTicketStatus(ITicket ticket)
        {
            Template.ticketStatus = string.Empty;
        }

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            bool betHomeTeam = ticket.BetTeam != null && ticket.BetTeam.Contains(BetTeamValue.X);
            var betTeam = betHomeTeam ? CoreBetList.lblDraw : Template.Match.awayTeam;

            Template.betTeam = betTeam;
        }

        /// <summary>
        /// Builds the match.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
        {
            string homeName = GetHomeTeamName(ticket, ticketHelper);
            string awayName = ticketHelper.GetTeamNameById(ticket.AwayId);

            Template.Match.homeTeam = homeName;
            Template.Match.home_firstGoal_lastGoal = string.Empty;
            Template.Match.awayTeam = awayName;
            Template.Match.away_firstGoal_lastGoal = string.Empty;

            BuildFGLGLabel(ticket.EventStatus, ref Template.Match.home_firstGoal_lastGoal, ref Template.Match.away_firstGoal_lastGoal);
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = Underdog;
        }
    }
}