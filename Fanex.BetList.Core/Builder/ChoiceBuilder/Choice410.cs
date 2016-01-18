namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{

    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;

    /// <summary>
    /// Pool House All In One: 1st Double Chance, bet type ID is 410.
    /// Its parent bet type is 151.
    /// </summary>
    public class Choice410 : Choice24
    {
        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var parentBetTypeId = ticketHelper.GetParentIdByBetTypeId(ticket.BetTypeId);

            Template.BetType.betTypeName = ticketHelper.GetBetTypeNameById(parentBetTypeId);
        }
    }
}