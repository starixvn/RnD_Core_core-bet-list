// <copyright file="IGameSettingDataAccess.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved. 
// </copyright>
//
// <summary>
// Define methods which use to access ibc data
// </summary>
//
// <history>
//  <change who="anders.le" date="2012.01.8">Created</change>
//  <change who="anders.le" date="2012.01.12">Implement GetByCustomer, and GetByExchangeId</change>
//	<change who="anders.le" date="2012.1.31">Add multiple update StakeLevel</change>
//	<change who="anders.le" date="2012.2.6">Add function single writelog</change>
// </history>


using System.Collections.Generic;
using Fanex.LiveCasinoSetting.Model;
using System.Collections;
using Fanex.LiveCasinoSetting.Request;
using System.Data;

namespace Fanex.LiveCasinoSetting.DataAccess
{
    public interface IGameSettingDataAccess
    {
        List<GameType> GetByCustomerId(int custId);
        Dictionary<int, List<GameType>> GetByListCustomer(List<int> custIdList);
        List<GameType> GetByExchangeId(int exchangeId, string site);
        //Result UpdateStakeLevel(int custId, int gameTypeId, string stakeLevelList);

        /// <summary>
        /// Dictionary<betTypeId, Result>
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="gameTypeList"></param>
        /// <returns></returns>
        Dictionary<int, Result> UpdateStakeLevel(int custId, int roleId, List<GameType> gameTypeList);
        
        /// <summary>
        /// Dictionary<custid, Dictionary<betTypeId, Result>>
        /// </summary>
        /// <param name="custIdList"></param>
        /// <param name="gameTypeId"></param>
        /// <param name="stakeLevelList"></param>
        /// <returns></returns>
        Dictionary<int, Dictionary<int, Result>> UpdateStakeLevel(List<int> custIdList, int roleId, List<GameType> gameTypeList);

        Result InsertAccount(InsertAccountRequest request, ref DataTable dataResult);
        Result InsertAccountFromExisting(InsertAccountRequest request, out DataTable dataResult);
    }
}
