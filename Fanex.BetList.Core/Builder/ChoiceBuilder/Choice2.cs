namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;

    /// <summary>
    /// Bet Type: Odd/Even.
    /// </summary>
    public class Choice2 : Choice1
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

            Template.Handicap.handicap = null;
            Template.betTeam = ticket.BetTeam == BetTeamValue.H ? CoreBetList.odd : CoreBetList.even;
            Template.betTeamClassName = Template.betTeam == CoreBetList.odd ? Favorite : Underdog;
        }
    }
}