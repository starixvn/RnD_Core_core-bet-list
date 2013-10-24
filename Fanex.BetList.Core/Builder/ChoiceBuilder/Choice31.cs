// ---------------------------------------------------------------------------------
// <copyright file="Choice31.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------------
// <history>
//     <change who="Marc.Bui" date="2012.11.16">Create</change>
//     <change who="Marc.Bui" date="2013.03.11">Update condition to work with Template changes made by Steven</change>
//     <change who="Marc.Bui" date="2013.10.09">Check stylecop & refactor</change>
// </history>
// ---------------------------------------------------------------------------------

namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
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

            this.Template.betTeamClassName = BLFAVORITE;

            this.Template.Match.homeTeam = string.Join(null, new string[] { CoreBetList.race, "&nbsp;", ticket.Race });
        }
    }
}