namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;

    /// <summary>
    /// Set Handicap.
    /// </summary>
    public class Choice1303 : Choice1
    {
        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            string parentBettypeId = ticketHelper.GetParentIdByBetTypeId(ticket.BetTypeId);

            Template.BetType.betTypeName = ticketHelper.GetBetTypeNameById(parentBettypeId);
        }
    }
}