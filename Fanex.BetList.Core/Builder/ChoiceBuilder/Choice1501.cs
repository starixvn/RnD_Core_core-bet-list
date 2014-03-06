// ---------------------------------------------------------------------------------
// <copyright file="Choice1501.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------------
// <history>
//     <change who="Marc.Bui" date="2014.03.06">Create</change>
// </history>
// ---------------------------------------------------------------------------------

namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;
    using System.Globalization;
    using System.Threading;

    public class Choice1501 : Choice1
    {
        /// <summary>
        /// Builds the choice.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <param name="ticketData">The ticket data.</param>
        protected override void BuildChoice(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            // Does not show handicap, match, and score.
            this.Template.Handicap = null;
            this.Template.Match = null;
            this.Template.Score = null;
            string kenoBetTypeID;

            this.Template.betTeam = ticketHelper.GetKenoBetChoiceName(ticket.BetTeam, out kenoBetTypeID);
            this.Template.BetType.betTypeName = ticketHelper.GetKenoBetTypeName(kenoBetTypeID);
            
            this.Template.betTeamClassName = BL_FAVORITE;

            if (this.Template.League != null)
            {
                this.Template.League.sportTypeName = ticketHelper.GetSportNameById(ticket.SportTypeId);
                this.Template.League.LeagueName.leagueName = ticketHelper.GetLeagueNameById(ticket.LeagueId);
            }
        }
    }
}