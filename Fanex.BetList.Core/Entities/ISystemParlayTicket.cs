// ---------------------------------------------------------------------------------
// <copyright file="ISystemParlayTicket.cs" company="Nexcel Solutions Vietnam">
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
    /// SystemParlayTicket data interface.
    /// </summary>
    public interface ISystemParlayTicket
    {
        /// <summary>
        /// Gets or sets Win Loss Date.
        /// </summary>
        /// <value>The win lost date.</value>
        DateTime WinLostDate { get; set; }

        /// <summary>
        /// Gets or sets TransId.
        /// </summary>
        /// <value>The trans identifier.</value>
        long TransId { get; set; }

        /// <summary>
        /// Gets or sets Serial number.
        /// </summary>
        /// <value>The serial.</value>
        long Serial { get; set; }

        /// <summary>
        /// Gets or sets Status Id.
        /// </summary>
        /// <value>The status identifier.</value>
        int StatusId { get; set; }

        /// <summary>
        /// Gets or sets Status name.
        /// </summary>
        /// <value>The status.</value>
        string Status { get; set; }

        /// <summary>
        /// Gets or sets Multiple odds.
        /// </summary>
        /// <value>The m odds.</value>
        decimal MOdds { get; set; }

        /// <summary>
        /// Gets or sets Stake.
        /// </summary>
        /// <value>The stake.</value>
        decimal Stake { get; set; }

        /// <summary>
        /// Gets or sets Win-Loss.
        /// </summary>
        /// <value>The win lost.</value>
        decimal WinLost { get; set; }

        /// <summary>
        /// Gets or sets Commission.
        /// </summary>
        /// <value>The commission.</value>
        decimal Comm { get; set; }

        /// <summary>
        /// Gets or sets Player's commission.
        /// </summary>
        /// <value>The player commission.</value>
        decimal PlayerComm { get; set; }
    }
}
