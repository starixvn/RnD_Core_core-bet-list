namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;

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

            this.Template.Handicap.handicap = null;
            bool home = ticket.BetTeam == "1";
            bool away = ticket.BetTeam == "2";

            this.Template.betTeam = home ? this.Template.Match.homeTeam : away ? this.Template.Match.awayTeam : CoreBetList.noTiebreak;
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper)
        {
            this.Template.BetType.betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId, ticket.BetId);
        }
    }
}