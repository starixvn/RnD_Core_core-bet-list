namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using System.Collections.Generic;
    using Entities;

    /// <summary>
    /// Virtual Horse Racing - Win/Place.
    /// </summary>
    public class Status1233 : Status33
    {
        public Status1233()
        {
            WinBetId = 1231;
            PlaceBetId = 1232;
        }

        protected override List<ITicketData> GetReferenceData(ITicket ticket, List<ITicketData> ticketData)
        {
            return ticketData.FindAll(item => item.RefNo.Equals(ticket.RefNo));
        }
    }
}