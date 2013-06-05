// <copyright file="ISettingDataAccess.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
//
// <summary>
// Define methods which use to access ibc data
// </summary>
//
// <history>
//  <change who="Anders" date="2012.01.8">Created</change>
//  <change who="Anders" date="2012.01.10">Add function to get ViewModel</change>
//	<change who="anders.le" date="2012.2.4">Explicit return and input data type instead of using IPTSetting</change>
// </history>

using System.Collections.Generic;
using Fanex.LiveCasinoSetting.Model;
using Fanex.LiveCasinoSetting.Request;

namespace Fanex.LiveCasinoSetting.DataAccess
{
    public interface ISettingDataAccess
    {
        CustomerInfo GetCustomerInfo(int custId);

        SuperPTSetting GetSuperPTSetting(int custId);

        PTSetting GetMasterPTSeting(int custId, int uplineId);

        PTSetting GetAgentPTSetting(int custId, int uplineId);

        PTSetting GetMemberPTSetting(int custId, int uplineId);

        PTSetting SuperGetMemberPTSetting(int custId, int uplineId);

        PTSetting MasterGetMemberPTSetting(int custId, int uplineId);

        Result UpdateStatusLiveCasino(UpdateLiveCasinoStatusRequest request);

        Result UpdateCustomerSyncInfo(InsertAccountRequest request);

        Result UpdateSuperPTSetting(IRequestContext requestContext, SuperPTSetting setting);

        Result UpdateMasterPTSetting(IRequestContext requestContext, PTSetting setting);

        Result UpdateAgentPTSetting(IRequestContext requestContext, PTSetting setting);

        Result UpdateMemberPTSetting(IRequestContext requestContext, PTSetting setting);

        Result UpdateMemberPTSettingBySuper(IRequestContext requestContext, PTSetting setting);

        Result UpdateMemberPTSettingByMaster(IRequestContext requestContext, PTSetting setting);

        Dictionary<int, Result> UpdateMasterPTSetting(IRequestContext requestContext, PTSetting setting, List<int> custIdList);

        Dictionary<int, Result> UpdateAgentPTSetting(IRequestContext requestContext, PTSetting setting, List<int> custIdList);

        Dictionary<int, Result> UpdateMemberPTSetting(IRequestContext requestContext, PTSetting setting, List<int> custIdList);

        Dictionary<int, Result> UpdateMemberPTSettingBySuper(IRequestContext requestContext, PTSetting setting, List<int> custIdList);

        Dictionary<int, Result> UpdateMemberPTSettingByMaster(IRequestContext requestContext, PTSetting setting, List<int> custIdList);

        string CustomerSelSiteByUserName(string username);
    }
}