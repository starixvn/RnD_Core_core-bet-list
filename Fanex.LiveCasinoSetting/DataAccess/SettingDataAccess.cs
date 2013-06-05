// <copyright file="SettingDataAccess.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
//
// <summary>
// Define methods which use to access ibc data
// </summary>
//
// <history>
//  <change who="Anders" date="2012.01.8">Created</change>
//  <change who="Anders" date="2012.01.17">Replace function Get<T> by To[T]</change>
//	<change who="anders.le" date="2012.2.3">Get DisabledAutoMPT for Master Get Agent and Member PT Setting</change>
//	<change who="anders.le" date="2012.2.4">Explicit return and input data type instead of using IPTSetting</change>
//	<change who="anders.le" date="2012.2.9">Update param logic to check update pt and update auto pt</change>
//	<change who="andy.nguyen" date="2012.2.14">Add more param username2</change>
// </history>

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Fanex.Data;
using Fanex.LiveCasinoSetting.Model;
using Fanex.LiveCasinoSetting.Request;

namespace Fanex.LiveCasinoSetting.DataAccess
{
    using Fanex.Common.Extensions;

    /// <summary>
    /// splist https://docs.google.com/a/nexcel.vn/spreadsheet/ccc?key=0Aru5MrhrJW7CdDdMcElSSjlRZFN2ZE1WRDh4TEY4T3c&hl=en_US&pli=1#gid=0
    /// </summary>
    internal class SettingDataAccess : DataAccessBase, ISettingDataAccess
    {
        #region Get PT Info

        public CustomerInfo GetCustomerInfo(int custId)
        {
            CustomerInfo custInfo = new CustomerInfo();
            custInfo.CustomerId = custId;
            custInfo.Limit = new MemberWinLimit();

            using (ObjectDb objDb = new ObjectDb("Acc_CustomerSelInfoLiveCS"))
            {
                objDb.AddInParameter("custid", DbType.Int32, custId);

                using (var reader = objDb.ExecuteReader())
                {
                    if (reader != null && reader.Read())
                    {
                        custInfo.FirstName = reader["firstname"].ToString();
                        custInfo.LastName = reader["lastname"].ToString();
                        custInfo.UserName = reader["username"].ToString();
                        custInfo.NickName = reader["username2"].ToString();
                        custInfo.Phone = reader["phone"].ToString();
                        custInfo.Mobile = reader["mobilephone"].ToString();
                        custInfo.Fax = reader["fax"].ToString();
                        custInfo.RoleId = reader.To<byte>("roleid");
                        custInfo.IsSyncLiveCasino = reader.ToBool("syncLiveCasino");
                        custInfo.ExChangeId = reader.ToInt("currency");
                        custInfo.Site = reader.ToString("site");
                        custInfo.DisableLiveCasino = reader.ToBool("DisableLiveCasino");
                        custInfo.UplineDisableLiveCasino = reader.ToBool("UplineDisableLiveCasino");
                        custInfo.Limit.MaxWinLimit = reader.ToDecimal("MaxWinLiveCasino");
                        custInfo.Limit.MaxWinLimitUpline = reader.ToDecimal("UplineMaxWinLiveCasino");
                        custInfo.Limit.IsDailyAutoReset = reader.ToBool("IsAutoResetLiveCasino");
                        custInfo.Limit.IsDailyAutoResetUpline = reader.ToBool("UplineIsAutoResetLiveCasino");
                    }
                }
            }

            return custInfo;
        }

        public SuperPTSetting GetSuperPTSetting(int custId)
        {
            SuperPTSetting ptSetting = new SuperPTSetting();

            using (var objDb = new ObjectDb("Acc_CustomerSelEditSuperLiveCS"))
            {
                objDb.AddInParameter("custid", DbType.Int32, custId);

                using (var reader = objDb.ExecuteReader())
                {
                    if (reader != null && reader.Read())
                    {
                        var adminSetting = new SuperPTSetting();
                        adminSetting.MaxPT = reader.ToDecimal("v162spositiontaking_1101_max");
                        adminSetting.Comm = reader.ToDecimal("v162sdicount_1101_max");

                        ptSetting.Upline = adminSetting;
                        ptSetting.MaxPT = reader.ToDecimal("v162spositiontaking_1101");
                        ptSetting.MinPT = reader.ToDecimal("v162spositiontaking_1101_min");
                        ptSetting.Comm = reader.ToDecimal("v162sdiscount_1101");
                    }
                }
            }

            return ptSetting;
        }

