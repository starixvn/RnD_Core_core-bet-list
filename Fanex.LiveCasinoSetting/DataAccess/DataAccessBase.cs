//<copyright file="DataAccessBase.cs" company="Nexcel Solutions Vietnam" >
//Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
//</copyright>
//
//<history>
//	<change who="anders.le" date="2012.1.30">Created</change>
//</history>

namespace Fanex.LiveCasinoSetting.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System.Data.Common;
    using Fanex.LiveCasinoSetting.Model;
    using System.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DataAccessBase
    {
        /// <summary>
        /// Add out put param for Execute Nonquery 
        /// Error Code and Error Messages
        /// </summary>
        /// <param name="db"></param>
        /// <param name="dbCommand"></param>
        public void AddResultParams(Database db, DbCommand dbCommand)
        {
            db.AddOutParameter(dbCommand, "ErrCode", DbType.String, 300);
            db.AddOutParameter(dbCommand, "ErrMsg", DbType.String, 4000);
        }

        public Result GetResultParams(Database db, DbCommand dbCommand)
        {
            var errorCode = db.GetParameterValue(dbCommand, "ErrCode");
            errorCode = (errorCode is DBNull) ? string.Empty : Convert.ToString(errorCode);

            var errorMessage = db.GetParameterValue(dbCommand, "ErrMsg");
            errorMessage = (errorMessage is DBNull) ? string.Empty : Convert.ToString(errorMessage);

            //get result message
            var result = new Result((string)errorCode, (string)errorMessage);
            
            return result;
        }
    }
}
