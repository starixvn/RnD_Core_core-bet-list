using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Collections;
using Fanex.Data.Dapper;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
namespace Fanex.Data
{
    internal static class Util
    {

        #region Util functions
        public static DynamicParameters AssignDynamicParamsValue(ObjectDB objectDB, params object[] parameters)
        {
            DynamicParameters dynamicParams = new DynamicParameters();
            if (!ReferenceEquals(parameters, null))
            {
                int i = 0;
                foreach (SqlParameter param in objectDB.DbCommand.Parameters)
                {
                    if (param.IsNotReturnValue())
                    {
                        dynamicParams.Add(param.ParameterName
                            , parameters.GetValue(i++)
                            , null, param.Direction
                            , param.Size);
                    }
                }
            }
            return dynamicParams;
        }
        public static void CheckInputValid(string settingKey)
        {
            if (string.IsNullOrWhiteSpace(settingKey))
            {
                throw new ArgumentNullException("settingKey");
            }
        }
        #endregion
    }
}
