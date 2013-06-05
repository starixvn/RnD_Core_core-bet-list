// <copyright file="GameSettingService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
//
// <summary>
//
// </summary>
//
// <history>
//  <change who="Anders" date="2012.01.11">Created</change>
//  <change who="Anders" date="2012.01.18">Implement update game setting</change>
//  <change who="Anders" date="2012.01.19">Replace key of dictionary from gametype to bettypeid</change>
//	<change who="anders.le" date="2012.2.1">Add multiple update function</change>
//	<change who="anders.le" date="2012.2.6">Add function single writelog</change>
//	<change who="anders.le" date="2012.2.13">Add InsertAccount</change>
// </history>

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fanex.Common.Helper;
using Fanex.LiveCasinoSetting.DataAccess;
using Fanex.LiveCasinoSetting.Model;
using Fanex.LiveCasinoSetting.Model.Log;
using Fanex.LiveCasinoSetting.Request;
using Fanex.Logging;

namespace Fanex.LiveCasinoSetting.Business
{
    public class GameSettingService : SettingServiceBase, IGameSettingService
    {
        private readonly IGameSettingDataAccess _gameSettingDAO;
        private readonly ISettingDataAccess _settingDAO;
        private static object lockerSync = new object();

        public GameSettingService(IGameSettingDataAccess gameSettingDAO = null
            , ISettingDataAccess settingDAO = null)
        {
            _gameSettingDAO = (gameSettingDAO) ?? new GameSettingDataAccess();
            _settingDAO = (settingDAO) ?? new SettingDataAccess();
        }

        #region IGameSettingService Members

        /// <summary>
        ///
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="exchangeId"></param>
        /// <returns></returns>
        public List<GameType> GetByCustomerId(int custId)
        {
            Check.Argument.IsNotNegativeOrZero(custId, "custId");
            List<GameType> gameTypeList = new List<GameType>();
            try
            {
                gameTypeList = _gameSettingDAO.GetByCustomerId(custId);
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }

            return gameTypeList;
        }

        /// <summary>
        /// get a list of gamesetting
        /// </summary>
        /// <param name="custIdList"></param>
        /// <returns>key is customerid</returns>
        public Dictionary<int, List<GameType>> GetByListCustomer(List<int> custIdList)
        {
            Check.Argument.IsNotNull(custIdList, "custIdList");
            Dictionary<int, List<GameType>> resultDic = new Dictionary<int, List<GameType>>();
            try
            {
                var result = _gameSettingDAO.GetByListCustomer(custIdList);
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }

            return resultDic;
        }

        public List<GameType> GetByExhangeId(int exchangeId, string site)
        {
            Check.Argument.IsNotNegativeOrZero(exchangeId, "exchangeId");
            List<GameType> gameTypeList = new List<GameType>();
            try
            {
                gameTypeList = _gameSettingDAO.GetByExchangeId(exchangeId, site);
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }

            return gameTypeList;
        }

        public Dictionary<int, Result> UpdateStakeLevel(int custId, int roleId, List<GameType> games)
        {
            return _gameSettingDAO.UpdateStakeLevel(custId, roleId, games);
        }

        /// <summary>
        /// update a list of game type
        /// dictionary of custid, result
        /// </summary>
        /// <param name="gameTypeList"></param>
        /// <param name="request"></param>
        /// <returns>return result list with key gametypeid,
        ///     string.IsNullOrWhiteSpace(result.ErrorCode) = true => update successfull
        /// </returns>
        public Dictionary<int, Result> UpdateGameSetting(int uplineRoleId, List<GameType> gameTypeList, InsertAccountRequest request)
        {
            Check.Argument.IsNotNull(gameTypeList, "gameTypeList");
            Check.Argument.IsNotNull(request, "request");
            Check.Argument.IsNotNegativeOrZero(request.CustId, "CustId");
            Check.Argument.IsNotNegativeOrZero(request.RoleId, "RoleId");
            Check.Argument.IsNotNegative(uplineRoleId, "uplineRoleId");

            var resultList = new Dictionary<int, Result>();

            lock (lockerSync)
            {
                try
                {
                    var custInfo = _settingDAO.GetCustomerInfo(request.CustId);
                    var result = new Result();

                    if (!custInfo.IsSyncLiveCasino && (uplineRoleId - request.RoleId == 1 || uplineRoleId == 0))//Sync direct downline only
                    {
                        result = InsertAccount(request);
                    }

                    if (string.IsNullOrWhiteSpace(result.ErrorCode) ||
                        result.ErrorCode == ErrorCode.NOT_UPDATE_PT_SETTING ||
                        result.ErrorCode == ErrorCode.NOT_UPDATE_BETTING_LIMIT)
                    {
                        resultList = _gameSettingDAO.UpdateStakeLevel(request.CustId, request.RoleId, gameTypeList);

                        if ((string.IsNullOrWhiteSpace(resultList[gameTypeList.ElementAt(0).BetTypeId].ErrorCode) ||
                            resultList[gameTypeList.ElementAt(0).BetTypeId].ErrorCode == ErrorCode.NOT_UPDATE_PT_SETTING ||
                            resultList[gameTypeList.ElementAt(0).BetTypeId].ErrorCode == ErrorCode.NOT_UPDATE_BETTING_LIMIT) &&
                            !custInfo.IsSyncLiveCasino && (uplineRoleId - request.RoleId == 1 || uplineRoleId == 0))
                        {
                            UpdateSyncLiveCasinoInfo(request);

                            //Write log manual insert account
                            var log = new LogWriterService();
                            var custList = new List<KeyValuePair<int, string>>();
                            custList.Add(new KeyValuePair<int, string>(request.CustId, request.CustUserName));

                            log.WriteLog
                                (
                                    new Fanex.LiveCasinoSetting.Model.Log.LogManualInsertAccount()
                                    {
                                        Action = request.RoleId == 4 ? LogActions.ManualInsert_Super :
                                                 request.RoleId == 3 ? LogActions.ManualInsert_Master :
                                                 request.RoleId == 2 ? LogActions.ManualInsert_Agent :
                                                                       LogActions.ManualInsert_Member,
                                        AgentId = request.AgentId,
                                        CustId = request.CustId,
                                        CustUserName = request.CustUserName,
                                        ExchangeId = request.ExchangeId,
                                        SignUpIp = request.SignUpIp,
                                        Language = request.Language,
                                        MasterId = request.MasterId,
                                        RoleId = request.RoleId,
                                        SignUpForm = request.SignUpForm,
                                        SubAccId = request.SubAccId,
                                        SubAccName = request.SubAccName,
                                        SuperId = request.SuperId,
                                        UserId = request.UserId,
                                        UserName = request.UserName,
                                        ErrorCode = result.ErrorCode,
                                        ErrorMessage = result.ErrorMessage,
                                        CustSite = request.CustSite,
                                        CustomerList = custList
                                    }
                                );
                        }
                    }
                }
                catch (Exception ex)
                {
                    ProcessException(ex);
                }
            }

            return resultList;
        }

