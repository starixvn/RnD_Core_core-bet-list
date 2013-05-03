// ---------------------------------------------------------------------------------
// <copyright file="ISystemParlayTicket.cs" company="Nexcel Solutions Vietnam">
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
    /// SystemParlayTicket data interface
    /// </summary>
    public interface ISystemParlayTicket
    {
        /// <summary>
        /// Win Loss Date
        /// </summary>
        DateTime WinLostDate { get; set; }

        /// <summary>
        /// TransId
        /// </summary>
        long TransId { get; set; }

        /// <summary>
        /// Serial number
        /// </summary>
        long Serial { get; set; }

        /// <summary>
        /// Status Id
        /// </summary>
        int StatusId { get; set; }

        /// <summary>
        /// Status name
        /// </summary>
        string Status { get; set; }

        /// <summary>
        /// Multiple odds
        /// </summary>
        decimal MOdds { get; set; }

        /// <summary>
        /// Stake
        /// </summary>
        decimal Stake { get; set; }

        /// <summary>
        /// Win Lost
        /// </summary>
        decimal WinLost { get; set; }

        /// <summary>
        /// Commission
        /// </summary>
        decimal Comm { get; set; }

        /// <summary>
        /// Player commission
        /// </summary>
        decimal PlayerComm { get; set; }
    }
}
