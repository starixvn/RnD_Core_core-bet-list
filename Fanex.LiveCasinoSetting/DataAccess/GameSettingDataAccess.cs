// <copyright file="GameSettingDataAccess.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved. 
// </copyright>
//
// <summary>
// Define methods which use to access ibc data
// </summary>
//
// <history>
//  <change who="Anders" date="2012.01.8">Created</change>
//  <change who="Anders" date="2012.01.12">Implement GetByCustomer, and GetByExchangeId</change>
//	<change who="anders.le" date="2012.1.31">Update multiple stakelvel</change>
//	<change who="anders.le" date="2012.2.1">Modify update function</change>
//	<change who="anders.le" date="2012.2.6">Add function single writelog</change>
// </history>


using System;
using System.Collections.Generic;
using Fanex.LiveCasinoSetting.Model;
using Fanex.Data;
using System.Data;
using System.Linq;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;
using System.Threading.Tasks;
using Fanex.LiveCasinoSetting.Request;
using Fanex.Common.Extensions;

namespace Fanex.LiveCasinoSetting.DataAccess
{
    using Microsoft.Practices.ObjectBuilder2;

    public class GameSettingDataAccess : DataAccessBase, IGameSettingDataAccess
    {
        #region IGameSettingDataAccess Members

        public List<GameType> GetByCustomerId(int custId)
        {
            using (ObjectDb objDb = new ObjectDb("TP_Admin_StakeLevel_GetCustStakeLevelMulti"))
            {
                List<GameType> gameSetting = new List<GameType>();
                objDb.AddInParameter("CustId", DbType.Int32, custId);
                using (var reader = objDb.ExecuteReader())
                {
                    if (reader != null)
                    {
                        int prevGameTypeId = -1;
                        GameType gameType = null;
                        List<StakeLevel> stakeLevelList = new List<StakeLevel>();

                        while (reader.Read())
                        {
                            var gameTypeId = reader.GetValue<int>("GameTypeID");
                            if (prevGameTypeId != gameTypeId)
                            {
                                gameType = new GameType();
                                gameType.BetTypeId = reader.To<int>("IBCBetTypeID");
                                gameType.BettingLimit = new List<StakeLevel>();
                                gameSetting.Add(gameType);
                                prevGameTypeId = gameTypeId;
                            }

                            gameType.Id = gameTypeId;
                            StakeLevel stakeLevel = new StakeLevel();
                            stakeLevel.Id = reader.GetValue<int>("StakeLevelID");
                            stakeLevel.Text = reader.GetValue<string>("StakeLevelName");
                            stakeLevel.IsSelected = reader.GetValue<bool>("IsChecked");

                            gameType.BettingLimit.Add(stakeLevel);
                        }
                    }
                    return gameSetting;
                }
            }
        }

        //not complate yet
        public Dictionary<int, List<GameType>> GetByListCustomer(List<int> custIdList)
        {
            using (ObjectDb objDb = new ObjectDb("TP_Admin_StakeLevel_GetCustStakeLevelMulti"))
            {

                var resultDic = new Dictionary<int, List<GameType>>();
                objDb.AddInParameter("CustId", DbType.Int32);
                custIdList.ForEach(custId
                    =>
                    {
                        List<GameType> gameSetting = new List<GameType>();
                        objDb.SetParameterValue("CustId", custId);
                        //using (var reader = objDb.ExecuteReader())
                        var reader = objDb.ExecuteReader();
                        {
                            if (reader != null)
                            {
                                int prevGameTypeId = -1;
                                GameType gameType = null;
                                List<StakeLevel> stakeLevelList = new List<StakeLevel>();

                                while (reader.Read())
                                {
                                    int gameTypeId = reader.GetValue<int>("GameTypeID");
                                    if (prevGameTypeId != gameTypeId)
                                    {
                                        gameType = new GameType();
                                        gameType.BetTypeId = reader.ToInt("IBCBetTypeID");
                                        gameType.BettingLimit = new List<StakeLevel>();
                                        gameSetting.Add(gameType);
                                        prevGameTypeId = gameTypeId;
                                    }

                                    gameType.Id = gameTypeId;
                                    StakeLevel stakeLevel = new StakeLevel();
                                    stakeLevel.Id = reader.GetValue<int>("StakeLevelID");
                                    stakeLevel.Text = reader.GetValue<string>("StakeLevelName");
                                    stakeLevel.IsSelected = reader.GetValue<bool>("IsChecked");

                                    gameType.BettingLimit.Add(stakeLevel);
                                }
                            }
                        }

                        resultDic.Add(custId, gameSetting);

                    });

                return resultDic;
            }
        }

