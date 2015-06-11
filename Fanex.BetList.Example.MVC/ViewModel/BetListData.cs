// ---------------------------------------------------------------------------------
// <copyright file="BetListData.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------------
// <history>
//     <change who="Marc.Bui" date="2014.02.12">Create</change>
// </history>
// ---------------------------------------------------------------------------------

namespace Fanex.BetList.Example.MVC.ViewModel
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;

    /// <summary>
    /// Class Bet List Data.
    /// </summary>
    public class BetListData
    {
        public IList<ITicket> Tickets
        {
            get;
            set;
        }

        public List<ITicketData> TicketData
        {
            get;
            set;
        }

        public int BetTypeId { get; set; }

        public string Language { get; set; }

        public string Name { get; set; }
    }
}