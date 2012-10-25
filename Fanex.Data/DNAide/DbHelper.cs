// <copyright file="DbHelper.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved. 
// </copyright>
//
// <summary>
// Provides data access methods through stored procedure
// </summary>
//
// <history>
//  <change who="Anders" date="2011.12.24">Clone form DNAide Lib</change>
//  <change who="Anders" date="2011.12.26">Implement ExcuteNonQuery, and ExecuteScalar</change>
//  <change who="Anders" date="2011.12.28">Add query mapping DTO</change>
//  <change who="Edward" date="2011.12.30">Implement ExecuteDataTable</change>
//  <change who="Anders" date="2011.12.30">Add parameterCache, and ExecuteDBResult methods, and 2 more functions for each section</change>
//  <change who="Anders" date="2011.12.30">Add ExecuteDBResult, and 2 more functions for each section</change>
//  <change who="Edward" date="2011.12.30">Add CheckInputValid, Add AssignDynamicParamsValue with param objet[]</change>
//  <change who="Edward" date="2011.12.30">Implement QueryEntity</change>
//  <change who="Anders" date="2012.01.04">Refactor - Remove check key null or whitespace</change>
//  <change who="Anders" date="2012.01.05">Add Paramater Cache</change>
// </history>
namespace Fanex.Data.DNAide
{
    using System;
    using System.Collections;
    using System.Data;
    using System.Data.SqlClient;
    using System.Data.Common;
    using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
    using System.Collections.Generic;
    using Fanex.Data.Dapper;
    using Fanex.Data.Configuration;
    using Fanex.Common.Extensions;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Provides data access methods through stored procedure
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public static class DbHelper
    {
        /// <summary>
        /// Default time out is 120s
        /// </summary>
        private const int TimeOutDefault = 120;

        #region Execute return DBResult
        /// <summary>
        /// Execute
        /// </summary>
        /// <param name="settingKey"></param>
        /// <returns></returns>
        [Obsolete("Use ExecuteDbResult instead")]
        public static DbResult Execute(string settingKey)
        {
            return ExecuteDbResult(settingKey, TimeOutDefault);
        }

        /// <summary>
        /// Execute
        /// </summary>
        /// <param name="settingKey"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [Obsolete("Use ExecuteDbResult instead")]
        public static DbResult Execute(string settingKey, int timeOut)
        {
            return ExecuteDbResult(settingKey, null, timeOut);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settingKey"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [Obsolete("Use ExecuteDbResult instead")]
        public static DbResult Execute(string settingKey, Hashtable parameters)
        {
            return ExecuteDbResult(settingKey, parameters, TimeOutDefault);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settingKey"></param>
        /// <param name="parameters"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [Obsolete("Use ExecuteDbResult instead")]
        public static DbResult Execute(string settingKey, Hashtable parameters, int timeOut)
        {
            return ExecuteDbResult(settingKey, parameters, timeOut);
        }
        #endregion

        #region ExecuteDbResult

        /// <summary>
        /// <see cref=" ExecuteDbResult(string settingKey, int timeOut)"/>
        /// </summary>
        /// <param name="settingKey"></param>
        /// <returns></returns>
        public static DbResult ExecuteDbResult(string settingKey)
        {
            return ExecuteDbResult(settingKey, TimeOutDefault);
        }

        /// <summary>
        /// <see cref="ExecuteDbResult(string settingKey, Hashtable parameters, int timeOut)"/>
        /// </summary>
        /// <param name="settingKey"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static DbResult ExecuteDbResult(string settingKey, int timeOut)
        {
            return ExecuteDbResult(settingKey, null, timeOut);
        }

        /// <summary>
        /// <see cref="ExecuteDbResult(string settingKey, Hashtable parameters, int timeOut)"/>
        /// </summary>
        /// <param name="settingKey"></param>
        /// <param name="parameterValues"></param>
        /// <returns></returns>
        public static DbResult ExecuteDbResult(string settingKey, Hashtable parameterValues)
        {
            return ExecuteDbResult(settingKey, parameterValues, TimeOutDefault);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settingKey">db setting key</param>
        /// <param name="parameterValues">hash table parameter, the key must be lower case</param>
        /// <param name="timeOut">second unit</param>
        /// <returns><see cref="DbResult"/></returns>
        public static DbResult ExecuteDbResult(string settingKey, Hashtable parameterValues, int timeOut)
        {
            DbResult result = null;
            using (ObjectDb objectDb = new ObjectDb(settingKey))
            {
                var db = objectDb.Database;
                var dbCommand = objectDb.DbCommand;
                dbCommand.CommandTimeout = timeOut;
                objectDb.AssignParamValues(parameterValues);
                DataSet ds = db.ExecuteDataSet(dbCommand);
                Hashtable hashParams = GetOuputParams(db, dbCommand);
                result = new DbResult(hashParams, ds);
            }

            return result;
        }

        public static DbResult ExecuteDbResult(string settingKey, dynamic parameterValues)
        {
            return ExecuteDbResult(settingKey, parameterValues, TimeOutDefault);
        }

        public static DbResult ExecuteDbResult(string settingKey, dynamic parameterValues, int timeOut)
        {
            DbResult result = null;
            using (ObjectDb objectDb = new ObjectDb(settingKey, timeOut))
            {
                //var db = objectDb.Database;
                //var dbCommand = objectDb.DbCommand;
                //dbCommand.CommandTimeout = timeOut;
                //objectDb.AssignParamValues(parameters);
                DataSet ds = objectDb.ExecuteDataSet(parameterValues);
                Hashtable hashParams = GetOuputParams(objectDb.Database, objectDb.DbCommand);
                result = new DbResult(hashParams, ds);
            }

            return result;
        }
        #endregion

        #region ExecuteDataset
        public static DataSet ExecuteDataSet(string settingKey)
        {
            return ExecuteDataSet(settingKey, TimeOutDefault);
        }

        public static DataSet ExecuteDataSet(string settingKey, int timeOut)
        {
            return ExecuteDataSet(settingKey, null, timeOut);
        }

        public static DataSet ExecuteDataSet(string settingKey, Hashtable parameterValues)
        {
            return ExecuteDataSet(settingKey, parameterValues, TimeOutDefault);
        }

        public static DataSet ExecuteDataSet(string settingKey, dynamic parameterValues)
        {
            return ExecuteDataSet(settingKey, TimeOutDefault, parameterValues);
        }

        public static DataSet ExecuteDataSet(string settingKey, int timeOut, dynamic parameterValues)
        {
            using (ObjectDb objectDb = new ObjectDb(settingKey, timeOut))
            {
                DataSet ds = objectDb.ExecuteDataSet(parameterValues);
                return ds;
            }
        }

        public static DataSet ExecuteDataSet(string settingKey, Hashtable parameterValues, int timeOut)
        {
            DbResult dbResult = ExecuteDbResult(settingKey, parameterValues, timeOut);
            if (dbResult != null)
            {
                return dbResult.ResultSet;
            }

            //return empty dataset
            return new DataSet();
        }

        #endregion

        #region ExecuteDataTable
        public static DataTable ExecuteDataTable(string settingKey)
        {
            return ExecuteDataTable(settingKey, TimeOutDefault);
        }

        public static DataTable ExecuteDataTable(string settingKey, int timeOut)
        {
            return ExecuteDataTable(settingKey, null, timeOut);
        }

        public static DataTable ExecuteDataTable(string settingKey, Hashtable parameterValues)
        {
            return ExecuteDataTable(settingKey, parameterValues, TimeOutDefault);
        }

        public static DataTable ExecuteDataTable(string settingKey, Hashtable parameterValues, int timeOut)
        {
            DataSet ds = ExecuteDataSet(settingKey, parameterValues, timeOut);
            if (ds.Tables != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }

            //return empty datatable
            return new DataTable();
        }

        public static DataTable ExecuteDataTable(string settingKey, dynamic parameterValues)
        {
            return ExecuteDataTable(settingKey, TimeOutDefault, parameterValues);
        }

        public static DataTable ExecuteDataTable(string settingKey, int timeOut, dynamic parameterValues)
        {
            DataSet dataSet = ExecuteDataSet(settingKey, parameterValues, timeOut);
            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                return dataSet.Tables[0];
            }

            return new DataTable();
        }
        #endregion

        #region ExecuteReader
        public static IDataReader ExecuteReader(string settingKey)
        {
            return ExecuteReader(settingKey, TimeOutDefault);
        }

        public static IDataReader ExecuteReader(string settingKey, int timeOut)
        {
            return ExecuteReader(settingKey, null, timeOut);
        }

        public static IDataReader ExecuteReader(string settingKey, Hashtable parameterValues)
        {
            return ExecuteReader(settingKey, parameterValues, TimeOutDefault);
        }

        public static IDataReader ExecuteReader(string settingKey, Hashtable parameterValues, int timeOut)
        {
            ObjectDb objectDb = new ObjectDb(settingKey);
            var db = objectDb.Database;
            var dbCommand = objectDb.DbCommand;
            objectDb.AssignParamValues(parameterValues);
            IDataReader reader = db.ExecuteReader(dbCommand);

            return reader;
        }

        public static IDataReader ExecuteReader(string settingKey, dynamic parameterValues)
        {
            return ExecuteReader(settingKey, TimeOutDefault, parameterValues);
        }

        /// <summary>
        /// have to call this function inside using statement to ensure dispose data reader 
        /// </summary>
        /// <param name="settingKey"></param>
        /// <param name="timeOut"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static IDataReader ExecuteReader(string settingKey, int timeOut, dynamic parameterValues)
        {
            ObjectDb objDb = new ObjectDb(settingKey, timeOut);

            return objDb.ExecuteReader(parameterValues);
        }
        #endregion

        #region ExecuteNonQuery
        public static int ExecuteNonQuery(string settingKey)
        {
            return ExecuteNonQuery(settingKey, TimeOutDefault);
        }

        public static int ExecuteNonQuery(string settingKey, int timeOut)
        {
            return ExecuteNonQuery(settingKey, null, timeOut);
        }

        public static int ExecuteNonQuery(string settingKey, Hashtable parameterValues)
        {
            return ExecuteNonQuery(settingKey, parameterValues, TimeOutDefault);
        }

        public static int ExecuteNonQuery(string settingKey, Hashtable parameterValues, int timeOut)
        {
            int rowAffected = 0;
            using (ObjectDb objectDb = new ObjectDb(settingKey))
            {
                var db = objectDb.Database;
                var dbCommand = objectDb.DbCommand;
                objectDb.AssignParamValues(parameterValues);
                rowAffected = db.ExecuteNonQuery(dbCommand);
            }

            return rowAffected;
        }

        public static int ExecuteNonQuery(string settingKey, dynamic parameterValues)
        {
            return ExecuteNonQuery(settingKey, TimeOutDefault, parameterValues);
        }

        public static int ExecuteNonQuery(string settingKey, int timeOut, dynamic parameterValues)
        {
            using (ObjectDb objectDB = new ObjectDb(settingKey, timeOut))
            {
                return objectDB.ExecuteNonQuery(parameterValues);
            }
        }
        #endregion

        #region ExecuteScalar
        public static object ExecuteScalar(string settingKey)
        {
            return ExecuteScalar(settingKey, TimeOutDefault);
        }

        public static object ExecuteScalar(string settingKey, int timeOut)
        {
            return ExecuteScalar(settingKey, null, timeOut);
        }

        public static object ExecuteScalar(string settingKey, Hashtable parameterValues)
        {
            return ExecuteScalar(settingKey, parameterValues, TimeOutDefault);
        }

        public static object ExecuteScalar(string settingKey, Hashtable parameterValues, int timeOut)
        {
            using (ObjectDb objectDb = new ObjectDb(settingKey))
            {
                var db = objectDb.Database;
                var dbCommand = objectDb.DbCommand;

                objectDb.AssignParamValues(parameterValues);

                object obj = db.ExecuteScalar(dbCommand);

                return obj;
            }
        }

        public static object ExecuteScalar(string settingKey, dynamic parameterValues)
        {
            return ExecuteScalar(settingKey, TimeOutDefault, parameterValues);
        }

        public static object ExecuteScalar(string settingKey, int timeOut, dynamic parameterValues)
        {
            using (ObjectDb objectDb = new ObjectDb(settingKey, timeOut))
            {
                return objectDb.ExecuteScalar(parameterValues);
            }
        }
        #endregion

        #region Dapper Query - mapping

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="settingKey"><see cref="TimeOutDefault"/></param>
        /// <returns></returns>
        public static IEnumerable<T> Query<T>(string settingKey)
        {
            return Query<T>(settingKey, null, TimeOutDefault);
        }

        public static IEnumerable<T> Query<T>(string settingKey, int timeOut)
        {
            return Query<T>(settingKey, null, timeOut);
        }

        public static IEnumerable<T> Query<T>(string settingKey, Hashtable parameterValues)
        {
            return Query<T>(settingKey, parameterValues, TimeOutDefault);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="settingKey"></param>
        /// <param name="parameterValues"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static IEnumerable<T> Query<T>(string settingKey, Hashtable parameterValues, int timeOut)
        {
            IEnumerable<T> result;
            var dbSetting = DbSettingProviderManager.Current.GetSetting(settingKey);
            using (ObjectDb objectDb = new ObjectDb(dbSetting))
            {
                using (DbConnection dbConnection = objectDb.DbConnection)
                {
                    DynamicParameters dynamicParams = objectDb.AssignDynamicParamsValueFromHashtable(parameterValues);

                    dbConnection.Open();

                    result = dbConnection.Query<T>(
                           dbSetting.SpName,
                           dynamicParams,
                           null,
                           true,
                           timeOut,
                           CommandType.StoredProcedure);

                    if (dbConnection.State != ConnectionState.Closed)
                    {
                        dbConnection.Close();
                    }
                }
            }

            return result;
        }

        public static IEnumerable<T> Query<T>(string settingKey, params object[] parameterValues)
        {
            return Query<T>(settingKey, TimeOutDefault, parameterValues);
        }

        public static IEnumerable<T> Query<T>(string settingKey, int timeOut, params object[] parameterValues)
        {
            IEnumerable<T> result;
            var dbSetting = DbSettingProviderManager.Current.GetSetting(settingKey);
            using (ObjectDb objectDb = new ObjectDb(dbSetting))
            {
                using (DbConnection dbConnection = objectDb.DbConnection)
                {
                    DynamicParameters dynamicParams = objectDb.AssignDynamicParamsValue(parameterValues);

                    dbConnection.Open();
                    result = dbConnection.Query<T>(
                        dbSetting.SpName,
                        dynamicParams,
                        null,
                        true,
                        TimeOutDefault,
                        CommandType.StoredProcedure);

                    if (dbConnection.State != ConnectionState.Closed)
                    {
                        dbConnection.Close();
                    }
                }

                return result;
            }
        }

        public static T QueryEntity<T>(string settingKey)
        {
            return QueryEntity<T>(settingKey, null, TimeOutDefault);
        }

        public static T QueryEntity<T>(string settingKey, int timeOut)
        {
            return QueryEntity<T>(settingKey, null, timeOut);
        }

        public static T QueryEntity<T>(string settingKey, Hashtable parameterValues)
        {
            return QueryEntity<T>(settingKey, parameterValues, TimeOutDefault);
        }

        public static T QueryEntity<T>(string settingKey, Hashtable parameterValues, int timeOut)
        {
            IEnumerable<T> result = Query<T>(settingKey, parameterValues, timeOut);
            return result.GetFirstOrDefault<T>();
        }

        public static T QueryEntity<T>(string settingKey, params object[] parameterValues)
        {
            return QueryEntity<T>(settingKey, TimeOutDefault, parameterValues);
        }

        public static T QueryEntity<T>(string settingKey, int timeOut, params object[] parameterValues)
        {
            IEnumerable<T> result = Query<T>(settingKey, timeOut, parameterValues);
            return result.GetFirstOrDefault<T>();
        }

        #endregion

        #region Util functions

        /// <summary>
        /// get output parameter
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        private static Hashtable GetOuputParams(SqlDatabase db, DbCommand dbCommand)
        {
            if (dbCommand.HasOutputParam())
            {
                Hashtable outputParam = new Hashtable();

                foreach (SqlParameter param in dbCommand.Parameters)
                {
                    if (param.IsNotReturnValue()
                        && param.Direction != ParameterDirection.Input)
                    {
                        object outputParamValue = db.GetParameterValue(dbCommand, param.ParameterName);
                        outputParam.Add(param.GetLowerName(), outputParamValue);
                    }
                }

                return outputParam;
            }

            return null;
        }

        #endregion
    }
}