        public PTSetting GetMasterPTSeting(int custId, int uplineId)
        {
            var ptSetting = new PTSetting();//Master

            using (var objDb = new ObjectDb("Acc_CustomerSelEditMasterLiveCS"))
            {
                objDb.AddInParameter("custid", DbType.Int32, custId);
                objDb.AddInParameter("srecommend", DbType.Int32, uplineId);

                using (var reader = objDb.ExecuteReader())
                {
                    if (reader != null && reader.Read())
                    {
                        var superSetting = new PTSetting();//Super
                        superSetting.MaxPT = reader.ToDecimal("v162spositiontaking_1101_max");
                        superSetting.Comm = reader.ToDecimal("v162discount_1101_max");
                        superSetting.PresetPT = reader.ToDecimal("v162spositiontaking_1101");
                        superSetting.AutoPresetPT = reader.ToDecimal("auto_v162spostiontaking_1101");
                        superSetting.IsAutoPresetPT = reader.ToBool("isauto_v162spositiontaking_1101");

                        ptSetting.Upline = superSetting;
                        ptSetting.MaxPT = reader.ToDecimal("v162mpositiontaking_1101");
                        ptSetting.MinPT = reader.ToDecimal("v162mpositiontaking_1101_min");
                        ptSetting.Comm = reader.ToDecimal("v162discount_1101");
                    }
                }
            }

            return ptSetting;
        }

        public PTSetting GetAgentPTSetting(int custId, int uplineId)
        {
            var ptSetting = new PTSetting();//Agent

            using (var objDb = new ObjectDb("Acc_CustomerSelEditAgentLiveCS"))
            {
                objDb.AddInParameter("custid", DbType.Int32, custId);
                objDb.AddInParameter("mrecommend", DbType.Int32, uplineId);

                using (var reader = objDb.ExecuteReader())
                {
                    if (reader != null && reader.Read())
                    {
                        var masterSetting = new PTSetting(reader.ToBool("DisableAutoMPT"));//Master
                        masterSetting.MaxPT = reader.ToDecimal("auto_v162mpositiontaking_1101_max");
                        masterSetting.Comm = reader.ToDecimal("v162discount_1101_max");
                        masterSetting.PresetPT = reader.ToDecimal("v162mpositiontaking_1101");
                        masterSetting.AutoPresetPT = reader.ToDecimal("auto_v162mpositiontaking_1101");
                        masterSetting.IsAutoPresetPT = reader.ToBool("isauto_v162mpositiontaking_1101");

                        ptSetting.Upline = masterSetting;
                        ptSetting.MaxPT = reader.ToDecimal("v162apositiontaking_1101");
                        ptSetting.MinPT = reader.ToDecimal("v162apositiontaking_1101_min");
                        ptSetting.Comm = reader.ToDecimal("v162discount_1101");
                    }
                }
            }

            return ptSetting;
        }

        public PTSetting GetMemberPTSetting(int custId, int uplineId)
        {
            var ptSetting = new PTSetting();//Member

            using (var objDb = new ObjectDb("Acc_CustomerSelEditMemberLiveCS"))
            {
                objDb.AddInParameter("custid", DbType.Int32, custId);
                objDb.AddInParameter("recommend", DbType.Int32, uplineId);

                using (var reader = objDb.ExecuteReader())
                {
                    if (reader != null && reader.Read())
                    {
                        var agentSetting = new PTSetting();//Agent
                        agentSetting.PresetPT = reader.ToDecimal("v162apositiontaking_1101");
                        agentSetting.MaxPT = reader.ToDecimal("v162apositiontaking_1101_max");
                        agentSetting.MinPT = reader.ToDecimal("v162apositiontaking_1101_min");
                        agentSetting.Comm = reader.ToDecimal("v162playerdiscount_1101_max");

                        ptSetting.Upline = agentSetting;
                        ptSetting.Comm = reader.ToDecimal("v162playerdiscount_1101");
                    }
                }
            }

            return ptSetting;
        }

        public PTSetting SuperGetMemberPTSetting(int custId, int uplineId)
        {
            var ptSetting = new PTSetting();//Member setting

            using (var objDb = new ObjectDb("Acc_CustomerSelSEditCustomerLiveCS"))
            {
                objDb.AddInParameter("custid", DbType.Int32, custId);

                using (var reader = objDb.ExecuteReader())
                {
                    if (reader != null && reader.Read())
                    {
                        var superSetting = new PTSetting();//Super
                        superSetting.MaxPT = reader.ToDecimal("v162spositiontaking_1101_max");
                        superSetting.MinPT = reader.ToDecimal("v162spositiontaking_1101_min");
                        superSetting.PresetPT = reader.ToDecimal("v162spositiontaking_1101");
                        superSetting.AutoPresetPT = reader.ToDecimal("auto_v162spositiontaking_1101_mem");
                        superSetting.MaxAutoPresetPT = reader.ToDecimal("auto_v162spositiontaking_1101_max");
                        superSetting.IsAutoPresetPT = reader.ToBool("isauto_v162spositiontaking_1101_mem");

                        var masterSetting = new PTSetting();//Master
                        masterSetting.PresetPT = reader.ToDecimal("v162mpositiontaking_1101");
                        masterSetting.IsAutoPresetPT = reader.ToBool("isauto_v162spositiontaking_1101");
                        masterSetting.Upline = superSetting;

                        var agentSetting = new PTSetting();//Agent
                        agentSetting.PresetPT = reader.ToDecimal("v162apositiontaking_1101");
                        agentSetting.Upline = masterSetting;

                        ptSetting.Upline = agentSetting;
                    }
                }
            }

            return ptSetting;
        }

