// ---------------------------------------------------------------------------------
// <copyright file="Choice1220.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------------
// <history>
//     <change who="Marc.Bui" date="2013.02.27">Create</change>
// </history>
// ---------------------------------------------------------------------------------

namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.Entities;
    using System.Collections.Generic;

    /// <summary>
    /// Virtual Tennis - PlayerWin
    /// </summary>
    public class Choice1220 : Choice1
    {
        protected override void BuildChoice(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            base.BuildChoice(ticket, ticketHelper, ticketData);

            this.Template.Handicap = null;
            this.Template.betTeamClassName = BL_UNDER_DOG;            
        }
    }
}