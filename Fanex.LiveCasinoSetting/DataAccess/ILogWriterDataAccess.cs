//<copyright file="ILogWriterDataAccess.cs" company="Nexcel Solutions Vietnam" >
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
//</copyright>
//
//<history>
//	<change who="anders.le" date="2012.2.8">Created</change>
//</history>
namespace Fanex.LiveCasinoSetting.DataAccess
{
    using System;
    using System.Collections.Generic;
    using Fanex.LiveCasinoSetting.Request;
    using Fanex.LiveCasinoSetting.Model.Log;

    public interface ILogWriterDataAccess
    {
        //void WriteLog(WriteLogRequest request, List<string> oldValues, List<string> newValues);

        void WriteLog(ILogData logData);
    }
}
