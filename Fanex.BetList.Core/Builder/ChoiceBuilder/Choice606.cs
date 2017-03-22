namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Entities;

    /// <summary>
    /// BAIO - 1H Race to X Points
    /// </summary>
    public class Choice606 : Choice603
    {
        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.BetType.betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId, ticket.RefNo, ticket.BetId, ticket.BetCheck);
        }
    }
}