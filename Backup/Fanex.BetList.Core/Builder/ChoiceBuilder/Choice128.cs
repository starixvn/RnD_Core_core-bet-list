namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;

    /// <summary>
    ///  Choice for bet type 128 HT/FT Odd/Even.
    /// </summary>
    public class Choice128 : Choice16
    {
        /// <summary>
        ///  Set template bet team block by bet team string get from ticket.
        /// </summary>
        /// <param name="ticket"> Tick with bet type 128.</param>
        /// <param name="ticketHelper"> Not use ticket helper parameter.</param>
        /// <param name="ticketData"> Not use ticket data parameter.</param>
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

                default:
                    Template.betTeam = string.Empty;
                    break;
            }
        }
    }
}