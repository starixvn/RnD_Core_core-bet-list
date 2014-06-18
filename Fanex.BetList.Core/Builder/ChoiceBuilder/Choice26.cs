namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;

    /// <summary>
    /// Both/One/Neither team to score.
    /// </summary>
    public class Choice26 : Choice1
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

            if (ticket.BetTeam == BetTeamValue.B)
            {
                Template.betTeam = CoreBetList.lblBoth;
            }
            else if (ticket.BetTeam == BetTeamValue.O)
            {
                Template.betTeam = CoreBetList.lblOne;
            }
            else if (ticket.BetTeam == BetTeamValue.N)
            {
                Template.betTeam = CoreBetList.nogoal;
            }

            Template.betTeamClassName = Favorite;
        }
    }
}