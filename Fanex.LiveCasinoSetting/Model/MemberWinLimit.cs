// --------------------------------------------------------------------------
// <copyright file="MemberWinLimit.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Andy.Nguyen" date="2012.10.2">Created</change>
// </history>

namespace Fanex.LiveCasinoSetting.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class MemberWinLimit : IMemberWinLimit
    {
        #region IMemberWinLimit Members

        public ICollection<int> Limits
        {
            get { return new List<int>() { 3, 5, 10, 0 }; }
        }

        public bool IsDailyAutoReset
        {
            get;
            set;
        }

        public decimal CurrentWinLimit
        {
            get;
            set;
        }

        public decimal TotalWinLimit
        {
            get;
            set;
        }

        public decimal MaxWinLimit
        {
            get;
            set;
        }

        public bool IsDailyAutoResetUpline
        {
            get;
            set;
        }

        public decimal MaxWinLimitUpline
        {
            get;
            set;
        }

        #endregion IMemberWinLimit Members
    }
}