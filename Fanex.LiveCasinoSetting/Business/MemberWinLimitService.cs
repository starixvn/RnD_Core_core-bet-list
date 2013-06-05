// --------------------------------------------------------------------------
// <copyright file="MemberWinLimitService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Andy.Nguyen" date="2012.10.3">Created</change>
// </history>

namespace Fanex.LiveCasinoSetting.Business
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.Common.Helper;
    using Fanex.LiveCasinoSetting.DataAccess;
    using Fanex.LiveCasinoSetting.Model;
    using Fanex.LiveCasinoSetting.Request;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class MemberWinLimitService : SettingServiceBase, IMemberWinLimitService
    {
        private readonly IMemberWinLimitDataAccess dao;

        public MemberWinLimitService(IMemberWinLimitDataAccess dataAccess = null)
        {
            dao = dataAccess ?? new MemberWinLimitDataAccess();
        }

        #region IMemberWinLimitBusiness Members

        public Result UpdateMemberWinLimit(IMemberWinLimit memberWinLimit, IRequestContext context)
        {
            Check.Argument.IsNotNull(memberWinLimit, "memberWinLimit");
            Check.Argument.IsNotNull(context, "context");

            try
            {
                return dao.UpdateMemberWinLimit(memberWinLimit, context);
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }

            return null;
        }

        public IMemberWinLimit GetMemberWinLimit(int custId)
        {
            Check.Argument.IsNotNegativeOrZero(custId, "custId");

            try
            {
                return dao.GetMemberWinLimit(custId);
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }

            return null;
        }

        public IList<Result> UpdateMultipleMemberWinLimit(IMemberWinLimit memberWinLimit, IRequestContext context, IList<int> custIds)
        {
            Check.Argument.IsNotNull(memberWinLimit, "memberWinLimit");
            Check.Argument.IsNotNull(context, "context");
            Check.Argument.IsNotNull(custIds, "custIds");

            try
            {
                return dao.UpdateMultipleMemberWinLimit(memberWinLimit, context, custIds);
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }

            return null;
        }

        #endregion IMemberWinLimitBusiness Members
    }
}