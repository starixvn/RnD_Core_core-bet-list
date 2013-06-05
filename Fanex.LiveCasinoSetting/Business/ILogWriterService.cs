//<copyright file="ILogWriterService.cs" company="Nexcel Solutions Vietnam" >
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
//</copyright>
//
//<history>
//	<change who="anders.le" date="2012.2.8">Created</change>
//</history>


namespace Fanex.LiveCasinoSetting.Business
{
    using System;
    using Fanex.LiveCasinoSetting.Request;
    using System.Collections.Generic;
    using Fanex.LiveCasinoSetting.Model;
    using Fanex.LiveCasinoSetting.Model.Log;

    public interface ILogWriterService
    {
        //void WriteLog(ILogData request, List<GameType> oldValues, List<GameType> newValues
        //    , Dictionary<int, Result> resultDictionary);

        //void WriteLog(ILogData request, List<GameType> oldValues, List<GameType> newValues
        //    , Dictionary<int, Dictionary<int, Result>> resultDictionary);

        void WriteLog(ILogData logData);
    }
}