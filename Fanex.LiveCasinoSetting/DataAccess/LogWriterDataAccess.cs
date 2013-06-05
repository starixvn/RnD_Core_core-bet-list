//<copyright file="LogWriterDataAccess.cs" company="Nexcel Solutions Vietnam" >
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
    using System.Linq;
    using System.Text;
    using Fanex.Data;
    using Fanex.LiveCasinoSetting.Request;
    using System.Data;
    using Fanex.LiveCasinoSetting.Model.Log;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class LogWriterDataAccess : ILogWriterDataAccess
    {
        //public void WriteLog(WriteLogRequest request, List<string> oldValues, List<string> newValues)
        //{
        //    using (ObjectDb objDb = new ObjectDb("WriteLog_UserLog_RunSP"))
        //    {
        //        objDb.AddInParameter("UserId", DbType.Int32, request.UserId);
        //        objDb.AddInParameter("CustId", DbType.Int32, request.CustId);
        //        objDb.AddInParameter("SpName", DbType.String, request.SpNameTag);
        //        objDb.AddInParameter("Doing", DbType.String);
        //        objDb.AddInParameter("ActionId", DbType.Int32, request.ActionId);
        //        objDb.AddInParameter("TrackingValue", DbType.String);
        //        objDb.AddInParameter("TrackingValue2", DbType.String);
        //        objDb.AddInParameter("Duration", DbType.Int32);

        //        int count = oldValues.Count;

        //        for (int i = 0; i < count; i++)
        //        {
        //            objDb.SetParameterValue("TrackingValue", newValues[i]);
        //            objDb.SetParameterValue("TrackingValue2", oldValues[i]);

        //            objDb.ExecuteNonQuery();
        //        }
        //    }
        //}

        public void WriteLog(ILogData logData)
        {
            using (ObjectDb objDb = new ObjectDb("WriteLog_UserLog_RunSP"))
            {
                objDb.AddInParameter("UserId", DbType.Int32, logData.UserId);
                objDb.AddInParameter("CustId", DbType.Int32, logData.CustId);
                objDb.AddInParameter("SpName", DbType.String, logData.SpNameTag);
                objDb.AddInParameter("Doing", DbType.String);
                objDb.AddInParameter("ActionId", DbType.Int32, logData.ActionId);
                objDb.AddInParameter("TrackingValue", DbType.String);
                objDb.AddInParameter("TrackingValue2", DbType.String);
                objDb.AddInParameter("Duration", DbType.Int32);

                int count = logData.NewValues.Count;                

                var oldValues = logData.OldValues;
                var newValues = logData.NewValues;

                for (int i = 0; i < count; i++)
                {
                    objDb.SetParameterValue("TrackingValue", newValues.Count > i ? (string.IsNullOrWhiteSpace(newValues[i]) ? null : newValues[i]) : null); 
                    objDb.SetParameterValue("TrackingValue2", oldValues.Count > i ? (string.IsNullOrWhiteSpace(oldValues[i]) ? null : oldValues[i]) : null);

                    objDb.ExecuteNonQuery();
                }
            }
        }
    }
}
