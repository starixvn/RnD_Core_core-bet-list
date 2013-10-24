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
    /// SystemParlayData data class.
    /// </summary>
    public class SystemParlayData : ISystemParlayData
    {
        /// <summary>
        /// Gets or sets Ticket List.
        /// </summary>
        /// <value>The ticket list.</value>
        public IList<ISystemParlayTicket> TicketList { get; set; }

        /// <summary>
        /// Gets or sets Serial List.
        /// </summary>
        /// <value>The serial list.</value>
        public IList<ISystemParlaySerial> SerialList { get; set; }

        /// <summary>
        /// Gets or sets Away Id.
        /// </summary>
        /// <value>The outstanding.</value>
        public decimal Outstanding { get; set; }
    }
}
