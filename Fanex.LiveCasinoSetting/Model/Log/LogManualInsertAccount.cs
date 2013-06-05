//<copyright file="LogAutoInsert.cs" company="Nexcel Solutions Vietnam" >
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
    using System.Collections.Specialized;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class LogManualInsertAccount : LogDataBase, ILogData
    {
        private const string TrackingTemplate
            = "<TP_User_Customer_ManualInsertAccount CustID=\"{0}\" UserName=\"{1}\" CasinoNo=\"{2}\" ExchangeID=\"{3}\" SCustID=\"{4}\" MCustID=\"{5}\" ACustID=\"{6}\" RoleID=\"{7}\" SignupIP=\"{8}\" SignupBy=\"{9}\" AddFrom=\"{10}\" Lang=\"{11}\" ErrCode=\"{12}\" ErrMsg=\"{13}\" {14} CustSite=\"{15}\" />";

        public string CasinoNo { get { return "01"; } }
        public string CustUserName { get; set; }
        public int ExchangeId { get; set; }
        public int SuperId { get; set; }
        public int MasterId { get; set; }
        public int AgentId { get; set; }
        public string UserName { get; set; }
        public int SignUpForm { get; set; }
        public string CustSite { get; set; }

        /// <summary>
        /// a list of (custid, cust-username) which manually insert successfully
        /// </summary>
        public List<KeyValuePair<int, string>> CustomerList { get; set; }

        public override string SpNameTag
        {
            get
            {
                return "TP_User_Customer_ManualInsertAccount";
            }
        }

        private string GetTrackingValue(int custId, string username)
        {
            return string.Format(TrackingTemplate
            , custId
            , username
            , CasinoNo
            , ExchangeId
            , SuperId
            , MasterId
            , AgentId
            , RoleId
            , SignUpIp
            , UserId
            , SignUpForm
            , Language
            , ErrorCode
            , ErrorMessage
            , RoleId == 4 ? string.Empty : string.Format(TrackingSubAcc, SubAccId, SubAccName)
            , CustSite
            );
        }

        public override List<string> NewValues
        {
            get
            {
                List<string> newValues = new List<string>();
                CustomerList.ForEach(info =>
                {
                    newValues.Add(GetTrackingValue(info.Key, info.Value));

                    //this log format does not require oldvalues
                    _oldValues.Add(string.Empty);
                });

                return newValues;
            }
        }

        private List<string> _oldValues = new List<string>();
        public override List<string> OldValues
        {
            get
            {
                return _oldValues;
            }
        }        
    }
}
