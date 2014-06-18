namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;

    /// <summary>
    /// Correct Score.
    /// </summary>
    public class Choice4 : Choice1
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

            string choice = (ticket.BetTeam == "0:5" || ticket.BetTeam == "5:0") ? ticket.BetTeam + " UP" : ticket.BetTeam;

            // "&nbsp" because export format
            this.Template.betTeam = string.Join(null, new string[] { HtmlCharacters.NoneBreakingSpace, choice });

            this.Template.betTeamClassName = BLFAVORITE;
        }
    }
}