        public PTSetting MasterGetMemberPTSetting(int custId, int uplineId)
        {
            var ptSetting = new PTSetting();//Member setting

            using (var objDb = new ObjectDb("Acc_CustomerSelMEditCustomerLiveCS"))
            {
                objDb.AddInParameter("custid", DbType.Int32, custId);

                using (var reader = objDb.ExecuteReader())
                {
                    if (reader != null && reader.Read())
                    {
                        var masterSetting = new PTSetting(reader.ToBool("DisableAutoMPT"));//Master
                        masterSetting.MaxPT = reader.ToDecimal("v162mpositiontaking_1101_max");
                        masterSetting.MinPT = reader.ToDecimal("v162mpositiontaking_1101_min");
                        masterSetting.PresetPT = reader.ToDecimal("v162mpositiontaking_1101");
                        masterSetting.AutoPresetPT = reader.ToDecimal("auto_v162mpositiontaking_1101_mem");
                        masterSetting.MaxAutoPresetPT = reader.ToDecimal("auto_v162mpositiontaking_1101_max");
                        masterSetting.IsAutoPresetPT = reader.ToBool("isauto_v162mpositiontaking_1101_mem");

                        var agentSetting = new PTSetting();//Agent
                        agentSetting.PresetPT = reader.ToDecimal("V162apositiontaking_1101");
                        agentSetting.IsAutoPresetPT = reader.ToBool("isauto_v162mpositiontaking_1101");
                        agentSetting.Upline = masterSetting;
                        ptSetting.Upline = agentSetting;
                    }
                }
            }

            return ptSetting;
        }

        #endregion Get PT Info

        #region Update PT Setting

        public Result UpdateSuperPTSetting(IRequestContext requestContext, SuperPTSetting setting)
        {
            using (var objDb = new ObjectDb("Acc_CustomerUpdSuperLiveCS_All"))
            {
                objDb.AddInParameter("custid", DbType.Int32, requestContext.CustId);
                objDb.AddInParameter("v162spositiontaking_1101", DbType.Decimal, setting.MaxPT);
                objDb.AddInParameter("v162spositiontaking_1101_min", DbType.Decimal, setting.MinPT);
                objDb.AddInParameter("v162discount_1101", DbType.Decimal, setting.Comm);
                objDb.AddInParameter("v162UpdatePositiontaking_1101", DbType.Boolean, requestContext.IsSyncLiveCasino ? setting.IsMaxPTChanged : true);
                objDb.AddInParameter("v162UpdateMinPositiontaking_1101", DbType.Boolean, requestContext.IsSyncLiveCasino ? setting.IsMinPTChanged : true);
                objDb.AddInParameter("v162UpdateCommission_1101", DbType.Boolean, requestContext.IsSyncLiveCasino ? setting.IsCommChanged : true);
                objDb.AddInParameter("IP", DbType.String, requestContext.SignUpIp);
                objDb.AddInParameter("UserID", DbType.Int32, requestContext.UserId);
                objDb.AddInParameter("Language", DbType.String, requestContext.Language);
                AddResultParams(objDb.Database, objDb.DbCommand);
                objDb.ExecuteNonQuery();
                Result result = GetResultParams(objDb.Database, objDb.DbCommand);

                return result;
            }
        }

        public Result UpdateMasterPTSetting(IRequestContext requestContext, PTSetting setting)
        {
            using (var objDb = new ObjectDb("Acc_CustomerUpdMasterLiveCS_All"))
            {
                var pt = setting as PTSetting;
                var superPTSetting = pt.Upline as PTSetting;

                var isUpdatePT = superPTSetting.IsPresetPTChanged || pt.IsMaxPTChanged;
                var isAutoPresetPTChanged = superPTSetting.IsIsAutoPresetPTChanged || superPTSetting.IsAutoPresetPTChanged;

                objDb.AddInParameter("custid", DbType.Int32, requestContext.CustId);
                objDb.AddInParameter("srecommend", DbType.Int32, requestContext.UplineId);
                objDb.AddInParameter("v162spositiontaking_1101", DbType.Decimal, superPTSetting.PresetPT);
                objDb.AddInParameter("v162mpositiontaking_1101", DbType.Decimal, pt.MaxPT);
                objDb.AddInParameter("auto_v162spositiontaking_1101", DbType.Decimal, superPTSetting.AutoPresetPT);
                objDb.AddInParameter("isauto_v162spositiontaking_1101", DbType.Boolean, superPTSetting.IsAutoPresetPT);
                objDb.AddInParameter("v162mpositiontaking_1101_min", DbType.Decimal, pt.MinPT);
                objDb.AddInParameter("v162discount_1101", DbType.Decimal, pt.Comm);
                objDb.AddInParameter("v162UpdatePositiontaking_1101", DbType.Boolean, requestContext.IsSyncLiveCasino ? isUpdatePT : true);
                objDb.AddInParameter("v162UpdateMinPositiontaking_1101", DbType.Boolean, requestContext.IsSyncLiveCasino ? pt.IsMinPTChanged : true);
                objDb.AddInParameter("v162UpdateAutoPositiontaking_1101", DbType.Boolean, requestContext.IsSyncLiveCasino ? isAutoPresetPTChanged : true);
                objDb.AddInParameter("v162UpdateCommission_1101", DbType.Boolean, requestContext.IsSyncLiveCasino ? pt.IsCommChanged : true);
                objDb.AddInParameter("SubAccID", DbType.Int32, requestContext.SubAccId);
                objDb.AddInParameter("IP", DbType.String, requestContext.SignUpIp);
                objDb.AddInParameter("UserID", DbType.Int32, requestContext.UserId);
                objDb.AddInParameter("Language", DbType.String, requestContext.Language);
                objDb.AddInParameter("updateallmembers", DbType.Boolean, superPTSetting.IsUpdateAllMember);
                objDb.AddInParameter("newspositiontaking ", DbType.Decimal, superPTSetting.NewMemberPT);

                AddResultParams(objDb.Database, objDb.DbCommand);
                objDb.ExecuteNonQuery();
                Result result = GetResultParams(objDb.Database, objDb.DbCommand);

                return result;
            }
        }

