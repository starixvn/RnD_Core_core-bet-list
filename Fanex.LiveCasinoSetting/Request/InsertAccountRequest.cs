//<copyright file="AutoInsertPTSettingRequest.cs" company="Nexcel Solutions Vietnam" >
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
//</copyright>
//
//<history>
//	<change who="anders.le" date="2012.2.2">Created</change>
//	<change who="anders.le" date="2012.2.10">Add IsAutoInsert property</change>
//</history>

namespace Fanex.LiveCasinoSetting.Request
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class InsertAccountRequest
    {
        public int CustId { get; set; }

        public int OldCustId { get; set; }

        public string CustUserName { get; set; }

        public int ExchangeId { get; set; }

        public int SuperId { get; set; }

        public int MasterId { get; set; }

        public int AgentId { get; set; }

        public int RoleId { get; set; }

        public string SignUpIp { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public int SignUpForm { get; set; }

        //Live casino status of upline
        public bool IsDisabledLiveCasino { get; set; }

        public bool IsEnableDownline { get; set; }

        public int SubAccId { get; set; }

        public string SubAccName { get; set; }

        public string Language { get; set; }

        public bool SyncLiveCasino { get; set; }

        public bool IsAutoInsert { get; set; }

        public string CustSite { get; set; }

        public string CopiedFrom { get; set; }
    }
}