// ---------------------------------------------------------------------------------
// <copyright file="SystemParlaySerial.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------------
// <history>
//     <change who="Marc.Bui" date="2013.10.24">Apply stylecop</change>
//     <change who="Jacob.Phan" date="2012.11.16">Create</change>
// </history>
// ---------------------------------------------------------------------------------

namespace Fanex.BetList.Core.Entities
{
    using System;

    /// <summary>
    /// SystemParlaySerial data class.
    /// </summary>
    public class SystemParlaySerial : ISystemParlaySerial
    {
        /// <summary>
        /// Gets or sets Serial number.
        /// </summary>
        /// <value>The serial.</value>
        public long Serial { get; set; }

        /// <summary>
        /// Gets or sets Home Id.
        /// </summary>
        /// <value>The home team identifier.</value>
        public int HomeId { get; set; }

        /// <summary>
        /// Gets or sets Away Id.
        /// </summary>
        /// <value>The away team identifier.</value>
        public int AwayId { get; set; }
    }
}
