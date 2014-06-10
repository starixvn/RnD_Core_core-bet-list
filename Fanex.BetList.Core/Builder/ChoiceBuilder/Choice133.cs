// --------------------------------------------------------------------------
// <copyright file="Choice133.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2014.6.10">Created</change>
// </history>

namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;

    public class Choice133 : Choice1
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

            if (ticket.BetTeam == "y")
            {
                this.Template.betTeam = CoreBetList.lblYes;
            }
            else if (ticket.BetTeam == "n")
            {
                this.Template.betTeam = CoreBetList.lblNo;
            }

            this.Template.Handicap = null;
            this.Template.OddMixParlay = null;
            this.Template.Score = null;
        }
    }
}