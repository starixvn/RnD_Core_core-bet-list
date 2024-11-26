namespace Fanex.BetList.Example.Data
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;

    public interface ITicketsDAO
    {
        IList<ITicket> GetTickets();

        IList<ITicketData> GetTicketData();
    }
}