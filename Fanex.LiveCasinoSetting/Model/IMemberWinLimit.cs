// --------------------------------------------------------------------------
// <copyright file="IMemberWinLimit.cs" company="Nexcel Solutions Vietnam">
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
    public interface IMemberWinLimit
    {
        ICollection<int> Limits
        {
            get;
        }

        bool IsDailyAutoReset
        {
            get;
            set;
        }

        bool IsDailyAutoResetUpline
        {
            get;
            set;
        }

        decimal CurrentWinLimit
        {
            get;
            set;
        }

        decimal TotalWinLimit
        {
            get;
            set;
        }

        decimal MaxWinLimit
        {
            get;
            set;
        }

        decimal MaxWinLimitUpline
        {
            get;
            set;
        }
    }
}