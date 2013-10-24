// ---------------------------------------------------------------------------------
// <copyright file="ISystemParlaySerial.cs" company="Nexcel Solutions Vietnam">
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
    /// SystemParlaySerial data interface.
    /// </summary>
    public interface ISystemParlaySerial
    {
        /// <summary>
        /// Gets or sets Serial number.
        /// </summary>
        /// <value>The serial.</value>
        long Serial { get; set; }

        /// <summary>
        /// Gets or sets Home Id.
        /// </summary>
        /// <value>The home team identifier.</value>
        int HomeId { get; set; }

        /// <summary>
        /// Gets or sets Away Id.
        /// </summary>
        /// <value>The away team identifier.</value>
        int AwayId { get; set; }
    }
}
