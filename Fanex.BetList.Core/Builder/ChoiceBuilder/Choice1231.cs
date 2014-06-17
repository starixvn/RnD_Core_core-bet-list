namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;

    /// <summary>
    /// Virtual Horse Racing - Win.
    /// </summary>
    public class Choice1231 : Choice1
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

            if (this.Template.Match != null)
            {
                this.Template.Match.VS = null;
                this.Template.Match.home_firstGoal_lastGoal = null;
                this.Template.Match.away_firstGoal_lastGoal = null;
                this.Template.Match.awayTeam = null;
            }

            this.Template.Handicap.handicap = null;

            this.Template.League.LeagueName.leagueName = ticketHelper.GetLeagueNameById(ticket.LeagueId);

            this.Template.betTeam = string.Join(null, new string[] { ticket.MatchCode, " - ", ticketHelper.GetHorseTeamNameById(ticket.HomeId, ticket.AwayId) });

            this.Template.betTeamClassName = BLFAVORITE;

            DateTime eventTime;

            if (DateTime.TryParse(ticket.ShowTime, out eventTime))
            {
                this.Template.Match.homeTeam = eventTime.ToString("M/d/yyyy hh:mm");
            }
            else
            {
                this.Template.Match.homeTeam = ticket.ShowTime;
            }
        }

        /// <summary>
        /// Calculates the FG _ LG.
        /// </summary>
        /// <param name="betStatus">The bet status.</param>
        /// <param name="homeFGLG">The home FGLG.</param>
        /// <param name="awayFGLG">The away FGLG.</param>
        protected override void CalculateFG_LG(string betStatus, ref string homeFGLG, ref string awayFGLG)
        {
            // There's no first goal last goal for this bettype
            homeFGLG = string.Empty;
            awayFGLG = string.Empty;
        }

        /// <summary>
        /// Calculates the F g_ L g_ excel.
        /// </summary>
        /// <param name="betStatus">The bet status.</param>
        /// <param name="homeFGLG">The home FGLG.</param>
        /// <param name="awayFGLG">The away FGLG.</param>
        protected override void CalculateFG_LG_Excel(string betStatus, ref string homeFGLG, ref string awayFGLG)
        {
            // There's no first goal last goal for this bettype
            homeFGLG = string.Empty;
            awayFGLG = string.Empty;
        }
    }
}