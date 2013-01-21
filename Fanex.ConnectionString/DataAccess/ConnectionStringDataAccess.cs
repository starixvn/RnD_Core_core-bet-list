// --------------------------------------------------------------------------
// <copyright file="ConnectionStringDataAccess.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.11">Created</change>
// </history>

namespace Fanex.ConnectionString.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using Fanex.Caching;
    using Fanex.ConnectionString.Configuration;
    using Fanex.ConnectionString.Constanst;
    using Fanex.ConnectionString.Domain;
    using System.Configuration;
    using System.Web.Configuration;
    using System.Xml.Linq;
    using System.Xml;
    using System.IO;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal class ConnectionStringDataAccess : IConnectionStringDataAccess
    {
        #region Properties

        private static string _connnection = ConnectionSettings.Settings.Connection;

        private static string _filePath = ConnectionSettings.Settings.FilePath;

        private SqlConnection _sqlConn = null;

        private ICache _cache = CacheService.CreateInstance(CacheType.MemoryCache);

        private volatile static ConnectionStringDataAccess _connectionStringDataAccess;

        #endregion

        private ConnectionStringDataAccess()
        {
            SqlDependency.Stop(_connnection);

            SqlDependency.Start(_connnection);

            _sqlConn = new SqlConnection(_connnection);
        }

        public static ConnectionStringDataAccess InstanceCreation()
        {
            object lockingObject = new object();
            if (_connectionStringDataAccess == null)
            {
                lock (lockingObject)
                {
                    if (_connectionStringDataAccess == null)
                    {
                        _connectionStringDataAccess = new ConnectionStringDataAccess();
                    }
                }
            }
            return _connectionStringDataAccess;
        }

        public IEnumerable<ConnectionString> GetAll()
        {
            try
            {
                var list = new List<ConnectionString>();

                var command = new SqlCommand("ConnectionString_ConnectionStrings_ClientGet", _sqlConn);
                command.CommandType = CommandType.StoredProcedure;

                SqlDependency dependency = new SqlDependency(command);

                dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                if (_sqlConn.State == ConnectionState.Closed)
                {
                    _sqlConn.Open();
                }

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var connection = new ConnectionString
                        {
                            Id = Convert.ToInt32(reader["Id"].ToString()),
                            Alias = new Alias
                            {
                                Name = reader["Name"].ToString().ToLower()
                            },
                            Server = new DatabaseServer
                            {
                                Id = Convert.ToInt32(reader["ServerId"].ToString()),
                                Name = reader["ServerName"].ToString(),
                                ServerIP = reader["ServerIP"].ToString()
                            },
                            InitialCatalog = reader["InitialCatalog"].ToString(),
                            IntegratedSecurity = Convert.ToBoolean(reader["IntegratedSecurity"].ToString()),
                            UserID = reader["UserID"].ToString(),
                            Password = reader["Password"].ToString(),
                            FreeText = reader["FreeText"].ToString()
                        };

                        string tempConnection = string.Format(ConectionStringConstanst.CONNECTION_FORMAT,
                                                            connection.Server.ServerIP,
                                                            connection.InitialCatalog,
                                                            connection.IntegratedSecurity,
                                                            connection.UserID,
                                                            connection.Password,
                                                            connection.FreeText);
                        connection.Connection = tempConnection;
                        list.Add(connection);
                    }

                    GenerateConnectionStringFile(list);

                    _cache.Set<List<ConnectionString>>(ConectionStringConstanst.PRE_KEY, list);

                    reader.Close();
                    _sqlConn.Close();
                }

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            SqlDependency dependency = sender as SqlDependency;

            dependency.OnChange -= dependency_OnChange;

            GetAll();
        }

        private void GenerateConnectionStringFile(List<ConnectionString> list)
        {
            try
            {
                var document = new XDocument();
                var elementRoot = new XElement("connectionStrings");

                foreach (var item in list)
                {
                    var elementConnection = new XElement("add");
                    var attributeName = new XAttribute("name", item.Alias.Name.ToLower());
                    var attributeConnectionString = new XAttribute("connectionString", item.Connection);
                    var attributeProvider = new XAttribute("providerName", "System.Data.SqlClient");

                    elementConnection.Add(attributeName);
                    elementConnection.Add(attributeConnectionString);
                    elementConnection.Add(attributeProvider);

                    elementRoot.Add(elementConnection);
                }

                document.Add(elementRoot);
                document.Save(_filePath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ~ConnectionStringDataAccess()
        {
            SqlDependency.Stop(_connnection);
        }
    }
}
