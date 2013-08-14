// ---------------------------------------------------------------------------------
// <copyright file="Choice1104.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------------
// <history>
//     <change who="Faisall.Tran" date="2013.6.18">Create</change>
// </history>
// ---------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Fanex.BetList.Core.App_GlobalResources;
using Fanex.BetList.Core.Entities;
using Fanex.BetList.Core.Utils;

namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    /// <summary>
    /// LiveCasino - LiveRoullet
    /// </summary>
    public class Choice1104 : Choice1102
    {
        protected override void BuildChoice(Entities.ITicket ticket, Entities.ITicketHelper ticketHelper, List<Entities.ITicketData> ticketData)
        {
            base.BuildChoice(ticket, ticketHelper, ticketData);

            this.Template.League.LeagueName.leagueName = string.Join(null,
                                                                     new string[] { CoreLang.LiveRoulette, "&nbsp;", ticket.ShowTime });
        }

    }
}