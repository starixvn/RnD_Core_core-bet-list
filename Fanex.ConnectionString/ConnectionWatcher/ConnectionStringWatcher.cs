// --------------------------------------------------------------------------
// <copyright file="ConnectionStringWatcher.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.31">Created</change>
// </history>

namespace Fanex.ConnectionString.ConnectionWatcher
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using Fanex.Caching;
    using Fanex.ConnectionString.Configuration;
    using Fanex.ConnectionString.Constant;

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    internal static class ConnectionStringWatcher
    {
        #region Member

        private static ConnectionStringDataAccess _connectionStringDataAccess;

        private static readonly string _commandText = "bodb_ConnectionString.dbo.ConnectionString_ConnectionString_ClientGet";

        private static readonly string _sqlConnection = ConnectionSettings.Settings.Connection;

        private static readonly string _filePath = ConnectionSettings.Settings.FilePath;

        private static ICache _cache = CacheService.CreateInstance(CacheType.MemoryCache);

        private static readonly int _currentSiteId = ConnectionSettings.Settings.SiteId;

        #endregion

        /// <summary>
        /// Start sql dependency
        /// </summary>
        public static void Start()
        {
            //Build the command object we want to monitor (don't include a SqlConnection)
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand(_commandText);
            cmd.CommandType = CommandType.StoredProcedure;

            //Setup the SQLWatcher
            _connectionStringDataAccess = new ConnectionStringDataAccess(_sqlConnection, cmd, SqlWatcherNotificationType.Blocking);
            _connectionStringDataAccess.OnChange += new ConnectionStringDataAccess.SqlWatcherEventHandler(QueueSQLWatcher_OnChange);
            _connectionStringDataAccess.Start();
        }

        /// <summary>
        /// Trace data change
        /// </summary>
        /// <param name="reader"></param>
        private static void QueueSQLWatcher_OnChange(IDataReader reader)
        {
            try
            {
                var collection = new ConnectionStringSettingsCollection();

                //Do something with the updated DataSet object
                while (reader.Read())
                {
                    var name = reader["Name"].ToString().ToLower();
                    var serverIp = reader["ServerIP"].ToString();
                    var initialCatalog = reader["InitialCatalog"].ToString();
                    var integratedSecurity = Convert.ToBoolean(reader["IntegratedSecurity"].ToString());
                    var userID = reader["UserID"].ToString();
                    var password = reader["Password"].ToString();
                    var freeText = reader["FreeText"].ToString();
                    var siteId = Convert.ToInt32(reader["SiteId"].ToString());
                    string tempConnection = string.Format(ConnectionStringConstant.CONNECTIONFORMAT,
                                                            serverIp,
                                                            initialCatalog,
                                                            integratedSecurity,
                                                            userID,
                                                            password,
                                                            freeText);
                    if (_currentSiteId != 0)
                    {
                        if (siteId == _currentSiteId)
                        {
                            collection.Add(new ConnectionStringSettings
                            {
                                Name = name,
                                ConnectionString = tempConnection,
                                ProviderName = ConnectionStringConstant.PROVIDERNAME
                            });
                        }
                    }
                    else
                    {
                        collection.Add(new ConnectionStringSettings
                        {
                            Name = name,
                            ConnectionString = tempConnection,
                            ProviderName = ConnectionStringConstant.PROVIDERNAME
                        });
                    }
                }

                _cache.Set<ConnectionStringSettingsCollection>(ConnectionStringConstant.PREKEY, collection, DateTime.MaxValue);

                Task.Factory.StartNew(() => GenerateConnectionStringFile(collection));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                reader.Close();
            }
        }

        /// <summary>
        /// Generate connection string file with format xml
        /// </summary>
        /// <param name="collection"></param>
        private static void GenerateConnectionStringFile(ConnectionStringSettingsCollection collection)
        {
            try
            {
                var document = new XDocument();
                var elementRoot = new XElement("connectionStrings");

                foreach (ConnectionStringSettings item in collection)
                {
                    var elementConnection = new XElement("add");
                    var attributeName = new XAttribute("name", item.Name.ToLower());
                    var attributeConnectionString = new XAttribute("connectionString", item.ConnectionString);
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

        /// <summary>
        /// Stop sql dependency
        /// </summary>
        public static void Stop()
        {
            _connectionStringDataAccess.Dispose();
        }
    }
}
