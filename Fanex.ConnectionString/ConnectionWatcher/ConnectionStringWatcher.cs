// --------------------------------------------------------------------------
// <copyright file="ConnectionStringDataAccess.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.11">Created</change>
// </history>

namespace Fanex.ConnectionString.ConnectionWatcher
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Xml.Linq;
    using Fanex.Caching;
    using Fanex.ConnectionString.Configuration;
    using Fanex.ConnectionString.Constant;
    using System.Threading.Tasks;
    using System.Configuration;

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    internal class ConnectionStringWatcher : IDisposable
    {
        #region Properties

        private static string _connection = ConnectionSettings.Settings.Connection;

        private static string _filePath = ConnectionSettings.Settings.FilePath;

        private SqlConnection _sqlConn = null;

        private ICache _cache = CacheService.CreateInstance(CacheType.MemoryCache);

        private int _currentSiteId = ConnectionStringConstant.GETALL;

        #endregion

        public ConnectionStringWatcher(int siteId)
        {
            SqlDependency.Stop(_connection);

            SqlDependency.Start(_connection);

            _sqlConn = new SqlConnection(_connection);

            _filePath = GetResourcePath(_filePath);

            _currentSiteId = siteId;
        }

        public ConnectionStringSettingsCollection Fetch()
        {
            try
            {
                var collection = new ConnectionStringSettingsCollection();

                var command = new SqlCommand("ConnectionString_ConnectionString_ClientGet", _sqlConn);
                command.CommandType = CommandType.StoredProcedure;           

                SqlDependency dependency = new SqlDependency(command);

                dependency.OnChange += new OnChangeEventHandler(DependencyOnChange);

                if (_sqlConn.State == ConnectionState.Closed)
                {
                    _sqlConn.Open();
                }

                using (var reader = command.ExecuteReader())
                {
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

                    Task.Factory.StartNew(() => GenerateConnectionStringFile(collection));

                    _cache.Set<ConnectionStringSettingsCollection>(ConnectionStringConstant.PREKEY, collection, DateTime.MaxValue);

                    _sqlConn.Close();
                }

                return collection;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void DependencyOnChange(object sender, SqlNotificationEventArgs e)
        {
            SqlDependency dependency = sender as SqlDependency;

            dependency.OnChange -= DependencyOnChange;

            Fetch();
        }

        private void GenerateConnectionStringFile(ConnectionStringSettingsCollection collection)
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

        private string GetResourcePath(string filePath)
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

        public void Dispose()
        {
            SqlDependency.Stop(_connection);
        }
    }
}
