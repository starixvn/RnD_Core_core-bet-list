//<copyright file="LogWriterService.cs" company="Nexcel Solutions Vietnam" >
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
//</copyright>
//
//<history>
//	<change who="anders.le" date="2012.2.8">Created</change>
//	<change who="anders.le" date="2012.2.13">remove writelogrequest param</change>
//</history>
namespace Fanex.LiveCasinoSetting.Business
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.LiveCasinoSetting.Request;
    using Fanex.Common.Helper;
    using Fanex.LiveCasinoSetting.Model;
    using Fanex.LiveCasinoSetting.DataAccess;
    using Fanex.LiveCasinoSetting.Model.Log;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class LogWriterService : SettingServiceBase, ILogWriterService
    {
        private ILogWriterDataAccess _logWriterDataAccess;
        public LogWriterService(ILogWriterDataAccess logWriterDataAccess = null)
        {
            _logWriterDataAccess = logWriterDataAccess ?? new LogWriterDataAccess();
        }

        public void WriteLog(ILogData logData)
        {
            Check.Argument.IsNotNull(logData, "logData");

            try
            {
                _logWriterDataAccess.WriteLog(logData);
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }       
    }
}
