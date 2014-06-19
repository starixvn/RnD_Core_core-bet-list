namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Entities;

    /// <summary>
    /// Match Total Games Odd/Even.
    /// </summary>
    public class Choice1305 : Choice2
    {
        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.BetType.betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId, ticket.BetId);
        }
    }
}