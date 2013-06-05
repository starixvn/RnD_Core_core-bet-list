//<copyright file="WriteLogRequest.cs" company="Nexcel Solutions Vietnam" >
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
//</copyright>
//
//<history>
//	<change who="anders.le" date="2012.2.6">Created</change>
//</history>

namespace Fanex.LiveCasinoSetting.Request
{
    using System;
    using Fanex.Common.Extensions;
    using Fanex.LiveCasinoSetting.Model;
    using Fanex.LiveCasinoSetting.Model.Log;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class WriteLogRequest
    {
        /// <summary>
        /// user perform action update
        /// </summary>        
        public int UserId { get; set; }

        /// <summary>
        /// ip of userid
        /// </summary>        
        public string Ip { get; set; }

        /// <summary>
        /// current language of system that the user has already authenticated
        /// </summary>        
        public string Language { get; set; }

        /// <summary>
        /// indentity of customer who is updated
        /// it can be memberid, agentid, masterid, or superid
        /// note: dont use it when perform action update multiple
        /// </summary>        
        public int CustId { get; set; }

        /// <summary>
        /// Sub-account Id is > 0 if sub-account updates settings
        /// </summary>
        public string SubAccName { get; set; }

        public LogActions Action { get; set; }

        /// <summary>
        /// Convert Level 
        /// default return 359
        /// </summary>
        public int ActionId
        {
            get
            {
                return Convert.ToInt32(this.Action);
            }
        }

        public bool IsValid
        {
            get
            {
                return UserId > 0
                    && Ip.IsIpAddress()
                    && !string.IsNullOrWhiteSpace(Language)
                    && CustId > 0;
            }
        }

        public string SpNameTag
        {
            get
            {
                switch (ActionId)
                {
                    case 356:
                    case 357:
                    case 358:
                    case 369:
                        return "TP_User_CustGameType_UpdStakeLevel";
                    case 360:
                    case 361:
                    case 362:
                    case 363:
                        return "TP_User_Customer_AutoInsertAccount";
                }

                throw new ArgumentOutOfRangeException("ActionId");
            }
        }

       
    }
}
