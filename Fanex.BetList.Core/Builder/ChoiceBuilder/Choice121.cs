// ---------------------------------------------------------------------------------
// <copyright file="Choice121.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------------
// <history>
//     <change who="Marc.Bui" date="2012.11.16">Create</change>
//     <change who="Marc.Bui" date="2013.01.18">Update ScoreClassName to BLFAVORITE (red) for all bettypes</change>
//     <change who="Marc.Bui" date="2013.02.21">Update showing Neutral</change>
//     <change who="Marc.Bui" date="2013.10.09">Check stylecop & refactor</change>
//     <change who="Marc.Bui" date="2014.01.15">Change betteam classname to UNDERDOG due to specs.</change>
// </history>
// ---------------------------------------------------------------------------------

namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using BetList.Core.App_GlobalResources;
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
            bool betHomeTeam = ticket.BetTeam != null && ticket.BetTeam.Contains("x");

            string homeName = ticketHelper.GetTeamNameById(ticket.HomeId);
            string awayName = ticketHelper.GetTeamNameById(ticket.AwayId);

            this.Template.betTeam = betHomeTeam ? CoreLang.lblDraw : awayName;

            this.Template.betTeamClassName = BLUNDERDOG;

            this.Template.Handicap.handicap = null;

            this.Template.Score.homeScore = ticket.LiveHomeScore.ToString();
            this.Template.Score.awayScore = ticket.LiveAwayScore.ToString();

            // For special cases
            this.Template.Score.Visible = ticket.IsLive;

            this.Template.BetType.betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId);
            this.Template.Match.homeTeam = string.Join(null, new string[] { homeName, ticket.IsNeutral ? " (N) " : string.Empty });
            this.Template.Match.home_firstGoal_lastGoal = string.Empty;
            this.Template.Match.awayTeam = awayName;
            this.Template.Match.away_firstGoal_lastGoal = string.Empty;

            this.CalculateFG_LG(ticket.EventStatus, ref this.Template.Match.home_firstGoal_lastGoal, ref this.Template.Match.away_firstGoal_lastGoal);

            this.Template.League.sportTypeName = ticketHelper.GetSportNameById(ticket.SportTypeId);
            this.Template.League.LeagueName.leagueName = ticketHelper.GetLeagueNameById(ticket.LeagueId);
        }
    }
}