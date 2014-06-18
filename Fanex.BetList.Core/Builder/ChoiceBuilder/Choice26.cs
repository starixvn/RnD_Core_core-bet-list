namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Constants;

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

            this.Template.Handicap.handicap = null;

            if (ticket.BetTeam == BetTeamValue.B)
            {
                this.Template.betTeam = CoreBetList.lblBoth;
            }
            else if (ticket.BetTeam == BetTeamValue.O)
            {
                this.Template.betTeam = CoreBetList.lblOne;
            }
            else if (ticket.BetTeam == BetTeamValue.N)
            {
                this.Template.betTeam = CoreBetList.nogoal;
            }

            this.Template.betTeamClassName = Favorite;
        }
    }
}