namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using Entities;

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

            if (Template.Match != null)
            {
                Template.Match.VS = null;
                Template.Match.home_firstGoal_lastGoal = null;
                Template.Match.away_firstGoal_lastGoal = null;
                Template.Match.awayTeam = null;
            }

            Template.Handicap.handicap = null;

            Template.League.LeagueName.leagueName = ticketHelper.GetLeagueNameById(ticket.LeagueId);

            Template.betTeam = string.Join(null, new string[] { ticket.MatchCode, " - ", ticketHelper.GetHorseTeamNameById(ticket.HomeId, ticket.AwayId) });

            Template.betTeamClassName = Favorite;

            DateTime eventTime;

            if (DateTime.TryParse(ticket.ShowTime, out eventTime))
            {
                Template.Match.homeTeam = eventTime.ToString("M/d/yyyy hh:mm");
            }
            else
            {
                Template.Match.homeTeam = ticket.ShowTime;
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