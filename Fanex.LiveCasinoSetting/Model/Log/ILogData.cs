//<copyright file="ILogEntity.cs" company="Nexcel Solutions Vietnam" >
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
//</copyright>
//
//<history>
//	<change who="anders.le" date="2012.2.9">Created</change>
//</history>


namespace Fanex.LiveCasinoSetting.Model.Log
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface ILogData
    {
        int UserId { get; set; }

        int RoleId { get; set; }

        string ErrorCode { get; set; }

        string ErrorMessage { get; set; }

        /// <summary>
        /// ip of userid
        /// </summary>        
        string SignUpIp { get; set; }

        /// <summary>
        /// current language of system that the user has already authenticated
        /// </summary>        
        string Language { get; set; }

        /// <summary>
        /// indentity of customer who is updated
        /// it can be memberid, agentid, masterid, or superid
        /// note: dont use it when perform action update multiple
        /// </summary>        
        int CustId { get; set; }

        string SpNameTag { get; }

        /// <summary>
        /// Sub-account Id is > 0 if sub-account updates settings
        /// </summary>
        string SubAccName { get; set; }

        int SubAccId { get; set; }

        //string TrackingValue { get; set; }

        LogActions Action { get; set; }      
       

        List<string> NewValues { get; }
        List<string> OldValues { get; }
        
        int ActionId { get; }
        bool IsValid { get; }
    }
}
