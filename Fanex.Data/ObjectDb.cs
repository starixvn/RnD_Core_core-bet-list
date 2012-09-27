//////////
// <copyright file="ObjectDB.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// <summary>
//
// </summary>
// <history>
//  <change who="Anders" date="2011.12.14">Created</change>
//  <change who="Edward" date="2012.1.3">Add two function Query</change>
//  <change who="Anders" date="2012.01.04">Refactor - Add assign param to objectDB</change>
//  <change who="Edward" date="2012.2.08">Add ctor fucntion null for sp resource</change>
// </history>
namespace Fanex.Data
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Linq;
    using Fanex.Data.Configuration;
    using Fanex.Data.Dapper;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
    using System.Text;

    /// <summary>
    /// Composite data access object,
    /// provide SqlDatabase entlib object, DBCommand,
    /// and DBConnection (include Fanex.Data.Dapper to use useful extension mothods)
    /// </summary>
    public sealed class ObjectDb : IDisposable
    {
        private static volatile ParameterCache _parameterCache = new ParameterCache();
        private const int TimeOutDefault = 120;
        private int _commandTimeOut;

        #region Ctor

        private IDbSetting _dbSetting;

        /// <summary>
        /// Create instance with command timeout default
        /// </summary>
        /// <param name="key"></param>
        /// <param name="commandTimeOut">command timeout</param>
        public ObjectDb(string key, int commandTimeOut = TimeOutDefault)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException();

            _commandTimeOut = commandTimeOut;
            _dbSetting = DbSettingProviderManager.Current.GetSetting(key);
        }

        public ObjectDb(IDbSetting setting, int commandTimeOut = TimeOutDefault)
        {
            if (ReferenceEquals(setting, null))
                throw new ArgumentNullException();

            _commandTimeOut = commandTimeOut;
            _dbSetting = setting;
        }

        #endregion Ctor

        #region Properties

        private SqlDatabase _sqlDatabase;

        /// <summary>
        /// <see cref="Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase"/>
        /// </summary>
        public SqlDatabase Database
        {
            get
            {
                if (_sqlDatabase == null)
                {
                    _sqlDatabase = new SqlDatabase(_dbSetting.ConnectionString);
                }

                return _sqlDatabase;
            }
        }

        private DbCommand _dbCommand;

        /// <summary>
        /// <see cref="System.Data.Common.DbCommand"/>
        /// </summary>
        public DbCommand DbCommand
        {
            get
            {
                if (_dbCommand == null)
                {
                    _dbCommand = Database.GetStoredProcCommand(_dbSetting.SpName);
                    _dbCommand.CommandTimeout = _commandTimeOut;
                }
                return _dbCommand;
            }
        }

        private DbConnection _dbConnection;

        /// <summary>
        /// Gets the db connection.
        /// </summary>
        public DbConnection DbConnection
        {
            get
            {
                if (_dbConnection == null)
                {
                    _dbConnection = Database.CreateConnection();
                }

                return _dbConnection;
            }
        }

        public IDbSetting DbSetting
        {
            get { return _dbSetting; }
            private set { _dbSetting = value; }
        }

        #endregion Properties

        #region Query

        /// <summary>
        /// Query data with auto mapping
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mappingColumns">{key:value}={fieldname:property-name}</param>
        /// <param name="param">if param is hashtable,auto mapping dynamic object</param>
        /// <param name="transaction"></param>
        /// <param name="buffer">true to minimizes shared locks in the db
        /// and cuts down on db network time.
        /// DO NOT use when executing huge queries
        /// </param>
        /// <param name="commandTimeout">commandTimeout</param>
        /// <returns>IEnumerable<T></returns>
        public IEnumerable<T> Query<T>(dynamic param = null, IDbTransaction transaction = null
           , bool buffer = true, int? commandTimeout = null)
        {
            IEnumerable<T> results;
            using (var connection = DbConnection)
            {
                connection.Open();

                if (param is Hashtable)
                {
                    Hashtable parameters = (Hashtable)param;
                    DynamicParameters dynamicParamas = new DynamicParameters();
                    dynamicParamas = this.AssignDynamicParamsValueFromHashtable(parameters);
                    results = connection.Query<T>(DbSetting.CustomMappingColumns, DbSetting.SpName, dynamicParamas
                                     , transaction
                                     , buffer
                                     , commandTimeout ?? TimeOutDefault
                                     , CommandType.StoredProcedure);

                    foreach (SqlParameter paramItem in DbCommand.Parameters)
                    {
                        if (paramItem.Direction == ParameterDirection.InputOutput || paramItem.Direction == ParameterDirection.Output)
                        {
                            string paramName = paramItem.GetName();

                            string paramLowerName = paramName.ToLower();

                            if (parameters.ContainsKey(paramLowerName))
                            {
                                parameters[paramLowerName] = dynamicParamas.Get<object>(paramName);
                            }
                        }
                    }

                }
                else
                {
                    results = connection.Query<T>(DbSetting.CustomMappingColumns, DbSetting.SpName, (object)param, transaction, buffer
                                 , commandTimeout ?? TimeOutDefault
                                 , CommandType.StoredProcedure);
                }

                connection.Close();
            }

            return results;
        }


        /// <summary>
        /// Queries the entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mappingColumns">The mapping columns.</param>
        /// <param name="param">The param.</param>
        /// <param name="transaction">The transaction.</param>
        /// <param name="buffer">if set to <c>true</c> [buffer].</param>
        /// <param name="commandTimeout">The command timeout.</param>
        /// <returns></returns>
        public T QueryEntity<T>(dynamic param = null, IDbTransaction transaction = null
         , bool buffer = true, int? commandTimeout = null)
        {
            IEnumerable<T> results = Query<T>(param, transaction, buffer, commandTimeout);

            return results.FirstOrDefault<T>();
        }


        #endregion Query

        #region Nested Class

        /// <summary>
        /// Query nested class
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IEnumerable<TResult> Query<TFirst, TSecond, TResult>(Func<TFirst, TSecond, TResult> func, string splitOn = "Id", dynamic parameterValues = null)
        {
            IEnumerable<TResult> result;
            using (var db = DbConnection)
            {
                db.Open();

                if (parameterValues is Hashtable)
                {
                    Hashtable parameters = (Hashtable)parameterValues;
                    DynamicParameters dynamicParamas = new DynamicParameters();
                    dynamicParamas = this.AssignDynamicParamsValueFromHashtable(parameters);
                    result = db.Query<TFirst, TSecond, TResult>(
                                    DbSetting.CustomMappingColumns
                                    , DbSetting.SpName
                                    , func
                                    , dynamicParamas
                                    , null
                                    , true
                                    , splitOn
                                    , TimeOutDefault
                                    , CommandType.StoredProcedure);

                    foreach (SqlParameter paramItem in DbCommand.Parameters)
                    {
                        if (paramItem.Direction == ParameterDirection.InputOutput || paramItem.Direction == ParameterDirection.Output)
                        {
                            if (parameters.ContainsKey(paramItem.GetLowerName()))
                            {
                                parameters[paramItem.GetLowerName()] = dynamicParamas.Get<object>(paramItem.GetLowerName());
                            }
                        }
                    }
                }
                else
                {
                    result = db.Query<TFirst, TSecond, TResult>(
                                    DbSetting.CustomMappingColumns
                                    , DbSetting.SpName
                                    , func
                                    , (object)parameterValues
                                    , null
                                    , true
                                    , splitOn
                                    , TimeOutDefault
                                    , CommandType.StoredProcedure);
                }

                db.Close();
            }

            return result;
        }


        /// <summary>
        /// Query nested class
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IEnumerable<TResult> Query<TFirst, TSecond, TThird, TResult>(Func<TFirst, TSecond, TThird, TResult> func, string splitOn = "Id", dynamic parameterValues = null)
        {
            IEnumerable<TResult> result;
            using (var db = DbConnection)
            {
                db.Open();

                if (parameterValues is Hashtable)
                {
                    Hashtable parameters = (Hashtable)parameterValues;
                    DynamicParameters dynamicParamas = new DynamicParameters();
                    dynamicParamas = this.AssignDynamicParamsValueFromHashtable(parameters);
                    result = db.Query<TFirst, TSecond, TThird, TResult>(
                                      DbSetting.CustomMappingColumns
                                    , DbSetting.SpName
                                    , func
                                     , dynamicParamas
                                     , null
                                     , true
                                     , splitOn
                                     , TimeOutDefault
                                     , CommandType.StoredProcedure);

                    foreach (SqlParameter paramItem in DbCommand.Parameters)
                    {
                        if (paramItem.Direction == ParameterDirection.InputOutput || paramItem.Direction == ParameterDirection.Output)
                        {
                            if (parameters.ContainsKey(paramItem.GetLowerName()))
                            {
                                parameters[paramItem.GetLowerName()] = dynamicParamas.Get<object>(paramItem.GetLowerName());
                            }
                        }
                    }
                }
                else
                {
                    result = db.Query<TFirst, TSecond, TThird, TResult>(
                              DbSetting.CustomMappingColumns
                        , DbSetting.SpName
                            , func
                            , (object)parameterValues
                            , null
                            , true
                            , splitOn
                            , TimeOutDefault
                            , CommandType.StoredProcedure);
                }

                db.Close();
            }
            return result;
        }


        /// <summary>
        /// Query nested class
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IEnumerable<TResult> Query<TFirst, TSecond, TThird, TFourth, TResult>(Func<TFirst, TSecond, TThird, TFourth, TResult> func, string splitOn = "Id", dynamic parameterValues = null)
        {
            IEnumerable<TResult> result;
            using (var db = DbConnection)
            {
                db.Open();

                if (parameterValues is Hashtable)
                {
                    Hashtable parameters = (Hashtable)parameterValues;
                    DynamicParameters dynamicParamas = new DynamicParameters();
                    dynamicParamas = this.AssignDynamicParamsValueFromHashtable(parameters);
                    result = db.Query<TFirst, TSecond, TThird, TFourth, TResult>(
                                      DbSetting.CustomMappingColumns
                                    , DbSetting.SpName
                                    , func
                                     , dynamicParamas
                                     , null
                                     , true
                                     , splitOn
                                     , TimeOutDefault
                                     , CommandType.StoredProcedure);

                    foreach (SqlParameter paramItem in DbCommand.Parameters)
                    {
                        if (paramItem.Direction == ParameterDirection.InputOutput || paramItem.Direction == ParameterDirection.Output)
                        {
                            if (parameters.ContainsKey(paramItem.GetLowerName()))
                            {
                                parameters[paramItem.GetLowerName()] = dynamicParamas.Get<object>(paramItem.GetLowerName());
                            }
                        }
                    }
                }
                else
                {
                    result = db.Query<TFirst, TSecond, TThird, TFourth, TResult>(
                          DbSetting.CustomMappingColumns
                        , DbSetting.SpName
                            , func
                            , (object)parameterValues
                            , null
                            , true
                            , splitOn
                            , TimeOutDefault
                            , CommandType.StoredProcedure);

                }

                db.Close();

            }

            return result;
        }



        /// <summary>
        /// Query nested class
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IEnumerable<TResult> Query<TFirst, TSecond, TThird, TFourth, TFifth, TResult>(Func<TFirst, TSecond, TThird, TFourth, TFifth, TResult> func, string splitOn = "Id", dynamic parameterValues = null)
        {
            IEnumerable<TResult> result;
            using (var db = DbConnection)
            {
                db.Open();

                if (parameterValues is Hashtable)
                {
                    Hashtable parameters = (Hashtable)parameterValues;
                    DynamicParameters dynamicParamas = new DynamicParameters();
                    dynamicParamas = this.AssignDynamicParamsValueFromHashtable(parameters);
                    result = db.Query<TFirst, TSecond, TThird, TFourth, TFifth, TResult>(
                                      DbSetting.CustomMappingColumns
                                    , DbSetting.SpName
                                    , func
                                     , dynamicParamas
                                     , null
                                     , true
                                     , splitOn
                                     , TimeOutDefault
                                     , CommandType.StoredProcedure);

                    foreach (SqlParameter paramItem in DbCommand.Parameters)
                    {
                        if (paramItem.Direction == ParameterDirection.InputOutput || paramItem.Direction == ParameterDirection.Output)
                        {
                            if (parameters.ContainsKey(paramItem.GetLowerName()))
                            {
                                parameters[paramItem.GetLowerName()] = dynamicParamas.Get<object>(paramItem.GetLowerName());
                            }
                        }
                    }
                }
                else
                {
                    result = db.Query<TFirst, TSecond, TThird, TFourth, TFifth, TResult>(
                          DbSetting.CustomMappingColumns
                        , DbSetting.SpName
                            , func
                            , (object)parameterValues
                            , null
                            , true
                            , splitOn
                            , TimeOutDefault
                            , CommandType.StoredProcedure);
                }

                db.Close();
            }

            return result;
        }



        /// <summary>
        /// Query nested class
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public TResult QueryEntity<TFirst, TSecond, TResult>(Func<TFirst, TSecond, TResult> func, string splitOn = "Id", dynamic parameters = null)
        {
            IEnumerable<TResult> result = Query<TFirst, TSecond, TResult>(func, splitOn, parameters);
            return result.FirstOrDefault();
        }



        /// <summary>
        /// Query nested class
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public TResult QueryEntity<TFirst, TSecond, TThird, TResult>(Func<TFirst, TSecond, TThird, TResult> func, string splitOn = "Id", dynamic parameters = null)
        {
            IEnumerable<TResult> result = Query<TFirst, TSecond, TThird, TResult>(func, splitOn, parameters);
            return result.FirstOrDefault();
        }


        /// <summary>
        /// Query nested class
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public TResult QueryEntity<TFirst, TSecond, TThird, TFourth, TResult>(Func<TFirst, TSecond, TThird, TFourth, TResult> func, string splitOn = "Id", dynamic parameters = null)
        {
            IEnumerable<TResult> result = Query<TFirst, TSecond, TThird, TFourth, TResult>(func, splitOn, parameters);
            return result.FirstOrDefault();
        }



        /// <summary>
        /// Query nested class
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public TResult QueryEntity<TFirst, TSecond, TThird, TFourth, TFifth, TResult>(Func<TFirst, TSecond, TThird, TFourth, TFifth, TResult> func, string splitOn = "Id", dynamic parameters = null)
        {
            IEnumerable<TResult> result = Query<TFirst, TSecond, TThird, TFourth, TFifth, TResult>(func, splitOn, parameters);
            return result.FirstOrDefault();
        }


        #endregion Nested Class

        #region Entlib functions

        #region ExecuteDataSet

        public DataSet ExecuteDataSet()
        {
            return Database.ExecuteDataSet(DbCommand);
        }

        public DataSet ExecuteDataSet(dynamic paramaterValues)
        {
            AssignDynamicParamsValue(DbCommand, paramaterValues);
            return Database.ExecuteDataSet(DbCommand);
        }

        public DataSet ExecuteDataSet(DbTransaction transaction, dynamic paramaterValues)
        {
            AssignDynamicParamsValue(DbCommand, paramaterValues);
            return Database.ExecuteDataSet(transaction, DbSetting.SpName, paramaterValues);
        }

        public DataSet ExecuteDataSet(Hashtable paramaterValues)
        {
            AssignDynamicParamsValue(DbCommand, paramaterValues);
            DataSet ds = Database.ExecuteDataSet(DbCommand);
            foreach (SqlParameter paramItem in DbCommand.Parameters)
            {
                if (paramItem.Direction == ParameterDirection.Output || paramItem.Direction == ParameterDirection.InputOutput)
                {
                    object value = Database.GetParameterValue(DbCommand, paramItem.GetLowerName());
                    paramaterValues[paramItem.GetLowerName()] = value;
                }
            }
            return ds;
        }

        #endregion ExecuteDataSet

        #region ExecuteNonQuery

        /// <summary>
        /// Executes the non query.
        /// </summary>
        /// <returns></returns>
        public int ExecuteNonQuery()
        {
            return Database.ExecuteNonQuery(DbCommand);
        }

        /// <summary>
        /// Executes the non query.
        /// </summary>
        /// <param name="paramaterValues">The paramater values.</param>
        /// <returns></returns>
        public int ExecuteNonQuery(dynamic paramaterValues)
        {
            AssignDynamicParamsValue(DbCommand, paramaterValues);
            return Database.ExecuteNonQuery(DbCommand);
        }

        /// <summary>
        /// Executes the non query.
        /// </summary>
        /// <param name="transaction">The transaction.</param>
        /// <param name="paramaterValues">The paramater values.</param>
        /// <returns></returns>
        public int ExecuteNonQuery(DbTransaction transaction, dynamic paramaterValues)
        {
            AssignDynamicParamsValue(DbCommand, paramaterValues);
            return Database.ExecuteNonQuery(transaction, DbSetting.SpName, paramaterValues);
        }

        public void ExecuteNonQuery(Hashtable paramaterValues)
        {
            AssignDynamicParamsValue(DbCommand, paramaterValues);
            Database.ExecuteNonQuery(DbCommand);
            foreach (SqlParameter paramItem in DbCommand.Parameters)
            {
                if (paramItem.Direction == ParameterDirection.Output || paramItem.Direction == ParameterDirection.InputOutput)
                {
                    object value = Database.GetParameterValue(DbCommand, paramItem.GetLowerName());
                    paramaterValues[paramItem.GetLowerName()] = value;
                }
            }
        }

        #endregion ExecuteNonQuery

        #region ExecuteReader

        /// <summary>
        /// Executes the reader.
        /// </summary>
        /// <returns>return IDataReader</returns>
        public IDataReader ExecuteReader()
        {
            return Database.ExecuteReader(DbCommand);
        }

        /// <summary>
        /// Executes the reader.
        /// </summary>
        /// <param name="paramaterValues">The paramater values.</param>
        /// <returns>return IDataReader</returns>
        public IDataReader ExecuteReader(dynamic paramaterValues)
        {
            AssignDynamicParamsValue(DbCommand, paramaterValues);
            return Database.ExecuteReader(DbCommand);
        }

        /// <summary>
        /// Executes the reader.
        /// </summary>
        /// <param name="transaction">The transaction.</param>
        /// <param name="paramaterValues">The paramater values.</param>
        /// <returns>return IDataReader</returns>
        public IDataReader ExecuteReader(DbTransaction transaction, dynamic paramaterValues)
        {
            AssignDynamicParamsValue(DbCommand, paramaterValues);
            return Database.ExecuteReader(transaction, DbSetting.SpName, paramaterValues);
        }

        public IDataReader ExecuteReader(Hashtable paramaterValues)
        {
            AssignDynamicParamsValue(DbCommand, paramaterValues);
            return Database.ExecuteReader(DbCommand);
        }
        #endregion ExecuteReader

        #region ExecuteScalar

        /// <summary>
        /// Executes the scalar.
        /// </summary>
        /// <returns></returns>
        public object ExecuteScalar()
        {
            return Database.ExecuteScalar(DbCommand);
        }

        /// <summary>
        /// Executes the scalar.
        /// </summary>
        /// <param name="paramaterValues">The paramater values.</param>
        /// <returns></returns>
        public object ExecuteScalar(dynamic paramaterValues)
        {
            AssignDynamicParamsValue(DbCommand, paramaterValues);
            return Database.ExecuteScalar(DbCommand);
        }

        /// <summary>
        /// Executes the scalar.
        /// </summary>
        /// <param name="transaction">The transaction.</param>
        /// <param name="paramaterValues">The paramater values.</param>
        /// <returns></returns>
        public object ExecuteScalar(DbTransaction transaction, dynamic paramaterValues)
        {
            AssignDynamicParamsValue(DbCommand, paramaterValues);
            return Database.ExecuteScalar(transaction, DbSetting.SpName, paramaterValues);
        }

        public object ExecuteScalar(Hashtable paramaterValues)
        {
            AssignDynamicParamsValue(DbCommand, paramaterValues);
            object obj = Database.ExecuteScalar(DbCommand);
            foreach (SqlParameter paramItem in DbCommand.Parameters)
            {
                if (paramItem.Direction == ParameterDirection.Output || paramItem.Direction == ParameterDirection.InputOutput)
                {
                    object value = Database.GetParameterValue(DbCommand, paramItem.GetLowerName());
                    paramaterValues[paramItem.GetLowerName()] = value;
                }
            }
            return obj;
        }

        #endregion ExecuteScalar

        #region AddIn AddOut Param

        /// <summary>
        /// Adds the in parameter.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="dbType">Type of the db.</param>
        /// <param name="value">The value.</param>
        public void AddInParameter(string name, DbType dbType, object value)
        {
            Database.AddInParameter(DbCommand, name, dbType, value);
        }

        /// <summary>
        /// Adds the out parameter.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="dbType">Type of the db.</param>
        /// <param name="size">The size.</param>
        public void AddOutParameter(string name, DbType dbType, int size)
        {
            Database.AddOutParameter(DbCommand, name, dbType, size);
        }

        /// <summary>
        /// Gets the parameter value.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public object GetParameterValue(string name)
        {
            return Database.GetParameterValue(DbCommand, name);
        }

        /// <summary>
        /// set paramater value
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        public void SetParameterValue(string parameterName, object value)
        {
            Database.SetParameterValue(this.DbCommand, parameterName, value);
        }

        /// <summary>
        /// Create paramater
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dbType"></param>
        public void AddInParameter(string name, DbType dbType)
        {
            Database.AddInParameter(DbCommand, name, dbType);
        }

        /// <summary>
        /// Auto discover paramater from sp and assign value get from cache
        /// </summary>
        /// <param name="parameterCache"><see cref="Microsoft.Practices.EnterpriseLibrary.Data.ParameterCache"/></param>
        /// <param name="parameters">Hashtable</param>
        public void AssignParamValues(Hashtable parameters)
        {
            var dbCommand = this.DbCommand;
            var db = this.Database;

            _parameterCache.SetParameters(dbCommand, db);
            if (!ReferenceEquals(parameters, null))
            {
                foreach (SqlParameter param in dbCommand.Parameters)
                {
                    if (param.IsNotReturnValue())
                    {
                        param.Value = parameters[param.GetLowerName()];
                    }
                }
            }
        }

        #endregion AddIn AddOut Param

        #endregion Entlib functions

        #region IDisposable Members

        public void Dispose()
        {
            if (_dbCommand != null && _dbCommand.Connection != null)
            {
                if (_dbCommand.Connection.State != ConnectionState.Closed)
                {
                    _dbCommand.Connection.Close();
                    _dbCommand.Dispose();
                    _dbCommand = null;
                    _sqlDatabase = null;
                }
            }
        }

        #endregion IDisposable Members

        #region DynamicParameters functions

        /// <summary>
        /// Assigns the dynamic params value.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public DynamicParameters AssignDynamicParamsValue(params object[] parameters)
        {
            DynamicParameters dynamicParams = new DynamicParameters();
            if (!ReferenceEquals(parameters, null))
            {
                _parameterCache.SetParameters(this.DbCommand, this.Database);
                int i = 0;
                foreach (SqlParameter param in DbCommand.Parameters)
                {
                    if (param.IsNotReturnValue())
                    {
                        dynamicParams.Add(param.ParameterName, parameters.GetValue(i++)
                            , null, param.Direction
                            , param.Size);
                    }
                }
            }
            else
            {
                dynamicParams = AssignDynamicParamsValue();
            }
            return dynamicParams;
        }

        /// <summary>
        /// Assigns the dynamic params value.
        /// </summary>
        /// <returns></returns>
        public DynamicParameters AssignDynamicParamsValue()
        {
            DynamicParameters dynamicParams = new DynamicParameters();
            if (!ReferenceEquals(this.DbCommand.Parameters, null))
            {
                foreach (SqlParameter param in DbCommand.Parameters)
                {
                    if (param.IsNotReturnValue())
                    {
                        dynamicParams.Add(param.ParameterName
                            , param.Value
                            , null, param.Direction
                            , param.Size);
                    }
                }
            }
            return dynamicParams;
        }

        /// <summary>
        /// Assigns the dynamic params value.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public DynamicParameters AssignDynamicParamsValueFromHashtable(Hashtable parameters)
        {
            DynamicParameters dynamicParams = new DynamicParameters();
            _parameterCache.SetParameters(this.DbCommand, this.Database);
            if (!ReferenceEquals(parameters, null))
            {
                foreach (SqlParameter param in DbCommand.Parameters)
                {
                    if (param.IsNotReturnValue())
                    {
                        dynamicParams.Add(param.ParameterName
                            , value: parameters[param.GetLowerName()]
                            , dbType: param.DbType
                            , direction: param.Direction
                            , size: param.Size);
                    }
                }
            }
            return dynamicParams;
        }

        /// <summary>
        /// get output param
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public Hashtable GetOuputParams()
        {
            var dbCommand = this.DbCommand;
            var db = this.Database;

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

        /// <summary>
        /// Assigns the dynamic params value.
        /// </summary>
        /// <param name="dbCommand">The db command.</param>
        /// <param name="paramaterValues">The paramater values.</param>
        private void AssignDynamicParamsValue(DbCommand dbCommand, dynamic paramaterValues)
        {
            if (!ReferenceEquals(paramaterValues, null))
            {
                if (paramaterValues.GetType().Name == "Hashtable")
                {
                    var db = this.Database;
                    Hashtable parameters = (Hashtable)paramaterValues;

                    _parameterCache.SetParameters(dbCommand, db);
                    if (!ReferenceEquals(parameters, null))
                    {
                        foreach (SqlParameter param in dbCommand.Parameters)
                        {
                            if (param.IsNotReturnValue())
                            {
                                param.Value = parameters[param.GetLowerName()];
                            }
                        }
                    }
                }
                else
                {
                    object obj = (object)paramaterValues;
                    Action<IDbCommand, object> action = new Action<IDbCommand, object>(SqlMapper.CreateParamInfoGenerator(obj.GetType()));
                    if (dbCommand.Parameters.Count != 0)
                    {
                        dbCommand.Parameters.Clear();
                    }
                    action(dbCommand, obj);
                }
            }

        }

        #endregion DynamicParameters functions
    }
}