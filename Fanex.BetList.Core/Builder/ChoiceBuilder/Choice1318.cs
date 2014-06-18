namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.Entities;

    /// <summary>
    /// Set x Total Games Odd/Even.
    /// </summary>
    public class Choice1318 : Choice2
    {
        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper)
        {
            this.Template.BetType.betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId, ticket.BetId);
        }
    }
}