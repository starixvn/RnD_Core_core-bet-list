//<copyright file="LogDataBase.cs" company="Nexcel Solutions Vietnam" >
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
//</copyright>
//
//<history>
//	<change who="anders.le" date="2012.2.13">Created</change>
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
    public class LogDataBase : ILogData
    {
        #region ILogEntity Members

        protected const string TrackingSubAcc = "SubAccId=\"{0}\" SubAccName=\"{1}\"";

        public int UserId { get; set; }

        public int RoleId { get; set; }

        public string ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

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

        public int SubAccId { get; set; }

        public LogActions Action { get; set; }

        public int ActionId
        {
            get { return Convert.ToInt32(this.Action); }
        }

        public virtual bool IsValid
        {
            get { return true; }
        }

        #region Virtual functions

        public virtual string SpNameTag { get { throw new NotImplementedException(); } }

        public virtual List<string> NewValues { get { throw new NotImplementedException(); } }

        public virtual List<string> OldValues { get { throw new NotImplementedException(); } }

        #endregion Virtual functions

        #endregion ILogEntity Members

        #region ILogData Members

        public string SignUpIp
        {
            get;
            set;
        }

        public string CopiedFrom
        {
            get;
            set;
        }

        #endregion ILogData Members
    }
}