        public Dictionary<int, Result> UpdateMasterPTSetting(IRequestContext requestContext, PTSetting setting, List<int> custIdList)
        {
            var resultList = new Dictionary<int, Result>();
            using (var objDb = new ObjectDb("Acc_CustomerUpdMasterLiveCS_All"))
            {
                var pt = setting as PTSetting;
                var superPTSetting = pt.Upline as PTSetting;

                var isUpdatePT = superPTSetting.IsPresetPTChanged || pt.IsMaxPTChanged;
                var isAutoPresetPTChanged = superPTSetting.IsIsAutoPresetPTChanged || superPTSetting.IsAutoPresetPTChanged;

                objDb.AddInParameter("custid", DbType.Int32, requestContext.CustId);
                objDb.AddInParameter("srecommend", DbType.Int32, requestContext.UplineId);
                objDb.AddInParameter("v162spositiontaking_1101", DbType.Decimal, superPTSetting.PresetPT);
                objDb.AddInParameter("v162mpositiontaking_1101", DbType.Decimal, pt.MaxPT);
                objDb.AddInParameter("auto_v162spositiontaking_1101", DbType.Decimal, superPTSetting.AutoPresetPT);
                objDb.AddInParameter("isauto_v162spositiontaking_1101", DbType.Boolean, superPTSetting.IsAutoPresetPT);
                objDb.AddInParameter("v162mpositiontaking_1101_min", DbType.Decimal, pt.MinPT);
                objDb.AddInParameter("v162discount_1101", DbType.Decimal, pt.Comm);
                objDb.AddInParameter("v162UpdatePositiontaking_1101", DbType.Boolean, isUpdatePT);
                objDb.AddInParameter("v162UpdateMinPositiontaking_1101", DbType.Boolean, pt.IsMinPTChanged);
                objDb.AddInParameter("v162UpdateAutoPositiontaking_1101", DbType.Boolean, isAutoPresetPTChanged);
                objDb.AddInParameter("v162UpdateCommission_1101", DbType.Boolean, pt.IsCommChanged);
                objDb.AddInParameter("SubAccID", DbType.Int32, requestContext.SubAccId);
                objDb.AddInParameter("IP", DbType.String, requestContext.SignUpIp);
                objDb.AddInParameter("UserID", DbType.Int32, requestContext.UserId);
                objDb.AddInParameter("Language", DbType.String, requestContext.Language);
                objDb.AddInParameter("updateallmembers", DbType.Boolean, superPTSetting.IsUpdateAllMember);
                objDb.AddInParameter("newspositiontaking ", DbType.Decimal, superPTSetting.NewMemberPT);

                AddResultParams(objDb.Database, objDb.DbCommand);

                //create paramater
                foreach (var custId in custIdList)
                {
                    objDb.Database.SetParameterValue(objDb.DbCommand, "custid", custId);

                    objDb.ExecuteNonQuery();
                    Result result = GetResultParams(objDb.Database, objDb.DbCommand);

                    resultList.Add(custId, result);
                }
            }

            return resultList;
        }

