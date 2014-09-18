namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;

    /// <summary>
    ///  Choice for bet type 131 Home Team Total Goal.
    /// </summary>
    public class Choice131 : Choice6
    {
        /// <summary>
        /// Set resource bet team.
        /// </summary>
        /// <param name="ticket"> Ticket with bet type 129.</param>
        /// <param name="ticketHelper"> Not null ticket helper.</param>
        /// <param name="ticketData"> A valid ticket data.</param>
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var betTeam = ticket.BetTeam.Equals("4&over", System.StringComparison.OrdinalIgnoreCase) ? CoreBetList.FourAndOver : ticket.BetTeam;

            Template.betTeam = string.Join(null, new string[] { HtmlCharacters.NoneBreakingSpace, betTeam });
        }
    }
}