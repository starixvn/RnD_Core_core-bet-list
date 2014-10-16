namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;

    /// <summary>
    /// Match Total Games Odd/Even.
    /// </summary>
    public class Choice1305 : Choice2
    {
        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            string parentBettypeId = ticketHelper.GetParentIdByBetTypeId(ticket.BetTypeId);

            Template.BetType.betTypeName = ticketHelper.GetBetTypeNameById(parentBettypeId);
        }
    }
}