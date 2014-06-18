namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Constants;
    using Entities;

    /// <summary>
    /// Match Correct Score.
    /// </summary>
    public class Choice1302 : Choice1
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

            string betTeam = string.Empty;
            if (ticket.BetTeam.Length == 2)
            {
                betTeam = string.Join(null, new string[] { ticket.BetTeam.Substring(0, 1), ":", ticket.BetTeam.Substring(1, 1) });
            }

            // "&nbsp" for the export format
            Template.betTeam = string.Join(null, new string[] { HtmlCharacters.NoneBreakingSpace, betTeam });
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.betTeamClassName = Underdog;
            Template.Handicap.handicap = null;
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper)
        {
            Template.BetType.betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId, ticket.BetId);
        }
    }
}