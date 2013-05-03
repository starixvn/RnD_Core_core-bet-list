// ---------------------------------------------------------------------------------
// <copyright file="SystemParlayTicket.cs" company="Nexcel Solutions Vietnam">
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
    /// SystemParlayTicket data class
    /// </summary>
    public class SystemParlayTicket : ISystemParlayTicket
    {
        /// <summary>
        /// Win Loss Date
        /// </summary>
        public DateTime WinLostDate { get; set; }

        /// <summary>
        /// TransId
        /// </summary>
        public long TransId { get; set; }

        /// <summary>
        /// Serial number
        /// </summary>
        public long Serial { get; set; }

        /// <summary>
        /// Status Id
        /// </summary>
        public int StatusId { get; set; }

        /// <summary>
        /// Status name
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Multiple odds
        /// </summary>
        public decimal MOdds { get; set; }

        /// <summary>
        /// Stake
        /// </summary>
        public decimal Stake { get; set; }

        /// <summary>
        /// Win Lost
        /// </summary>
        public decimal WinLost { get; set; }

        /// <summary>
        /// Commission
        /// </summary>
        public decimal Comm { get; set; }

        /// <summary>
        /// Player commission
        /// </summary>
        public decimal PlayerComm { get; set; }
    }
}