        public List<GameType> GetByExchangeId(int exchangeId, string site)
        {
            List<GameType> gameSetting = new List<GameType>();
            using (ObjectDb objDb = new ObjectDb("TP_Admin_StakeLevel_GetGameTypeStakeLevel"))
            {
                objDb.AddInParameter("ExchangeId", DbType.Int32, exchangeId);
                objDb.AddInParameter("site", DbType.String, site);
                using (var reader = objDb.ExecuteReader())
                {
                    if (reader != null)
                    {
                        int prevGameTypeId = -1;
                        GameType gameType = null;
                        List<StakeLevel> stakeLevelList = new List<StakeLevel>();

                        while (reader.Read())
                        {
                            int gameTypeId = reader.GetValue<int>("GameTypeID");
                            if (prevGameTypeId != gameTypeId)
                            {
                                gameType = new GameType();
                                gameType.BetTypeId = reader.ToInt("IBCBetTypeID");
                                gameType.BettingLimit = new List<StakeLevel>();
                                gameSetting.Add(gameType);
                                gameType.Id = gameTypeId;
                                prevGameTypeId = gameTypeId;
                            }

                            StakeLevel stakeLevel = new StakeLevel();
                            stakeLevel.Id = reader.GetValue<int>("StakeLevelID");
                            stakeLevel.Text = reader.GetValue<string>("StakeLevelName");
                            stakeLevel.IsSelected = true;

                            gameType.BettingLimit.Add(stakeLevel);
                        }
                    }
                }
            }

            return gameSetting;
        }

        //public Result UpdateStakeLevel(int custId, int gameTypeId, string stakeLevelList)
        //{
        //    using (ObjectDb objDb = new ObjectDb("TP_User_CustGameType_UpdStakeLevel"))
        //    {
        //        objDb.AddInParameter("CustId", DbType.Int32, custId);
        //        objDb.AddInParameter("GameTypeID", DbType.Int32, gameTypeId);
        //        objDb.AddInParameter("StakeLevelList", DbType.String, stakeLevelList);
        //        objDb.AddOutParameter("ErrCode", DbType.String, 5);
        //        objDb.AddOutParameter("ErrMsg", DbType.String, 3000);

        //        objDb.ExecuteNonQuery();

        //        Result result = GetResultParams(objDb.Database, objDb.DbCommand);

        //        return result;
        //    }
        //}

        public Dictionary<int, Result> UpdateStakeLevel(int custId, int roleId, List<GameType> gameTypeList)
        {
            using (ObjectDb objDb = new ObjectDb("TP_User_CustGameType_UpdStakeLevel"))
            {
                Dictionary<int, Result> resultList = new Dictionary<int, Result>();

                objDb.AddInParameter("CustId", DbType.Int32, custId);
                objDb.AddInParameter("RoleId", DbType.Int32, roleId);
                objDb.AddInParameter("GameTypeID", DbType.Int32);
                objDb.AddInParameter("StakeLevelList", DbType.String);
                objDb.AddOutParameter("ErrCode", DbType.String, 5);
                objDb.AddOutParameter("ErrMsg", DbType.String, 3000);

                foreach (var gameType in gameTypeList)
                {
                    Result result = null;
                    if (gameType.IsDirty)
                    {
                        objDb.SetParameterValue("GameTypeID", gameType.Id);
                        string stakeLevelString = GetStakeLevelIdList(gameType.BettingLimit);
                        objDb.SetParameterValue("StakeLevelList", stakeLevelString);

                        objDb.ExecuteNonQuery();
                        result = GetResultParams(objDb.Database, objDb.DbCommand);
                    }
                    else
                    {
                        //not perform update action
                        result = new Result(ErrorCode.NOT_UPDATE_BETTING_LIMIT, string.Empty);
                    }

                    resultList.Add(gameType.BetTypeId + gameType.Id, result);
                }

                return resultList;
            }
        }

        public Dictionary<int, Dictionary<int, Result>> UpdateStakeLevel(List<int> custIdList, int roleId, List<GameType> gameTypeList)
        {
            using (ObjectDb objDb = new ObjectDb("TP_User_CustGameType_UpdStakeLevel"))
            {
                objDb.AddInParameter("CustId", DbType.Int32);
                objDb.AddInParameter("RoleId", DbType.Int32, roleId);
                objDb.AddInParameter("GameTypeID", DbType.Int32);
                objDb.AddInParameter("StakeLevelList", DbType.String);
                objDb.AddOutParameter("ErrCode", DbType.String, 5);
                objDb.AddOutParameter("ErrMsg", DbType.String, 3000);

                Dictionary<int, Dictionary<int, Result>> resultList = new Dictionary<int, Dictionary<int, Result>>();
                custIdList.ForEach(custId =>
                {
                    objDb.SetParameterValue("CustId", custId);

                    Dictionary<int, Result> gameTypeResult = new Dictionary<int, Result>();
                    foreach (var gameType in gameTypeList)
                    {
                        objDb.SetParameterValue("GameTypeID", gameType.Id);
                        string stakeLevelString = GetStakeLevelIdList(gameType.BettingLimit);
                        objDb.SetParameterValue("StakeLevelList", stakeLevelString);

                        objDb.ExecuteNonQuery();
                        Result result = GetResultParams(objDb.Database, objDb.DbCommand);

                        gameTypeResult.Add(gameType.Id, result);
                    }

                    resultList.Add(custId, gameTypeResult);

                });

                return resultList;
            }
        }

