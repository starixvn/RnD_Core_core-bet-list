// ---------------------------------------------------------------------------------
// <copyright file="TicketDataMapper.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------------
// <history>
//     <change who="Marc.Bui" date="2014.02.25">Create</change>
// </history>
// ---------------------------------------------------------------------------------

namespace Fanex.BetList.CoreToVerify.Wrap.DTO
{
    using System.Collections.Generic;

    /// <summary>
    /// Map field definition for ITicketData type and DataRow.
    /// </summary>
    public class TicketDataMapper
    {
        private static Dictionary<string, string> dict = new Dictionary<string, string>()         
        { 
            { "RefNo", "refno" }, 
            { "BetTypeId", "bettype" }, 
            { "Status", "status" },
            { "Odds", "odds" },
            { "Stake", "stake" }, 
            { "BetTeam", "betteam" },
            { "SuperPT", "spositiontaking" },
            { "MasterPT", "mpositiontaking" },
            { "AgentPT", "apositiontaking" }
        };

        public static Dictionary<string, string> Dict
        {
            get
            {
                return dict;
            }
        }
    }
}
