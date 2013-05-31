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
    using System.IO;

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    internal static class ConnectionStringWatcher
    {
        #region Member

        private static ConnectionStringDataAccess _connectionStringDataAccess;

        private static readonly string _commandText = "bodb_ConnectionString.dbo.ConnectionString_ConnectionString_ClientGet";

        private static readonly string _sqlConnection = ConnectionSettings.Settings.Connection;

        private static string _filePath = ConnectionSettings.Settings.FilePath;

        private static ICache _cache = CacheService.CreateInstance(CacheType.MemoryCache);

        private static readonly int _currentSiteId = ConnectionSettings.Settings.SiteId;

        #endregion

        /// <summary>
        /// Start sql dependency
        /// </summary>
        public static void Start()
        {
            _filePath = GetResourcePath(_filePath);
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

                GenerateConnectionStringFile(collection);
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
        public static void GenerateConnectionStringFile(ConnectionStringSettingsCollection collection)
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

                var isNew = true;

                if (File.Exists(_filePath))
                {
                    isNew = false;
                    var configDocument = XDocument.Load(_filePath);
                    var elementConnectionRoot = configDocument.Descendants("connectionStrings");
                    var count = 0;

                    foreach (var item in elementConnectionRoot)
                    {
                        var elementName = item.Descendants("add");

                        foreach (var itemName in elementName)
                        {
                            count++;
                            string name = itemName.Attribute("name").Value;
                            if (collection[name] != null)
                            {
                                string connection = itemName.Attribute("connectionString").Value;
                                if (string.Compare(connection, collection[name].ConnectionString) != 0)
                                {
                                    isNew = true;
                                }

                            }
                            else
                            {
                                isNew = true;
                            }
                        }
                    }

                    isNew = count == 0 ? true : isNew;
                }

                if (isNew)
                {
                    document.Save(_filePath);
                }
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

        /// <summary>
        /// Get path
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private static string GetResourcePath(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new NullReferenceException(Resources.FilePathNotSetYet);
            }

            // check root path http://msdn.microsoft.com/query/dev10.query?appId=Dev10IDEF1&l=EN-US&k=k%28SYSTEM.IO.PATH.ISPATHROOTED%29;k%28SOLUTIONITEMSPROJECT%29;k%28TargetFrameworkMoniker-%22.NETFRAMEWORK%2cVERSION%3dV4.0%22%29;k%28DevLang-CSHARP%29&rd=true
            if (!Path.IsPathRooted(filePath))
            {
                if (filePath.StartsWith("~"))
                {
                    if (System.Web.HttpContext.Current != null)
                    {
                        filePath = System.Web.HttpContext.Current.Server.MapPath(filePath);
                    }
                }
                else
                {
                    // if got the problem with window service
                    // see http://haacked.com/archive/2004/06/29/706.aspx
                    filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePath);
                }
            }

            return filePath;
        }
    }
}
