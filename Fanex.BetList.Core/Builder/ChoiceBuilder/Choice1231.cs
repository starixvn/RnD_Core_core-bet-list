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
        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
        {
            if (Template.Match != null)
            {
                Template.Match.VS = null;
                Template.Match.home_firstGoal_lastGoal = null;
                Template.Match.away_firstGoal_lastGoal = null;
                Template.Match.awayTeam = null;

                DateTime eventTime;
                Template.Match.homeTeam = DateTime.TryParse(ticket.ShowTime, out eventTime) ? eventTime.ToString("M/d/yyyy hh:mm") : ticket.ShowTime;
            }
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

        protected override void CalculateFG_LG_Excel(string betStatus, ref string homeFGLG, ref string awayFGLG)
        {
            // There's no first goal last goal for this bettype
            homeFGLG = string.Empty;
            awayFGLG = string.Empty;
        }
    }
}