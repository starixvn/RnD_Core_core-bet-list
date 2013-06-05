// <copyright file="SettingService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
//
// <summary>
// Provide services to get customer info, PT setting, Stake Level Setting
// and moreover support to get ViewModel
// </summary>
//
// <history>
//  <change who="Anders" date="2012.01.8">Created</change>
//  <change who="Anders" date="2012.01.9">Add function GetCustomerInfo, and AdminGetSuperPT</change>
//  <change who="Anders" date="2012.01.9">Add function to get ViewModel</change>
//  <change who="Anders" date="2012.01.11">Seperate game setting to another service</change>
//  <change who="Anders" date="2012.01.12">Seperaete GetGameSetting to 2 functions GetByCustId, and GetByExhangeId</change>
//  <change who="Anders" date="2012.01.16">Implement SuperGetMemberSetting, MasterGetMemberSetting, and refactor code</change>
//	<change who="anders.le" date="2012.2.3">Check object.IsDirty before updating</change>
//	<change who="anders.le" date="2012.2.4">Explicit return and input data type instead of using IPTSetting</change>
//	<change who="anders.le" date="2012.2.9">Get uplince setting if current user is not sync</change>
//	<change who="anders.le" date="2012.2.10">InsertAccount</change>
// </history>

namespace Fanex.LiveCasinoSetting.Business
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DataAccess;
    using Fanex.Common.Helper;
    using Fanex.LiveCasinoSetting.Request;
    using Fanex.Logging;
    using Model;
    using ViewModel;

    /// <summary>
    /// <see cref="http://ccs.nexcel.vn/viewtopic.php?f=175&t=5879"/>
    /// </summary>
    public class SettingService : SettingServiceBase, ISettingService
    {
        private readonly ISettingDataAccess _settingDAO;

        public SettingService(ISettingDataAccess settingDAO = null)
        {
            _settingDAO = settingDAO ?? new SettingDataAccess();
        }

        #region QUERIES

        public CustomerInfo GetCustomerInfo(int custId)
        {
            Check.Argument.IsNotNegativeOrZero(custId, "custId");

            CustomerInfo custInfo = _settingDAO.GetCustomerInfo(custId);

            return custInfo;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="custId"></param>
        /// <returns></returns>
        public SuperPTSetting GetSuperPTSetting(int custId)
        {
            Check.Argument.IsNotNegativeOrZero(custId, "custId");
            SuperPTSetting ptSetting = null;
            try
            {
                ptSetting = _settingDAO.GetSuperPTSetting(custId);

                if (ptSetting.Upline == null)
                    throw new NullReferenceException("Upline PT is null");
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }

            return ptSetting;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="uplineId"></param>
        /// <returns></returns>
        public PTSetting GetMasterPTSeting(int custId, int uplineId)
        {
            Check.Argument.IsNotNegativeOrZero(custId, "custId");
            Check.Argument.IsNotNegative(uplineId, "uplineId");
            PTSetting ptSetting = null;

            try
            {
                ptSetting = _settingDAO.GetMasterPTSeting(custId, uplineId);

                if (ptSetting.Upline == null)
                    throw new NullReferenceException("Upline PT is null");
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }

            return ptSetting;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="uplineId"></param>
        /// <returns></returns>
        public PTSetting GetAgentPTSetting(int custId, int uplineId)
        {
            Check.Argument.IsNotNegativeOrZero(custId, "custId");
            Check.Argument.IsNotNegative(uplineId, "uplineId");
            PTSetting ptSetting = null;

            try
            {
                ptSetting = _settingDAO.GetAgentPTSetting(custId, uplineId);

                if (ptSetting.Upline == null)
                    throw new NullReferenceException("Upline PT is null");
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }

            return ptSetting;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="uplineId"></param>
        /// <returns></returns>
        public PTSetting GetMemberPTSetting(int custId, int uplineId)
        {
            Check.Argument.IsNotNegativeOrZero(custId, "custId");
            Check.Argument.IsNotNegative(uplineId, "uplineId");
            PTSetting ptSetting = null;
            try
            {
                ptSetting = _settingDAO.GetMemberPTSetting(custId, uplineId);

                if (ptSetting.Upline == null)
                    throw new NullReferenceException("Upline PT is null");
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }

            return ptSetting;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="uplineId"></param>
        /// <returns></returns>
        public PTSetting SuperGetMemberPTSetting(int custId, int uplineId)
        {
            Check.Argument.IsNotNegativeOrZero(custId, "custId");
            Check.Argument.IsNotNegative(uplineId, "uplineId");
            PTSetting ptSetting = null;
            try
            {
                ptSetting = _settingDAO.SuperGetMemberPTSetting(custId, uplineId);

                if (ptSetting.Upline == null || ptSetting.Upline.Upline == null || ptSetting.Upline.Upline.Upline == null)
                    throw new NullReferenceException("Upline PT is null");
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }

            return ptSetting;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="uplineId"></param>
        /// <returns></returns>
        public PTSetting MasterGetMemberPTSetting(int custId, int uplineId)
        {
            Check.Argument.IsNotNegativeOrZero(custId, "custId");
            Check.Argument.IsNotNegative(uplineId, "uplineId");
            PTSetting ptSetting = null;
            try
            {
                ptSetting = _settingDAO.MasterGetMemberPTSetting(custId, uplineId);

                if (ptSetting.Upline == null || ptSetting.Upline.Upline == null)
                    throw new NullReferenceException("Upline PT is null");
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }

            return ptSetting;
        }

        #region ViewModel

        /// <summary>
        ///
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="exchangeId"></param>
        /// <returns></returns>
        public SettingViewModel GetSuperSetting(int custId, int exchangeId = 0)
        {
            CustomerInfo custInfo = _settingDAO.GetCustomerInfo(custId);
            SettingViewModel viewModel = new SettingViewModel(custInfo);

            viewModel.PTSetting = GetSuperPTSetting(custId);
            string site = _settingDAO.CustomerSelSiteByUserName(custInfo.UserName);
            //maybe we change to user servie locator here
            IGameSettingService gameSettingService = new GameSettingService();
            viewModel.GameSetting = (exchangeId == 0)
                ? gameSettingService.GetByCustomerId(custId)
                : gameSettingService.GetByExhangeId(exchangeId, site);

            return viewModel;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="uplineId"></param>
        /// <returns></returns>
        public SettingViewModel GetMasterSetting(int custId, int uplineId)
        {
            SettingViewModel viewModel = CreateViewModel(custId, uplineId, GetMasterPTSeting);
            return viewModel;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="uplineId"></param>
        /// <returns></returns>
        public SettingViewModel GetAgentSetting(int custId, int uplineId)
        {
            SettingViewModel viewModel = CreateViewModel(custId, uplineId, GetAgentPTSetting);
            return viewModel;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="uplineId"></param>
        /// <returns></returns>
        public SettingViewModel GetMemberSetting(int custId, int uplineId)
        {
            SettingViewModel viewModel = CreateViewModel(custId, uplineId, GetMemberPTSetting);
            return viewModel;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="uplineId"></param>
        /// <returns></returns>
        public SettingViewModel SuperGetMemberSetting(int custId, int uplineId)
        {
            SettingViewModel viewModel = CreateViewModel(custId, uplineId, SuperGetMemberPTSetting);
            return viewModel;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="uplineId"></param>
        /// <returns></returns>
        public SettingViewModel MasterGetMemberSetting(int custId, int uplineId)
        {
            SettingViewModel viewModel = CreateViewModel(custId, uplineId, MasterGetMemberPTSetting);
            return viewModel;
        }

        private delegate IPTSetting GetPTSetting(int custId, int uplineId);

        private SettingViewModel CreateViewModel(int custId, int uplineId, GetPTSetting function)
        {
            CustomerInfo custInfo = GetCustomerInfo(custId);
            SettingViewModel viewModel = new SettingViewModel(custInfo);

            IGameSettingService gameSettingService = new GameSettingService();

            //get upline data if current data is null or empty
            viewModel.GameSetting = (custInfo.IsSyncLiveCasino)
                ? gameSettingService.GetByCustomerId(custId)
                : uplineId > 0 ? gameSettingService.GetByCustomerId(uplineId) : new List<GameType>();

            if (!custInfo.IsSyncLiveCasino)
            {
                viewModel.GameSetting.ForEach
                    (
                        o => o.BettingLimit.RemoveAll
                             (
                                g => !g.IsSelected
                             )
                    );
            }

            viewModel.PTSetting = function(custId, uplineId);
            return viewModel;
        }

        #endregion ViewModel

        #endregion QUERIES

        #region Update functions

        /// <summary>
        ///
        /// </summary>
        /// <param name="requestContext"></param>
        /// <param name="setting"></param>
        /// <returns></returns>
        public Result UpdateSuperPTSetting(IRequestContext requestContext, SuperPTSetting setting)
        {
            Result result = new Result();
            try
            {
                Check.Argument.IsNotNull(requestContext, "requestContext");
                Check.Argument.IsNotNull(setting, "setting");

                requestContext.IsEditSuper = true;
                //if (requestContext.IsValid && setting.IsValid)
                //{
                    if (!requestContext.IsSyncLiveCasino || setting.IsDirty)
                    {
                        result = _settingDAO.UpdateSuperPTSetting(requestContext, setting);
                    }
                    else
                    {
                        result = ReturnNoActionPerform();
                    }
                //}
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="requestContext"><paramref name="IRequestContext"/></param>
        /// <param name="setting"></param>
        /// <returns></returns>
        public Result UpdateMasterPTSetting(IRequestContext requestContext, PTSetting setting)
        {
            Result result = new Result();
            try
            {
                Check.Argument.IsNotNull(requestContext, "requestContext");
                Check.Argument.IsNotNull(setting, "setting");

                if (requestContext.IsValid && setting.IsValid)
                {
                    if (!requestContext.IsSyncLiveCasino || setting.IsDirty)
                    {
                        result = _settingDAO.UpdateMasterPTSetting(requestContext, setting);
                    }
                    else
                    {
                        result = ReturnNoActionPerform();
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="requestContext"></param>
        /// <param name="setting"></param>
        /// <param name="custIdList"></param>
        /// <returns></returns>
        public Dictionary<int, Result> UpdateMasterPTSetting(IRequestContext requestContext, PTSetting setting, List<int> custIdList)
        {
            Dictionary<int, Result> results = null;

            try
            {
                Check.Argument.IsNotNull(requestContext, "requestContext");
                Check.Argument.IsNotNull(setting, "setting");

                if (requestContext.IsValid && setting.IsValid)
                {
                    results = _settingDAO.UpdateMasterPTSetting(requestContext, setting, custIdList);
                }
            }
            catch (Exception exception)
            {
                ProcessException(exception);
            }

            return results;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="requestContext"><paramref name="IRequestContext"/></param>
        /// <param name="setting"></param>
        /// <returns></returns>
        public Result UpdateAgentPTSetting(IRequestContext requestContext, PTSetting setting)
        {
            Result result = new Result();
            try
            {
                Check.Argument.IsNotNull(requestContext, "requestContext");
                Check.Argument.IsNotNull(setting, "setting");

                if (requestContext.IsValid && setting.IsValid)
                {
                    if (!requestContext.IsSyncLiveCasino || setting.IsDirty)
                    {
                        result = _settingDAO.UpdateAgentPTSetting(requestContext, setting);
                    }
                    else
                    {
                        result = ReturnNoActionPerform();
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="requestContext"></param>
        /// <param name="setting"></param>
        /// <param name="custIdList"></param>
        /// <returns></returns>
        public Dictionary<int, Result> UpdateAgentPTSetting(IRequestContext requestContext, PTSetting setting, List<int> custIdList)
        {
            Dictionary<int, Result> results = null;
            try
            {
                Check.Argument.IsNotNull(requestContext, "requestContext");
                Check.Argument.IsNotNull(setting, "setting");

                if (requestContext.IsValid && setting.IsValid)
                {
                    results = _settingDAO.UpdateAgentPTSetting(requestContext, setting, custIdList);
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }

            return results;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="requestContext"><paramref name="IRequestContext"/></param>
        /// <param name="setting"></param>
        /// <returns></returns>
        public Result UpdateMemberPTSetting(IRequestContext requestContext, PTSetting setting)
        {
            Result result = new Result();
            try
            {
                Check.Argument.IsNotNull(requestContext, "requestContext");
                Check.Argument.IsNotNull(setting, "setting");

                if (requestContext.IsValid && setting.IsValid)
                {
                    if (!requestContext.IsSyncLiveCasino || setting.IsDirty)
                    {
                        result = _settingDAO.UpdateMemberPTSetting(requestContext, setting);
                    }
                    else
                    {
                        result = ReturnNoActionPerform();
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="requestContext"></param>
        /// <param name="setting"></param>
        /// <param name="custIdList"></param>
        /// <returns></returns>
        public Dictionary<int, Result> UpdateMemberPTSetting(IRequestContext requestContext, PTSetting setting, List<int> custIdList)
        {
            Dictionary<int, Result> results = null;
            try
            {
                Check.Argument.IsNotNull(requestContext, "requestContext");
                Check.Argument.IsNotNull(setting, "setting");

                if (requestContext.IsValid && setting.IsValid)
                {
                    results = _settingDAO.UpdateMemberPTSetting(requestContext, setting, custIdList);
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }

            return results;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="requestContext"><paramref name="IRequestContext"/></param>
        /// <param name="setting"></param>
        /// <returns></returns>
        public Result UpdateMemberPTSettingBySuper(IRequestContext requestContext, PTSetting setting)
        {
            Result result = new Result();
            try
            {
                Check.Argument.IsNotNull(requestContext, "requestContext");
                Check.Argument.IsNotNull(setting, "setting");

                if (requestContext.IsValid && setting.IsValid)
                {
                    if (setting.IsDirty)
                    {
                        result = _settingDAO.UpdateMemberPTSettingBySuper(requestContext, setting);
                    }
                    else
                    {
                        result = ReturnNoActionPerform();
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="requestContext"></param>
        /// <param name="setting"></param>
        /// <param name="custIdList"></param>
        /// <returns></returns>
        public Dictionary<int, Result> UpdateMemberPTSettingBySuper(IRequestContext requestContext, PTSetting setting, List<int> custIdList)
        {
            Dictionary<int, Result> results = null;
            try
            {
                Check.Argument.IsNotNull(requestContext, "requestContext");
                Check.Argument.IsNotNull(setting, "setting");

                if (requestContext.IsValid && setting.IsValid)
                {
                    results = _settingDAO.UpdateMemberPTSettingBySuper(requestContext, setting, custIdList);
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }

            return results;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="requestContext"><paramref name="IRequestContext"/></param>
        /// <param name="setting"></param>
        /// <returns></returns>
        public Result UpdateMemberPTSettingByMaster(IRequestContext requestContext, PTSetting setting)
        {
            Result result = new Result();
            try
            {
                Check.Argument.IsNotNull(requestContext, "requestContext");
                Check.Argument.IsNotNull(setting, "setting");

                if (requestContext.IsValid && setting.IsValid)
                {
                    if (setting.IsDirty)
                    {
                        result = _settingDAO.UpdateMemberPTSettingByMaster(requestContext, setting);
                    }
                    else
                    {
                        result = ReturnNoActionPerform();
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="requestContext"></param>
        /// <param name="setting"></param>
        /// <param name="custIdList"></param>
        /// <returns></returns>
        public Dictionary<int, Result> UpdateMemberPTSettingByMaster(IRequestContext requestContext, PTSetting setting, List<int> custIdList)
        {
            Dictionary<int, Result> results = null;
            try
            {
                Check.Argument.IsNotNull(requestContext, "requestContext");
                Check.Argument.IsNotNull(setting, "setting");

                if (requestContext.IsValid && setting.IsValid)
                {
                    results = _settingDAO.UpdateMemberPTSettingByMaster(requestContext, setting, custIdList);
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }

            return results;
        }

        /// <summary>
        /// Update live casino status
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Result UpdateStatusLiveCasino(UpdateLiveCasinoStatusRequest request)
        {
            Result result = new Result();
            try
            {
                Check.Argument.IsNotNull(request, "request");
                //should add validator later

                result = _settingDAO.UpdateStatusLiveCasino(request);
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
            return result;
        }

        public Result UpdateCustomerSyncInfo(UpdateCustomerSyncInfo request)
        {
            Result result = new Result();
            try
            {
                Check.Argument.IsNotNull(request, "request");
                //should add validator later

                result = _settingDAO.UpdateCustomerSyncInfo(request);
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }

            return result;
        }

        /// <summary>
        /// if there is no update action occur
        /// errorcode is CS0000
        /// </summary>
        /// <returns></returns>
        private Result ReturnNoActionPerform()
        {
            return new Result(ErrorCode.NOT_UPDATE_PT_SETTING, string.Empty);
        }

        #endregion Update functions
    }
}