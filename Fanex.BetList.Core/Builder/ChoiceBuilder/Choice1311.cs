namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Entities;

    /// <summary>
    ///  Set x Winner.
    /// </summary>
    public class Choice1311 : Choice20
    {
        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper)
        {
            Template.BetType.betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId, ticket.BetId);
        }
    }
}