        public Result UpdateAgentPTSetting(IRequestContext requestContext, PTSetting setting)
        {
            using (var objDb = new ObjectDb("Acc_CustomerUpdAgentLiveCS_All"))
            {
                var masterPTSetting = setting.Upline as PTSetting;

                var isUpdatePT = masterPTSetting.IsPresetPTChanged || setting.IsMaxPTChanged;
                var isAutoPresetPTChanged = masterPTSetting.IsAutoPresetPTChanged || masterPTSetting.IsIsAutoPresetPTChanged;

                objDb.AddInParameter("custid", DbType.Int32, requestContext.CustId);
                objDb.AddInParameter("mrecommend", DbType.Decimal, requestContext.UplineId);
                objDb.AddInParameter("v162mpositiontaking_1101", DbType.Decimal, masterPTSetting.PresetPT);
                objDb.AddInParameter("auto_v162mpositiontaking_1101", DbType.Decimal, masterPTSetting.AutoPresetPT);
                objDb.AddInParameter("isauto_v162mpositiontaking_1101", DbType.Boolean, masterPTSetting.IsAutoPresetPT);
                objDb.AddInParameter("v162apositiontaking_1101", DbType.Decimal, setting.MaxPT);
                objDb.AddInParameter("min_v162apositiontaking_1101", DbType.Decimal, setting.MinPT);
                objDb.AddInParameter("v162discount_1101", DbType.Decimal, setting.Comm);
                objDb.AddInParameter("v162UpdatePositiontaking_1101", DbType.Boolean, isUpdatePT);
                objDb.AddInParameter("v162UpdateMinPositiontaking_1101", DbType.Boolean, requestContext.IsSyncLiveCasino ? setting.IsMinPTChanged : true);
                objDb.AddInParameter("v162UpdateAutoPositiontaking_1101", DbType.Boolean, requestContext.IsSyncLiveCasino ? isAutoPresetPTChanged : true);

                objDb.AddInParameter("v162UpdateCommission_1101", DbType.Boolean, requestContext.IsSyncLiveCasino ? setting.IsCommChanged : true);
                objDb.AddInParameter("UserID", DbType.Int32, requestContext.UserId);
                objDb.AddInParameter("SubAccID", DbType.Int32, requestContext.SubAccId);
                objDb.AddInParameter("Language", DbType.String, requestContext.Language);
                objDb.AddInParameter("IP", DbType.String, requestContext.SignUpIp);
                objDb.AddInParameter("updateallmembers", DbType.Boolean, masterPTSetting.IsUpdateAllMember);
                objDb.AddInParameter("newmpositiontaking ", DbType.Decimal, masterPTSetting.NewMemberPT);

                AddResultParams(objDb.Database, objDb.DbCommand);
                objDb.ExecuteNonQuery();
                Result result = GetResultParams(objDb.Database, objDb.DbCommand);

                return result;
            }
        }

        public Dictionary<int, Result> UpdateAgentPTSetting(IRequestContext requestContext, PTSetting setting, List<int> custIdList)
        {
            var resultList = new Dictionary<int, Result>();
            using (var objDb = new ObjectDb("Acc_CustomerUpdAgentLiveCS_All"))
            {
                var masterPTSetting = setting.Upline as PTSetting;
                var isUpdatePT = masterPTSetting.IsPresetPTChanged || setting.IsMaxPTChanged;
                var isAutoPresetPTChanged = masterPTSetting.IsAutoPresetPTChanged || masterPTSetting.IsIsAutoPresetPTChanged;

                objDb.AddInParameter("custid", DbType.Int32, requestContext.CustId);
                objDb.AddInParameter("mrecommend", DbType.Decimal, requestContext.UplineId);
                objDb.AddInParameter("v162mpositiontaking_1101", DbType.Decimal, masterPTSetting.PresetPT);
                objDb.AddInParameter("auto_v162mpositiontaking_1101", DbType.Decimal, masterPTSetting.AutoPresetPT);
                objDb.AddInParameter("isauto_v162mpositiontaking_1101", DbType.Boolean, masterPTSetting.IsAutoPresetPT);
                objDb.AddInParameter("v162apositiontaking_1101", DbType.Decimal, setting.MaxPT);
                objDb.AddInParameter("min_v162apositiontaking_1101", DbType.Decimal, setting.MinPT);
                objDb.AddInParameter("v162discount_1101", DbType.Decimal, setting.Comm);
                objDb.AddInParameter("v162UpdatePositiontaking_1101", DbType.Boolean, isUpdatePT);
                objDb.AddInParameter("v162UpdateAutoPositiontaking_1101", DbType.Boolean, isAutoPresetPTChanged);
                objDb.AddInParameter("v162UpdateMinPositiontaking_1101", DbType.Boolean, setting.IsMinPTChanged);
                objDb.AddInParameter("v162UpdateCommission_1101", DbType.Boolean, setting.IsCommChanged);
                objDb.AddInParameter("UserID", DbType.Int32, requestContext.UserId);
                objDb.AddInParameter("SubAccID", DbType.Int32, requestContext.SubAccId);
                objDb.AddInParameter("Language", DbType.String, requestContext.Language);
                objDb.AddInParameter("IP", DbType.String, requestContext.SignUpIp);
                objDb.AddInParameter("updateallmembers", DbType.Boolean, masterPTSetting.IsUpdateAllMember);
                objDb.AddInParameter("newmpositiontaking ", DbType.Decimal, masterPTSetting.NewMemberPT);

                AddResultParams(objDb.Database, objDb.DbCommand);

                //create paramater
                foreach (var custId in custIdList)
                {
                    objDb.Database.SetParameterValue(objDb.DbCommand, "custid", custId);

                    objDb.ExecuteNonQuery();
                    Result result = GetResultParams(objDb.Database, objDb.DbCommand);

                    resultList.Add(custId, result);
                }

                return resultList;
            }
        }

