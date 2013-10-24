// ---------------------------------------------------------------------------------
// <copyright file="Choice123.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------------
// <history>
//     <change who="Marc.Bui" date="2012.11.16">Create</change>
//     <change who="Marc.Bui" date="2013.03.11">Update condition to work with Template changes made by Steven</change>
// </history>
// ---------------------------------------------------------------------------------

namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;

    /// <summary>
    /// Draw no draw.
    /// </summary>
    public class Choice123 : Choice1
    {
        /// <summary>
        /// Builds the choice.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <param name="ticketData">The ticket data.</param>
        protected override void BuildChoice(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            this.Template.Handicap.handicap = null;
            bool betHomeTeam = ticket.BetTeam == "h";

            string homeName = ticketHelper.GetTeamNameById(ticket.HomeId);
            string awayName = ticketHelper.GetTeamNameById(ticket.AwayId);

            this.Template.betTeam = betHomeTeam ? CoreLang.lblDraw : CoreLang.lblNoDraw;
            this.Template.betTeamClassName = BLUNDERDOG;            

            this.Template.Score.homeScore = ticket.LiveHomeScore.ToString();
            this.Template.Score.awayScore = ticket.LiveAwayScore.ToString();

            // For special cases
            this.Template.Score.Visible = ticket.IsLive;

            this.Template.BetType.betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId);

            this.Template.Match.homeTeam = homeName + (ticket.IsNeutral ? " (N)" : string.Empty);
            this.Template.Match.home_firstGoal_lastGoal = string.Empty;
            this.Template.Match.awayTeam = awayName;
            this.Template.Match.away_firstGoal_lastGoal = string.Empty;

            this.CalculateFG_LG(ticket.EventStatus, ref this.Template.Match.home_firstGoal_lastGoal, ref this.Template.Match.away_firstGoal_lastGoal);

            this.Template.League.sportTypeName = ticketHelper.GetSportNameById(ticket.SportTypeId);
            this.Template.League.LeagueName.leagueName = ticketHelper.GetLeagueNameById(ticket.LeagueId);
        }
    }
}