namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;
    
    /// <summary>
    ///  Choice for bet type 128 HT/FT Odd/Even    
    /// </summary>
    public class Choice128 : Choice16
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            switch (ticket.BetTeam)
            { 
                case "oo":
                    Template.betTeam = CoreBetList.oo;
                    break;
                case "oe":
                    Template.betTeam = CoreBetList.oe;
                    break;
                case "eo":
                    Template.betTeam = CoreBetList.eo;
                    break;
                case "ee":
                    Template.betTeam = CoreBetList.ee;
                    break;
            }            
        }
    }
}