        public Result UpdateMemberPTSetting(IRequestContext requestContext, PTSetting setting)
        {
            using (var objDb = new ObjectDb("Acc_CustomerUpdMemberLiveCS_All"))
            {
                var agentPTSetting = setting.Upline as PTSetting;

                objDb.AddInParameter("custid", DbType.Int32, requestContext.CustId);
                objDb.AddInParameter("recommend", DbType.Decimal, requestContext.UplineId);
                objDb.AddInParameter("v162apositiontaking_1101", DbType.Decimal, agentPTSetting.PresetPT);
                objDb.AddInParameter("v162playerdiscount_1101", DbType.Decimal, setting.Comm);
                objDb.AddInParameter("v162UpdatePositiontaking_1101", DbType.Boolean, requestContext.IsSyncLiveCasino ? agentPTSetting.IsPresetPTChanged : true);
                objDb.AddInParameter("v162UpdateCommission_1101", DbType.Boolean, requestContext.IsSyncLiveCasino ? setting.IsCommChanged : true);
                objDb.AddInParameter("SubAccID", DbType.Int32, requestContext.SubAccId);
                objDb.AddInParameter("IP", DbType.String, requestContext.SignUpIp);
                objDb.AddInParameter("UserID", DbType.Int32, requestContext.UserId);
                objDb.AddInParameter("Language", DbType.String, requestContext.Language);

                AddResultParams(objDb.Database, objDb.DbCommand);
                objDb.ExecuteNonQuery();
                Result result = GetResultParams(objDb.Database, objDb.DbCommand);

                return result;
            }
        }

        public Dictionary<int, Result> UpdateMemberPTSetting(IRequestContext requestContext, PTSetting setting, List<int> custIdList)
        {
            var resultList = new Dictionary<int, Result>();
            using (var objDb = new ObjectDb("Acc_CustomerUpdMemberLiveCS_All"))
            {
                var uplinePT = setting.Upline as PTSetting;
                objDb.AddInParameter("custid", DbType.Int32);
                objDb.AddInParameter("recommend", DbType.Decimal, requestContext.UplineId);
                objDb.AddInParameter("v162apositiontaking_1101", DbType.Decimal, uplinePT.PresetPT);
                objDb.AddInParameter("v162playerdiscount_1101", DbType.Decimal, setting.Comm);
                objDb.AddInParameter("v162UpdatePositiontaking_1101", DbType.Boolean, uplinePT.IsPresetPTChanged);
                objDb.AddInParameter("v162UpdateCommission_1101", DbType.Boolean, setting.IsCommChanged);
                objDb.AddInParameter("SubAccID", DbType.Int32, requestContext.SubAccId);
                objDb.AddInParameter("IP", DbType.String, requestContext.SignUpIp);
                objDb.AddInParameter("UserID", DbType.Int32, requestContext.UserId);
                objDb.AddInParameter("Language", DbType.String, requestContext.Language);
                AddResultParams(objDb.Database, objDb.DbCommand);
                //create paramater
                foreach (var custId in custIdList)
                {
                    objDb.Database.SetParameterValue(objDb.DbCommand, "custid", custId);

                    objDb.ExecuteNonQuery();
                    Result result = GetResultParams(objDb.Database, objDb.DbCommand);

                    resultList.Add(custId, result);
                }

                return resultList;
            }
        }

        public Result UpdateMemberPTSettingBySuper(IRequestContext requestContext, PTSetting setting)
        {
            using (var objDb = new ObjectDb("Acc_CustomerUpdMemberLiveCS_SAg"))
            {
                var superPTSetting = setting.Upline.Upline.Upline as PTSetting;

                var isAutoPresetPTChanged = superPTSetting.IsAutoPresetPTChanged || superPTSetting.IsIsAutoPresetPTChanged;

                objDb.AddInParameter("custid", DbType.Int32, requestContext.CustId);
                objDb.AddInParameter("v162spositiontaking_1101", DbType.Decimal, superPTSetting.PresetPT);
                objDb.AddInParameter("auto_v162spositiontaking_1101", DbType.Decimal, superPTSetting.AutoPresetPT);
                objDb.AddInParameter("isauto_v162spositiontaking_1101", DbType.Boolean, superPTSetting.IsAutoPresetPT);
                objDb.AddInParameter("v162UpdatePositiontaking_1101", DbType.Boolean, superPTSetting.IsPresetPTChanged);
                objDb.AddInParameter("v162UpdateAutoPositiontaking_1101", DbType.Boolean, isAutoPresetPTChanged);
                objDb.AddInParameter("IP", DbType.String, requestContext.SignUpIp);
                objDb.AddInParameter("UserID", DbType.Int32, requestContext.UserId);
                objDb.AddInParameter("SubAccID", DbType.Int32, requestContext.SubAccId);
                objDb.AddInParameter("Language", DbType.String, requestContext.Language);
                AddResultParams(objDb.Database, objDb.DbCommand);
                objDb.ExecuteNonQuery();
                Result result = GetResultParams(objDb.Database, objDb.DbCommand);

                return result;
            }
        }

