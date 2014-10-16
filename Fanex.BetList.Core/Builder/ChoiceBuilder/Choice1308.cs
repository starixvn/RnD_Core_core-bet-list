namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Entities;

    /// <summary>
    /// Game Handicap.
    /// </summary>
    public class Choice1308 : Choice1
    {
        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            string parentBettypeId = ticketHelper.GetParentIdByBetTypeId(ticket.BetTypeId);

            Template.BetType.betTypeName = ticketHelper.GetBetTypeNameById(parentBettypeId);
        }
    }
}