        #endregion IGameSettingService Members

        /// <summary>
        /// multiple update gametype of a list customer
        /// </summary>
        /// <param name="custIdList"></param>
        /// <param name="roleId"></param>
        /// <param name="gameTypeList"></param>
        /// <returns>a dictionary of custid with value is dictionary of gametypeid and result object</returns>
        public Dictionary<int, Dictionary<int, Result>> UpdateGameSetting(List<int> custIdList, int uplineRoleId, List<GameType> gameTypeList, Dictionary<int, InsertAccountRequest> request)
        {
            Check.Argument.IsNotNull(custIdList, "custIdList");
            Check.Argument.IsNotNull(gameTypeList, "gameTypeList");
            Check.Argument.IsNotNull(request, "request");

            var resultList = new Dictionary<int, Dictionary<int, Result>>();

            try
            {
                custIdList.ForEach(custId =>
                    {
                        var result = UpdateGameSetting(uplineRoleId, gameTypeList, request[custId]);

                        resultList.Add(custId, result);
                    });
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }

            return resultList;
        }

        /// <summary>
        /// Auto insert account
        /// </summary>
        /// <param name="request"><see cref="InsertAccountRequest"/></param>
        /// <returns></returns>
        public Result InsertAccount(InsertAccountRequest request, ref DataTable dataResult)
        {
            Result result = new Result();
            try
            {
                Check.Argument.IsNotNull(request, "request");
                //should add validator later

                result = _gameSettingDAO.InsertAccount(request, ref dataResult);

                /*
                 * After auto insert successfully, perform update syncrhonize status
                 * and then update status of live casino follow upline status
                 */
                if (string.IsNullOrWhiteSpace(result.ErrorCode))
                {
                    if (request.IsAutoInsert)
                    {
                        UpdateSyncLiveCasinoInfo(request);
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
            return result;
        }

        public Result InsertAccountFromExisting(InsertAccountRequest request)
        {
            DataTable dataResult = null;
            Result result = new Result();

            try
            {
                Check.Argument.IsNotNull(request, "request");

                result = _gameSettingDAO.InsertAccountFromExisting(request, out dataResult);

                if (string.IsNullOrWhiteSpace(result.ErrorCode))
                {
                    UpdateSyncLiveCasinoInfo(request);

                    var log = new LogWriterService();

                    log.WriteLog
                        (
                            new Fanex.LiveCasinoSetting.Model.Log.LogInsertAccountFromExisting()
                            {
                                Action = request.RoleId == 3 ? LogActions.AutoInsertExisting_Master :
                                         request.RoleId == 2 ? LogActions.AutoInsertExisting_Agent :
                                                               LogActions.AutoInsertExisting_Member,
                                AgentId = request.AgentId,
                                CustId = request.CustId,
                                CustUserName = request.CustUserName,
                                ExchangeId = request.ExchangeId,
                                SignUpIp = request.SignUpIp,
                                Language = request.Language,
                                MasterId = request.MasterId,
                                RoleId = request.RoleId,
                                SignUpForm = request.SignUpForm,
                                SubAccId = request.SubAccId,
                                SubAccName = request.SubAccName,
                                SuperId = request.SuperId,
                                UserId = request.UserId,
                                UserName = request.UserName,
                                ErrorCode = result.ErrorCode,
                                ErrorMessage = result.ErrorMessage,
                                CopiedFrom = request.CopiedFrom,
                                dataResult = dataResult
                            }
                        );
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
            return result;
        }

        public void UpdateSyncLiveCasinoInfo(InsertAccountRequest request)
        {
            request.SyncLiveCasino = true;
            _settingDAO.UpdateCustomerSyncInfo(request);
            _settingDAO.UpdateStatusLiveCasino(new UpdateLiveCasinoStatusRequest(request));
        }

        /// <summary>
        /// Manually insert account
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Result InsertAccount(InsertAccountRequest request)
        {
            DataTable result = new DataTable();
            return InsertAccount(request, ref result);
        }
    }
}