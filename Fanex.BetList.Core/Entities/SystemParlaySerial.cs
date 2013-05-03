// ---------------------------------------------------------------------------------
// <copyright file="SystemParlaySerial.cs" company="Nexcel Solutions Vietnam">
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

    /// <summary>
    /// SystemParlaySerial data class
    /// </summary>
    public class SystemParlaySerial : ISystemParlaySerial
    {
        /// <summary>
        /// Serial number
        /// </summary>
        public long Serial { get; set; }

        /// <summary>
        /// Home Id
        /// </summary>
        public int HomeId { get; set; }

        /// <summary>
        /// Away Id
        /// </summary>
        public int AwayId { get; set; }
    }
}
