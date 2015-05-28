// ---------------------------------------------------------------------------------
// <copyright file="TicketDataTemplateColumnIndex.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------------
// <history>
//     <change who="Marc.Bui" date="2014.04.28">Create</change>
// </history>
// ---------------------------------------------------------------------------------

namespace Fanex.BetList.Example.Data
{
    public enum TicketDataTemplateColumnIndex : int
    {
        /// <summary>
        /// The reference no.
        /// </summary>
        RefNo = 0,

        /// <summary>
        /// The index of odds.
        /// </summary>
        Odds = 1,

        /// <summary>
        /// The stake.
        /// </summary>
        Stake = 2,

        /// <summary>
        /// The status.
        /// </summary>
        Status = 3,

        /// <summary>
        /// The agent PT.
        /// </summary>
        AgentPT = 4,

        /// <summary>
        /// The master PT.
        /// </summary>
        MasterPT = 5,

        /// <summary>
        /// The super PT.
        /// </summary>
        SuperPT = 6,

        /// <summary>
        /// The bet type identifier.
        /// </summary>
        BetTypeId = 7,

        /// <summary>
        /// The bet team.
        /// </summary>
        BetTeam = 8,

        /// <summary>
        /// The bet check.
        /// </summary>
        TransDesc = 9
    }
}