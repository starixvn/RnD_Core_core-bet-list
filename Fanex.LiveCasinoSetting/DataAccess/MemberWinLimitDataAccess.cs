// --------------------------------------------------------------------------
// <copyright file="MemberWinLimitDataAccess.cs" company="Nexcel Solutions Vietnam">
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
    using Fanex.Common.Extensions;
    using Fanex.Data;
    using Fanex.LiveCasinoSetting.Model;
    using Fanex.LiveCasinoSetting.Request;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal class MemberWinLimitDataAccess : DataAccessBase, IMemberWinLimitDataAccess
    {
        #region IMemberWinLimitDataAccess Members

        public Result UpdateMemberWinLimit(IMemberWinLimit memberWinLimit, IRequestContext context)
        {
            using (var objDb = new ObjectDb("Acc_CustomerUpdLiveCS_WinLimit"))
            {
                objDb.AddInParameter("custid", System.Data.DbType.Int32, context.CustId);
                objDb.AddInParameter("MaxWinLiveCasino", System.Data.DbType.Decimal, memberWinLimit.MaxWinLimit);
                objDb.AddInParameter("isAutoResetLiveCasino", System.Data.DbType.Boolean, memberWinLimit.IsDailyAutoReset);
                objDb.AddInParameter("SubAccID", System.Data.DbType.Int32, context.SubAccId);
                objDb.AddInParameter("IP", System.Data.DbType.String, context.SignUpIp);
                objDb.AddInParameter("UserID", System.Data.DbType.Int32, context.UserId);
                objDb.AddInParameter("Language", System.Data.DbType.String, context.Language);
                objDb.AddOutParameter("ErrCode", System.Data.DbType.Int32, sizeof(int));
                objDb.AddOutParameter("ErrMsg", System.Data.DbType.String, 4000);

                var reader = objDb.ExecuteNonQuery();

                return GetResultParams(objDb.Database, objDb.DbCommand);
            }
        }

        public IMemberWinLimit GetMemberWinLimit(int custId)
        {
            var result = new MemberWinLimit();

            using (var objDb = new ObjectDb("Acc_CustomerSelWinLimitLiveCS"))
            {
                objDb.AddInParameter("custid", System.Data.DbType.Int32, custId);
                var reader = objDb.ExecuteReader();

                while (reader != null && reader.Read())
                {
                    result = new MemberWinLimit()
                    {
                        MaxWinLimit = reader.ToDecimal("MaxWinLiveCasino"),
                        CurrentWinLimit = reader.ToDecimal("LCSWinPercent"),
                        TotalWinLimit = reader.ToDecimal("MaxWinCredit")
                    };
                }

                return result;
            }
        }

        public IList<Result> UpdateMultipleMemberWinLimit(IMemberWinLimit memberWinLimit, IRequestContext context, IList<int> custIds)
        {
            var results = new List<Result>();

            using (var objDb = new ObjectDb("Acc_CustomerUpdLiveCS_WinLimit"))
            {
                objDb.AddInParameter("custid", System.Data.DbType.Int32);
                objDb.AddInParameter("MaxWinLiveCasino", System.Data.DbType.Decimal, memberWinLimit.MaxWinLimit);
                objDb.AddInParameter("isAutoResetLiveCasino", System.Data.DbType.Boolean, memberWinLimit.IsDailyAutoReset);
                objDb.AddInParameter("SubAccID", System.Data.DbType.Int32, context.SubAccId);
                objDb.AddInParameter("IP", System.Data.DbType.String, context.SignUpIp);
                objDb.AddInParameter("UserID", System.Data.DbType.Int32, context.UserId);
                objDb.AddInParameter("Language", System.Data.DbType.String, context.Language);
                objDb.AddOutParameter("ErrCode", System.Data.DbType.Int32, sizeof(int));
                objDb.AddOutParameter("ErrMsg", System.Data.DbType.String, 4000);

                custIds.ToList().ForEach(custId =>
                {
                    objDb.SetParameterValue("custid", custId);

                    var reader = objDb.ExecuteNonQuery();
                    results.Add(GetResultParams(objDb.Database, objDb.DbCommand));
                });
            }

            return results;
        }

        #endregion IMemberWinLimitDataAccess Members
    }
}