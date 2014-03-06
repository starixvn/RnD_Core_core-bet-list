// ---------------------------------------------------------------------------------
// <copyright file="Status1501.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------------
// <history>
//     <change who="Marc.Bui" date="2014.03.05">Create</change>
// </history>
// ---------------------------------------------------------------------------------

namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;

    /// <summary>
    /// Keno games.
    /// </summary>
    public class Status1501 : Status1
    {
        public override Status_Template Render(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowVNIP)
        {
            base.Render(ticket, ticketHelper, ticketData, isShowVNIP);
            Template.StatusResult.Hide();            

            return Template;
        }
    }
}