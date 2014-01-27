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
    /// LiveCasino - Live Baccarat
    /// </summary>
    public class Choice1103: Choice1101
    {
        protected override void BuildChoice(Entities.ITicket ticket, Entities.ITicketHelper ticketHelper, List<Entities.ITicketData> ticketData)
        {
            base.BuildChoice(ticket, ticketHelper, ticketData);

            this.Template.League.LeagueName.leagueName = string.Join(null, new string[] { CoreLang.LiveBaccarat, "&nbsp;", ticket.ShowTime });    
        }
    }
}