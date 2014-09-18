// ---------------------------------------------------------------------------------
// <copyright file="ITicketsDAO.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------------
// <history>
//     <change who="Marc.Bui" date="2014.02.11">Create</change>
// </history>
// ---------------------------------------------------------------------------------

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