        public Dictionary<int, Result> UpdateMemberPTSettingBySuper(IRequestContext requestContext, PTSetting setting, List<int> custIdList)
        {
            var resultList = new Dictionary<int, Result>();
            using (var objDb = new ObjectDb("Acc_CustomerUpdMemberLiveCS_SAg"))
            {
                var superPTSetting = setting.Upline.Upline.Upline as PTSetting;

                var isAutoPresetPTChanged = superPTSetting.IsAutoPresetPTChanged || superPTSetting.IsIsAutoPresetPTChanged;

                objDb.AddInParameter("custid", DbType.Int32, requestContext.CustId);
                objDb.AddInParameter("v162spositiontaking_1101", DbType.Decimal, superPTSetting.PresetPT);
                objDb.AddInParameter("auto_v162spositiontaking_1101", DbType.Decimal, superPTSetting.AutoPresetPT);
                objDb.AddInParameter("isauto_v162spositiontaking_1101", DbType.Boolean, superPTSetting.IsAutoPresetPT);
                objDb.AddInParameter("v162UpdatePositiontaking_1101", DbType.Boolean, superPTSetting.IsPresetPTChanged);
                objDb.AddInParameter("v162UpdateAutoPositiontaking_1101", DbType.Boolean, isAutoPresetPTChanged);
                objDb.AddInParameter("IP", DbType.String, requestContext.SignUpIp);
                objDb.AddInParameter("UserID", DbType.Int32, requestContext.UserId);
                objDb.AddInParameter("SubAccID", DbType.Int32, requestContext.SubAccId);
                objDb.AddInParameter("Language", DbType.String, requestContext.Language);
                AddResultParams(objDb.Database, objDb.DbCommand);
                //create paramater
                foreach (var custId in custIdList)
                {
                    objDb.Database.SetParameterValue(objDb.DbCommand, "custid", custId);

                    objDb.ExecuteNonQuery();
                    Result result = GetResultParams(objDb.Database, objDb.DbCommand);

                    resultList.Add(custId, result);
                }

                return resultList;
            }
        }

        public Result UpdateMemberPTSettingByMaster(IRequestContext requestContext, PTSetting setting)
        {
            using (var objDb = new ObjectDb("Acc_CustomerUpdMemberLiveCS_MAg"))
            {
                var masterPTSetting = setting.Upline.Upline as PTSetting;

                objDb.AddInParameter("custid", DbType.Int32, requestContext.CustId);
                objDb.AddInParameter("v162mpositiontaking_1101", DbType.Decimal, masterPTSetting.PresetPT);
                objDb.AddInParameter("auto_v162mpositiontaking_mem_1101", DbType.Decimal, masterPTSetting.AutoPresetPT);
                objDb.AddInParameter("isauto_v162mpositiontaking_mem_1101", DbType.Boolean, masterPTSetting.IsAutoPresetPT);
                objDb.AddInParameter("isauto_v162mpositiontaking_1101", DbType.Boolean, masterPTSetting.IsAutoPresetPT);
                objDb.AddInParameter("v162UpdatePositiontaking_1101", DbType.Boolean, masterPTSetting.IsPresetPTChanged);
                objDb.AddInParameter("v162UpdateAutoPositiontaking_1101", DbType.Boolean, masterPTSetting.IsAutoPresetPTChanged || masterPTSetting.IsIsAutoPresetPTChanged);
                objDb.AddInParameter("UserID", DbType.Int32, requestContext.UserId);
                objDb.AddInParameter("SubAccID", DbType.Int32, requestContext.SubAccId);
                objDb.AddInParameter("Language", DbType.String, requestContext.Language);
                objDb.AddInParameter("IP", DbType.String, requestContext.SignUpIp);
                objDb.AddOutParameter("Username", DbType.String, 50);
                AddResultParams(objDb.Database, objDb.DbCommand);
                objDb.ExecuteNonQuery();
                Result result = GetResultParams(objDb.Database, objDb.DbCommand);

                return result;
            }
        }

