// ---------------------------------------------------------------------------------
// <copyright file="Choice1236.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------------
// <history>
//     <change who="Marc.Bui" date="2013.02.28">Create</change>
// </history>
// ---------------------------------------------------------------------------------

namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.Entities;
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;

    /// <summary>
    /// Virtual Tennis - TotalPoints
    /// </summary>
    public class Choice1236 : Choice1220
    {
        protected override void BuildChoice(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            base.BuildChoice(ticket, ticketHelper, ticketData);

            this.Template.betTeam = string.Join(null, new string[] { ticket.BetTeam, " ", CoreBetList.points});
        }
    }
}