namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;

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
            this.BuildScore(ticket);

            this.BuildBetType(ticket, ticketHelper);

            this.BuildBetAndMatch(ticket, ticketHelper);

            this.BuildLeague(ticket, ticketHelper);
        }

        protected override void BuildBetAndMatch(ITicket ticket, ITicketHelper ticketHelper)
        {
            this.Template.Handicap.handicap = null;

            string homeName = this.GetHomeTeamName(ticket, ticketHelper);
            string awayName = ticketHelper.GetTeamNameById(ticket.AwayId);

            this.Template.betTeam = this.GetBetTeam(ticket, ticketHelper);

            this.Template.betTeamClassName = BLUNDERDOG;
            
            this.Template.Match.homeTeam = homeName;
            this.Template.Match.home_firstGoal_lastGoal = string.Empty;
            this.Template.Match.awayTeam = awayName;
            this.Template.Match.away_firstGoal_lastGoal = string.Empty;

            this.CalculateFG_LG(ticket.EventStatus, ref this.Template.Match.home_firstGoal_lastGoal, ref this.Template.Match.away_firstGoal_lastGoal);
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