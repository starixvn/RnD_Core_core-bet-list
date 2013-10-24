// ---------------------------------------------------------------------------------
// <copyright file="ITicketData.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------------
// <history>
//     <change who="Marc.Bui" date="2012.12.27">Create</change>
//     <change who="Marc.Bui" date="2013.01.28">Add SuperPT, MasterPT, AgentPT for Win/Place PTs columns</change>
// </history>
// ---------------------------------------------------------------------------------

namespace Fanex.BetList.Core.Entities
{
    /// <summary>
    /// Reference data for bet types: 
    ///                     1) Horse Racing (33) => Build Odds.
    ///                     2) Live Casino Baccarat (1101) => Build Choice.
    ///                     3) Live Roulette (1102) => Build Choice.
    /// </summary>
    public interface ITicketData
    {
        string RefNo { get; set; }

        int BetTypeId { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// Values: Running, Waiting, Refund, Void, Won, Lose, Draw, Reject.
        /// </summary>
        /// <value>The status.</value>
        string Status { get; set; }

        decimal Odds { get; set; }

        decimal Stake { get; set; }

        string BetTeam { get; set; }
        
        decimal SuperPT { get; set; }

        decimal MasterPT { get; set; }

        decimal AgentPT { get; set; }
    }
}