        public Dictionary<int, Result> UpdateMemberPTSettingByMaster(IRequestContext requestContext, PTSetting setting, List<int> custIdList)
        {
            var resultList = new Dictionary<int, Result>();
            using (var objDb = new ObjectDb("Acc_CustomerUpdMemberLiveCS_MAg"))
            {
                var masterPTSetting = setting.Upline.Upline as PTSetting;

                objDb.AddInParameter("custid", DbType.Int32, requestContext.CustId);
                objDb.AddInParameter("v162mpositiontaking_1101", DbType.Decimal, masterPTSetting.PresetPT);
                objDb.AddInParameter("auto_v162mpositiontaking_mem_1101", DbType.Decimal, masterPTSetting.AutoPresetPT);
                objDb.AddInParameter("isauto_v162mpositiontaking_mem_1101", DbType.Boolean, masterPTSetting.IsAutoPresetPT);
                objDb.AddInParameter("isauto_v162mpositiontaking_1101", DbType.Boolean, masterPTSetting.IsAutoPresetPT);
                objDb.AddInParameter("v162UpdatePositiontaking_1101", DbType.Boolean, masterPTSetting.IsPresetPTChanged);
                objDb.AddInParameter("v162UpdateAutoPositiontaking_1101", DbType.Boolean, masterPTSetting.IsAutoPresetPTChanged || masterPTSetting.IsIsAutoPresetPTChanged);
                objDb.AddInParameter("UserID", DbType.Int32, requestContext.UserId);
                objDb.AddInParameter("SubAccID", DbType.Int32, requestContext.SubAccId);
                objDb.AddInParameter("Language", DbType.String, requestContext.Language);
                objDb.AddInParameter("IP", DbType.String, requestContext.SignUpIp);
                objDb.AddOutParameter("Username", DbType.String, 50);
                AddResultParams(objDb.Database, objDb.DbCommand);
                //create paramater
                foreach (var custId in custIdList)
                {
                    objDb.Database.SetParameterValue(objDb.DbCommand, "custid", custId);

                    objDb.ExecuteNonQuery();
                    Result result = GetResultParams(objDb.Database, objDb.DbCommand);

                    resultList.Add(custId, result);
                }

                return resultList;
            }
        }

        #endregion Update PT Setting

        public Result UpdateStatusLiveCasino(UpdateLiveCasinoStatusRequest request)
        {
            using (var objDb = new ObjectDb("Acc_CustomerUpdDisableLiveCS"))
            {
                objDb.AddInParameter("custid", DbType.Int32, request.CustId);
                objDb.AddInParameter("DisabledLiveCS", DbType.Boolean, request.IsDisabledLiveCasino);
                objDb.AddInParameter("EnableDownlinesLiveCS", DbType.Boolean, request.IsEnableLiveCasinoForDownline);
                objDb.AddInParameter("UserID", DbType.Int32, request.UserId);
                objDb.AddInParameter("subAccId", DbType.Int32, request.SubAccId);
                objDb.AddInParameter("ip", DbType.String, request.SignUpIp);
                objDb.AddInParameter("Language", DbType.String, request.Language);
                objDb.AddOutParameter("ErrCode", DbType.Int32, 4);
                objDb.AddOutParameter("ErrMsg", DbType.String, 4000);

                objDb.ExecuteNonQuery();

                var errorCode = objDb.GetParameterValue("ErrCode");
                errorCode = (errorCode is DBNull) ? string.Empty : Convert.ToString(errorCode);

                var errorMessage = objDb.GetParameterValue("ErrMsg");
                errorMessage = (errorMessage is DBNull) ? string.Empty : Convert.ToString(errorMessage);

                Result result = new Result((string)errorCode, (string)errorMessage);

                return result;
            }
        }

        public Result UpdateCustomerSyncInfo(InsertAccountRequest request)
        {
            using (var objDb = new ObjectDb("Acc_CustomerUpdSyncLiveCS"))
            {
                objDb.AddInParameter("custid", DbType.Int32, request.CustId);
                objDb.AddInParameter("syncLiveCS", DbType.Boolean, request.SyncLiveCasino);

                objDb.AddInParameter("UserID", DbType.Int32, request.UserId);
                objDb.AddInParameter("subAccId", DbType.Int32, request.SubAccId);
                objDb.AddInParameter("ip", DbType.String, request.SignUpIp);
                objDb.AddInParameter("Language", DbType.String, request.Language);
                objDb.AddOutParameter("ErrCode", DbType.Int32, 4);
                objDb.AddOutParameter("ErrMsg", DbType.String, 4000);

                objDb.ExecuteNonQuery();

                var errorCode = objDb.GetParameterValue("ErrCode");
                errorCode = (errorCode is DBNull) ? string.Empty : Convert.ToString(errorCode);

                var errorMessage = objDb.GetParameterValue("ErrMsg");
                errorMessage = (errorMessage is DBNull) ? string.Empty : Convert.ToString(errorMessage);

                Result result = new Result((string)errorCode, (string)errorMessage);

                return result;
            }
        }

        public string CustomerSelSiteByUserName(string username)
        {
            string sitename = string.Empty;
            using (ObjectDb objDB = new ObjectDb("Acc_CustomerSelSiteByUserName"))
            {
                objDB.AddInParameter("@username", DbType.String, username);

                DataSet ds = objDB.ExecuteDataSet();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        sitename = dt.Rows[0]["site"].ToString();
                    }
                }
            }
            return sitename;
        }
    }
}