// <copyright file="IGameSettingService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved. 
// </copyright>
//
// <summary>
// Provide services to get customer info, PT setting, Stake Level Setting
// and moreover support to get ViewModel
// </summary>
//
// <history>
//  <change who="Anders" date="2012.01.11">Created</change>
//  <change who="Anders" date="2012.01.12">Seperaete GetGameSetting to 2 functions GetByCustId, and GetByExhangeId</change>
//  <change who="Anders" date="2012.01.19">Remove Update Gametype function</change>
//	<change who="anders.le" date="2012.2.1">Update multiple</change>
//	<change who="anders.le" date="2012.2.6">Add function single writelog</change>
// </history>


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fanex.LiveCasinoSetting.Model;
using Fanex.LiveCasinoSetting.Request;
using System.Data;

namespace Fanex.LiveCasinoSetting.Business
{
    public interface IGameSettingService
    {
        List<GameType> GetByCustomerId(int custId);
        Dictionary<int, List<GameType>> GetByListCustomer(List<int> custIdList);
        List<GameType> GetByExhangeId(int exchangeId, string site);      
        Dictionary<int, Result> UpdateGameSetting(int uplineRoleId, List<GameType> gameTypeList, InsertAccountRequest request);
        Dictionary<int,  Dictionary<int, Result>> UpdateGameSetting(List<int> custIdList, int uplineRoleId, List<GameType> gameTypeList, Dictionary<int, InsertAccountRequest> request);
        Result InsertAccount(InsertAccountRequest request);
        Result InsertAccount(InsertAccountRequest request, ref DataTable dataResult);       
    }
}
