// <copyright file="ISettingService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
//
// <summary>
// </summary>
//
// <history>
//  <change who="Anders" date="2012.01.8">Created</change>
//  <change who="Anders" date="2012.01.10">Remove Admin functions</change>
//  <change who="Anders" date="2012.01.11">Change return param from PTSetting to IPTSetting</change>
//	<change who="anders.le" date="2012.2.4">Explicit return and input data type instead of using IPTSetting</change>
// </history>
namespace Fanex.LiveCasinoSetting.Business
{
    using System.Collections.Generic;
    using Fanex.LiveCasinoSetting.Request;
    using Model;
    using ViewModel;

    public interface ISettingService
    {
        #region QUERIES

        CustomerInfo GetCustomerInfo(int custId);

        SuperPTSetting GetSuperPTSetting(int custId);

        PTSetting GetMasterPTSeting(int custId, int uplineId);

        PTSetting GetAgentPTSetting(int custId, int uplineId);

        PTSetting GetMemberPTSetting(int custId, int uplineId);

        PTSetting SuperGetMemberPTSetting(int custId, int uplineId);

        PTSetting MasterGetMemberPTSetting(int custId, int uplineId);

        SettingViewModel GetSuperSetting(int custId, int exchangeId = 0);

        SettingViewModel GetMasterSetting(int custId, int uplineId);

        SettingViewModel GetAgentSetting(int custId, int uplineId);

        SettingViewModel GetMemberSetting(int custId, int uplineId);

        SettingViewModel SuperGetMemberSetting(int custId, int uplineId);

        SettingViewModel MasterGetMemberSetting(int custId, int uplineId);

        #endregion QUERIES

        #region COMMANDS

        Result UpdateSuperPTSetting(IRequestContext requestContext, SuperPTSetting setting);

        Result UpdateMasterPTSetting(IRequestContext requestContext, PTSetting setting);

        Result UpdateAgentPTSetting(IRequestContext requestContext, PTSetting setting);

        Result UpdateMemberPTSetting(IRequestContext requestContext, PTSetting setting);

        Result UpdateMemberPTSettingBySuper(IRequestContext requestContext, PTSetting setting);

        Result UpdateMemberPTSettingByMaster(IRequestContext requestContext, PTSetting setting);

        Dictionary<int, Result> UpdateMasterPTSetting(IRequestContext requestContext, PTSetting setting, List<int> custIdList);

        Dictionary<int, Result> UpdateAgentPTSetting(IRequestContext requestContext, PTSetting setting, List<int> custIdList);

        Dictionary<int, Result> UpdateMemberPTSettingByMaster(IRequestContext requestContext, PTSetting setting, List<int> custIdList);

        Dictionary<int, Result> UpdateMemberPTSettingBySuper(IRequestContext requestContext, PTSetting setting, List<int> custIdList);

        //Result InsertAccount(InsertAccountRequest request);
        Result UpdateStatusLiveCasino(UpdateLiveCasinoStatusRequest request);

        Result UpdateCustomerSyncInfo(UpdateCustomerSyncInfo request);

        #endregion COMMANDS
    }
}