        private DataTable GetCustStakeLevelByCustId(int custId)
        {
            DataTable dataTable = new DataTable();
            using (var objDb = new ObjectDb("TP_Admin_StakeLevel_GetCustStakeLevel"))
            {
                objDb.AddInParameter("CustId", DbType.Int32, custId);
                var ds = objDb.ExecuteDataSet();
                if (ds != null && ds.Tables.Count > 0)
                {
                    dataTable = ds.Tables[0];
                }
            }

            return dataTable;
        }

        public Result InsertAccount(InsertAccountRequest request, ref DataTable dataResult)
        {
            string spName = (request.IsAutoInsert)
                ? "TP_User_Customer_AutoInsertAccount"
                : "TP_User_Customer_ManualInsertAccount";
                        
            using (var objDb = new ObjectDb(spName))
            {
                objDb.AddInParameter("CustId", DbType.Int32, request.CustId);
                objDb.AddInParameter("CasinoNo", DbType.String, "01");
                objDb.AddInParameter("ExchangeID", DbType.Int32, request.ExchangeId);
                objDb.AddInParameter("SCustID", DbType.Int32, request.SuperId);
                objDb.AddInParameter("MCustID", DbType.Int32, request.MasterId);
                objDb.AddInParameter("ACustID", DbType.Int32, request.AgentId);
                objDb.AddInParameter("RoleID", DbType.Int32, request.RoleId);
                objDb.AddInParameter("SignupIP", DbType.String, request.SignUpIp);
                objDb.AddInParameter("SignupBy", DbType.Int32, request.UserId);
                objDb.AddInParameter("SignupFrom", DbType.Int32, request.SignUpForm);
                objDb.AddInParameter("ModifyUserName", DbType.String, request.UserName);
                objDb.AddInParameter("UserName", DbType.String, request.CustUserName);
                objDb.AddOutParameter("ErrCode", DbType.String, 5);
                objDb.AddOutParameter("ErrMsg", DbType.String, 500);
                if(!string.IsNullOrEmpty(request.CustSite))
                {
                    objDb.AddInParameter("CustSite", DbType.String, request.CustSite);
                }

                if (request.IsAutoInsert)
                {
                    var ds = objDb.ExecuteDataSet();
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        dataResult = ds.Tables[0];
                    }
                    else
                    {
                        //Because replication not finish yet, need to call GetCustStakeLevelByCustId again to get data for writting log. 
                        for (int i = 0; i < 5; i++)
                        {
                            System.Threading.Thread.Sleep(1000);
                            dataResult = GetCustStakeLevelByCustId(request.CustId);
                            if(dataResult.Rows.Count>0)
                                break;
                        }
                    }
                }
                else
                {
                    objDb.ExecuteNonQuery();
                }

                Result result = GetResultParams(objDb.Database, objDb.DbCommand);

                return result;
            }
        }
        /// <summary>
        /// This function is used for search and copy on agent site only
        /// </summary>
        /// <param name="request"></param>
        /// <param name="dataResult"></param>
        /// <returns></returns>
        public Result InsertAccountFromExisting(InsertAccountRequest request, out DataTable dataResult)
        {
            dataResult = null;
            
            string spName = "TP_User_Customer_AutoInsertAccountFromExisting";

            using (var objDb = new ObjectDb(spName))
            {
                objDb.AddInParameter("CustID", DbType.Int32, request.CustId);
                objDb.AddInParameter("OldCustID", DbType.Int32, request.OldCustId);
                objDb.AddInParameter("CasinoNo", DbType.String, "01");
                objDb.AddInParameter("SignupIP", DbType.String, request.SignUpIp);
                objDb.AddInParameter("SignupBy", DbType.Int32, request.UserId);
                objDb.AddInParameter("SignupFrom", DbType.Int32, request.SignUpForm);
                objDb.AddInParameter("ModifyUserName", DbType.String, request.UserName);
                objDb.AddInParameter("ToUserName", DbType.String, request.CustUserName);
                objDb.AddOutParameter("ErrCode", DbType.String, 5);
                objDb.AddOutParameter("ErrMsg", DbType.String, 500);
                                
                objDb.ExecuteNonQuery();

                for (int i = 0; i < 5; i++)
                {
                    System.Threading.Thread.Sleep(1000);
                    dataResult = GetCustStakeLevelByCustId(request.CustId);

                    if (dataResult.Rows.Count > 0)
                        break;
                }

                Result result = GetResultParams(objDb.Database, objDb.DbCommand);

                return result;
            }
        }

        /// <summary>
        /// get id list string
        /// </summary>
        /// <param name="stakeLevelList">list of id sepereted by commas</param>
        /// <returns></returns>
        private string GetStakeLevelIdList(List<StakeLevel> stakeLevelList)
        {
            string IdList = string.Empty;

            //get selected stakelevel id
            var stakeLevelListSelected = stakeLevelList
                .Where(s => s.IsSelected);

            if (stakeLevelListSelected != null)
            {
                stakeLevelListSelected.ForEach(s => IdList += s.Id.ToString() + ",");
            }

            return IdList.TrimEnd(',');
        }

        #endregion
    }
}
