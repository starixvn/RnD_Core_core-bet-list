namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Entities;

    /// <summary>
    ///  Set Handicap.
    /// </summary>
    public class Choice1303 : Choice1
    {
        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper)
        {
            Template.BetType.betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId, ticket.BetId);
        }
    }
}