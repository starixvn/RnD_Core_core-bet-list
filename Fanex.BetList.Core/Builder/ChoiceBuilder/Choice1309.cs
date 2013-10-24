// ---------------------------------------------------------------------------------
// <copyright file="Choice1309.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------------
// <history>
//     <change who="Marc.Bui" date="2012.11.16">Create</change>
//     <change who="Marc.Bui" date="2013.01.18">Update ScoreClassName to BLFAVORITE (red) for all bettypes</change>
//     <change who="Marc.Bui" date="2013.02.21">Update showing Neutral</change>
//     <change who="Marc.Bui" date="2013.10.09">Check stylecop & refactor</change>
// </history>
// ---------------------------------------------------------------------------------

namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils;

    /// <summary>
    ///    Match Handicap Games 3-way.
    /// </summary>
    public class Choice1309 : Choice1
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

            string awayName = ticketHelper.GetTeamNameById(ticket.AwayId);

            bool betHome = ticket.BetTeam == "1";
            bool betAway = ticket.BetTeam == "2";
            decimal handicap = ticket.Handicap2 > ticket.Handicap1 ? ticket.Handicap2 : ticket.Handicap1;

            if (betHome)
            {
                this.Template.betTeam = this.Template.Match.homeTeam;
                this.Template.betTeamClassName = ticket.Handicap2 >= ticket.Handicap1 ? BLUNDERDOG : BLFAVORITE;
                this.Template.Handicap.handicap = ConvertByBetType.Hdp((this.Template.betTeamClassName == BLFAVORITE) ? -handicap : handicap);
            }
            else if (betAway)
            {
                this.Template.betTeam = awayName;
                this.Template.betTeamClassName = ticket.Handicap2 <= ticket.Handicap1 ? BLUNDERDOG : BLFAVORITE;
                this.Template.Handicap.handicap = ConvertByBetType.Hdp((this.Template.betTeamClassName == BLFAVORITE) ? -handicap : handicap);
            }
            else
            {
                this.Template.betTeamClassName = BLUNDERDOG;                

                if (ticket.Handicap2 > ticket.Handicap1)
                {
                    this.Template.betTeam = string.Join(null, new string[] { CoreLang.lblDraw, "&nbsp;(", CoreBetList.h, "+", ConvertByBetType.Hdp(ticket.Handicap2) + ")" });
                }
                else
                {
                    this.Template.betTeam = string.Join(null, new string[] { CoreLang.lblDraw, "&nbsp;(", CoreBetList.a, "+", ConvertByBetType.Hdp(ticket.Handicap1), ")" });
                }

                this.Template.Handicap.handicap = null;
            }

            this.Template.BetType.betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId, ticket.BetId);
        }
    }
}