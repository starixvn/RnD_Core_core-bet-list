namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Entities;

    /// <summary>
    ///    Set x Player to win on a Tiebreak.
    /// </summary>
    public class Choice1315 : Choice1
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
            bool home = ticket.BetTeam == "1";
            bool away = ticket.BetTeam == "2";

            Template.betTeam = home ? Template.Match.homeTeam : away ? Template.Match.awayTeam : CoreBetList.noTiebreak;
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.BetType.betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId, ticket.BetId);
        }
    }
}