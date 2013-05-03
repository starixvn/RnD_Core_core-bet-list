// ---------------------------------------------------------------------------------
// <copyright file="ISystemParlaySerial.cs" company="Nexcel Solutions Vietnam">
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
    /// SystemParlaySerial data interface
    /// </summary>
    public interface ISystemParlaySerial
    {
        /// <summary>
        /// Serial number
        /// </summary>
        long Serial { get; set; }

        /// <summary>
        /// Home Id
        /// </summary>
        int HomeId { get; set; }

        /// <summary>
        /// Away Id
        /// </summary>
        int AwayId { get; set; }
    }
}
