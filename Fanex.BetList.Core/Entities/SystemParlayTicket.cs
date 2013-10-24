// ---------------------------------------------------------------------------------
// <copyright file="SystemParlayTicket.cs" company="Nexcel Solutions Vietnam">
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
    /// SystemParlayTicket data class.
    /// </summary>
    public class SystemParlayTicket : ISystemParlayTicket
    {
        /// <summary>
        /// Gets or sets Win Loss Date.
        /// </summary>
        /// <value>The win-loss date.</value>
        public DateTime WinLostDate { get; set; }

        /// <summary>
        /// Gets or sets Transaction Id.
        /// </summary>
        /// <value>The transaction identifier.</value>
        public long TransId { get; set; }

        /// <summary>
        /// Gets or sets Serial number.
        /// </summary>
        /// <value>The serial.</value>
        public long Serial { get; set; }

        /// <summary>
        /// Gets or sets Status Id.
        /// </summary>
        /// <value>The status identifier.</value>
        public int StatusId { get; set; }

        /// <summary>
        /// Gets or sets Status name.
        /// </summary>
        /// <value>The status.</value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets Multiple odds.
        /// </summary>
        /// <value>The multiple odds.</value>
        public decimal MOdds { get; set; }

        /// <summary>
        /// Gets or sets Stake.
        /// </summary>
        /// <value>The stake.</value>
        public decimal Stake { get; set; }

        /// <summary>
        /// Gets or sets Win-Loss.
        /// </summary>
        /// <value>The win-loss value.</value>
        public decimal WinLost { get; set; }

        /// <summary>
        /// Gets or sets Commission.
        /// </summary>
        /// <value>The commission.</value>
        public decimal Comm { get; set; }

        /// <summary>
        /// Gets or sets Player's commission.
        /// </summary>
        /// <value>The player commission.</value>
        public decimal PlayerComm { get; set; }
    }
}
