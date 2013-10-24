// ---------------------------------------------------------------------------------
// <copyright file="Choice1103.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------------
// <history>
//     <change who="Marc.Bui" date="2013.10.09">Check stylecop & refactor</change>
// </history>
// ---------------------------------------------------------------------------------

namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;

    /// <summary>
    /// LiveCasino - Live Baccarat.
    /// </summary>
    public class Choice1103 : Choice1101
    {
        protected override void BuildChoice(Entities.ITicket ticket, Entities.ITicketHelper ticketHelper, List<Entities.ITicketData> ticketData)
        {
            base.BuildChoice(ticket, ticketHelper, ticketData);

            this.Template.League.LeagueName.leagueName = string.Join(null, new string[] { CoreLang.LiveBaccarat, "&nbsp;", ticket.ShowTime });
        }
    }
}