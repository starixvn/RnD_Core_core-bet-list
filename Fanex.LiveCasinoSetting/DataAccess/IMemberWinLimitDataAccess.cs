// --------------------------------------------------------------------------
// <copyright file="IMemberWinLimitDataAccess.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Andy.Nguyen" date="2012.10.2">Created</change>
// </history>

namespace Fanex.LiveCasinoSetting.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.LiveCasinoSetting.Model;
    using Fanex.LiveCasinoSetting.Request;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IMemberWinLimitDataAccess
    {
        Result UpdateMemberWinLimit(IMemberWinLimit memberWinLimit, IRequestContext context);

        IList<Result> UpdateMultipleMemberWinLimit(IMemberWinLimit memberWinLimit, IRequestContext context, IList<int> custIds);

        IMemberWinLimit GetMemberWinLimit(int custId);
    }
}