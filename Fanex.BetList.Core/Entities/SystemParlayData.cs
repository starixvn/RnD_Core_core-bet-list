// ---------------------------------------------------------------------------------
// <copyright file="SystemParlayData.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------------
// <history>
//     <change who="Jacob.Phan" date="2012.11.16">Create</change>
// </history>
// ---------------------------------------------------------------------------------

namespace Fanex.BetList.Core.Entities
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// SystemParlayData data class
    /// </summary>
    public class SystemParlayData : ISystemParlayData
    {
        /// <summary>
        /// Ticket List
        /// </summary>
        public IList<ISystemParlayTicket> TicketList { get; set; }

        /// <summary>
        /// Ticket List
        /// </summary>
        public IList<ISystemParlaySerial> SerialList { get; set; }

        /// <summary>
        /// Away Id
        /// </summary>
        public decimal Outstanding { get; set; }
    }
}
