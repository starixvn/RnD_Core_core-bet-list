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

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal class ConnectionStringDataAccess : IConnectionStringDataAccess
    {
        #region Properties

        private static string _connnection = ConnectionSettings.Settings.Connection;

        private SqlConnection _sqlConn = null;

        private ICache cache = CacheService.CreateInstance(CacheType.MemoryCache);

        #endregion

        public ConnectionStringDataAccess()
        {
            SqlDependency.Stop(_connnection);

            SqlDependency.Start(_connnection);

            _sqlConn = new SqlConnection(_connnection);
        }

        ~ConnectionStringDataAccess()
        {
            SqlDependency.Stop(_connnection);
        }

        #region comment code

        //public DatabaseServer InsertSever(DatabaseServer serverItem)
        //{
        //    try
        //    {
        //        using (var sqlConn = new SqlConnection(_connnection))
        //        {
        //            var command = new SqlCommand("ConnectionString_Servers_Insert", sqlConn);
        //            command.CommandType = CommandType.StoredProcedure;

        //            command.Parameters.Add("@Id", SqlDbType.Int);
        //            command.Parameters.AddWithValue("@Name", serverItem.Name);
        //            command.Parameters.AddWithValue("@ServerIP", serverItem.ServerIP);
        //            command.Parameters.AddWithValue("@IsActive", serverItem.IsActive);
        //            command.Parameters.AddWithValue("@Creator", serverItem.Creator.Id);
        //            command.Parameters.AddWithValue("@CreatedDate", serverItem.CreatedDate);

        //            sqlConn.Open();

        //            try
        //            {
        //                command.ExecuteNonQuery();
        //            }
        //            finally
        //            {
        //                sqlConn.Close();
        //            }

        //            serverItem.Id = Convert.ToInt32(command.Parameters["@Id"].ToString());

        //            return serverItem;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public ConnectionString InserverConnectionString(ConnectionString connectionStringItem)
        //{
        //    try
        //    {
        //        using (var sqlConn = new SqlConnection(_connnection))
        //        {
        //            var command = new SqlCommand("ConnectionString_ConnectionStrings_Insert", sqlConn);
        //            command.CommandType = CommandType.StoredProcedure;

        //            command.Parameters.Add("@Id", SqlDbType.Int);
        //            command.Parameters.AddWithValue("@Name", connectionStringItem.Name);
        //            command.Parameters.AddWithValue("@Connection", connectionStringItem.Connection);
        //            command.Parameters.AddWithValue("@ServerId", connectionStringItem.Server.Id);
        //            command.Parameters.AddWithValue("@ProviderName", connectionStringItem.ProviderName);
        //            command.Parameters.AddWithValue("@IsActive", connectionStringItem.IsActive);
        //            command.Parameters.AddWithValue("@Creator", connectionStringItem.Creator.Id);
        //            command.Parameters.AddWithValue("@CreatedDate", connectionStringItem.CreatedDate);

        //            sqlConn.Open();

        //            try
        //            {
        //                command.ExecuteNonQuery();
        //            }
        //            finally
        //            {
        //                sqlConn.Close();
        //            }

        //            connectionStringItem.Id = Convert.ToInt32(command.Parameters["@Id"].ToString());

        //            return connectionStringItem;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public bool UpdateServer(DatabaseServer serverItem)
        //{
        //    try
        //    {
        //        using (var sqlConn = new SqlConnection(_connnection))
        //        {
        //            var command = new SqlCommand("ConnectionString_Servers_Update", sqlConn);
        //            command.CommandType = CommandType.StoredProcedure;

        //            command.Parameters.AddWithValue("@Id", serverItem.Id);
        //            command.Parameters.AddWithValue("@Name", serverItem.Name);
        //            command.Parameters.AddWithValue("@ServerIP", serverItem.ServerIP);
        //            command.Parameters.AddWithValue("@IsActive", serverItem.IsActive);
        //            command.Parameters.AddWithValue("@Modifier", serverItem.Modifier.Id);
        //            command.Parameters.AddWithValue("@ModifiedDate", serverItem.ModifiedDate);

        //            sqlConn.Open();

        //            int check = 0;

        //            try
        //            {
        //                check = (int)command.ExecuteNonQuery();
        //            }
        //            finally
        //            {
        //                sqlConn.Close();
        //            }


        //            return check > 1 ? true : false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public bool UpdateServerStatus(int serverItemId, bool isAcitve, int modifierId, DateTime modifiedDate)
        //{
        //    try
        //    {
        //        using (var sqlConn = new SqlConnection(_connnection))
        //        {
        //            var command = new SqlCommand("ConnectionString_Servers_UpdateStatus", sqlConn);
        //            command.CommandType = CommandType.StoredProcedure;

        //            command.Parameters.AddWithValue("@Id", serverItemId);
        //            command.Parameters.AddWithValue("@IsActive", isAcitve);
        //            command.Parameters.AddWithValue("@Modifier", modifierId);
        //            command.Parameters.AddWithValue("@ModifiedDate", modifiedDate);

        //            sqlConn.Open();

        //            int check = 0;

        //            try
        //            {
        //                check = (int)command.ExecuteNonQuery();
        //            }
        //            finally
        //            {
        //                sqlConn.Close();
        //            }


        //            return check > 1 ? true : false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public bool UpdateConnectionString(ConnectionString connectionStringItem)
        //{
        //    try
        //    {
        //        using (var sqlConn = new SqlConnection(_connnection))
        //        {
        //            var command = new SqlCommand("ConnectionString_ConnectionStrings_Update", sqlConn);
        //            command.CommandType = CommandType.StoredProcedure;

        //            command.Parameters.AddWithValue("@Id", connectionStringItem.Id);
        //            command.Parameters.AddWithValue("@Name", connectionStringItem.Name);
        //            command.Parameters.AddWithValue("@Connection", connectionStringItem.Connection);
        //            command.Parameters.AddWithValue("@ServerId", connectionStringItem.Server.Id);
        //            command.Parameters.AddWithValue("@ProviderName", connectionStringItem.ProviderName);
        //            command.Parameters.AddWithValue("@IsActive", connectionStringItem.IsActive);
        //            command.Parameters.AddWithValue("@Modifier", connectionStringItem.Modifier.Id);
        //            command.Parameters.AddWithValue("@ModifiedDate", connectionStringItem.ModifiedDate);

        //            sqlConn.Open();

        //            int check = 0;

        //            try
        //            {
        //                check = (int)command.ExecuteNonQuery();
        //            }
        //            finally
        //            {
        //                sqlConn.Close();
        //            }

        //            return check > 1 ? true : false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public bool UpdateConnectionStringStatus(int connectionStringItemId, bool isActive, int modifierId, DateTime modifiedDate)
        //{
        //    try
        //    {
        //        using (var sqlConn = new SqlConnection(_connnection))
        //        {
        //            var command = new SqlCommand("ConnectionString_ConnectionStrings_UpdateStatus", sqlConn);
        //            command.CommandType = CommandType.StoredProcedure;

        //            command.Parameters.AddWithValue("@Id", connectionStringItemId);
        //            command.Parameters.AddWithValue("@IsActive", isActive);
        //            command.Parameters.AddWithValue("@Modifier", modifierId);
        //            command.Parameters.AddWithValue("@ModifiedDate", modifiedDate);

        //            sqlConn.Open();

        //            int check = 0;

        //            try
        //            {
        //                check = (int)command.ExecuteNonQuery();
        //            }
        //            finally
        //            {
        //                sqlConn.Close();
        //            }

        //            return check > 1 ? true : false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public DatabaseServer GetServer(int serverId)
        //{
        //    try
        //    {
        //        using (var sqlConn = new SqlConnection(_connnection))
        //        {
        //            var command = new SqlCommand("ConnectionString_Servers_GetServer", sqlConn);
        //            command.CommandType = CommandType.StoredProcedure;

        //            command.Parameters.AddWithValue("@Id", serverId);

        //            sqlConn.Open();

        //            var server = new DatabaseServer();

        //            try
        //            {
        //                using (var reader = command.ExecuteReader())
        //                {
        //                    while (reader.Read())
        //                    {
        //                        server.Id = Convert.ToInt32(reader["Id"].ToString());
        //                        server.Name = reader["Name"].ToString();
        //                        server.ServerIP = reader["ServerIP"].ToString();
        //                        server.IsActive = Convert.ToBoolean(reader["IsActive"].ToString());
        //                        server.Creator = reader["Creator"] != null ? new Users
        //                        {
        //                            Id = Convert.ToInt32(reader["Creator"].ToString())
        //                        } : new Users();
        //                        server.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
        //                        server.Modifier = reader["Modifier"] != null ? new Users
        //                        {
        //                            Id = Convert.ToInt32(reader["Modifier"].ToString())
        //                        } : new Users();
        //                        server.ModifiedDate = Convert.ToDateTime(reader["ModifiedDate"].ToString());
        //                    }
        //                }
        //            }
        //            finally
        //            {
        //                sqlConn.Close();
        //            }


        //            return server;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public ConnectionString GetConnectionString(int connectionStringId)
        //{
        //    try
        //    {
        //        using (var sqlConn = new SqlConnection(_connnection))
        //        {
        //            var command = new SqlCommand("ConnectionString_ConnectionStrings_GetConnectionString", sqlConn);
        //            command.CommandType = CommandType.StoredProcedure;

        //            command.Parameters.AddWithValue("@Id", connectionStringId);

        //            sqlConn.Open();

        //            var connectionString = new ConnectionString();

        //            try
        //            {
        //                using (var reader = command.ExecuteReader())
        //                {
        //                    while (reader.Read())
        //                    {
        //                        connectionString.Id = Convert.ToInt32(reader["Id"].ToString());
        //                        connectionString.Name = reader["Name"].ToString();
        //                        connectionString.IsActive = Convert.ToBoolean(reader["IsActive"].ToString());
        //                        connectionString.Creator = reader["Creator"] != null ? new Users
        //                        {
        //                            Id = Convert.ToInt32(reader["Creator"].ToString())
        //                        } : new Users();
        //                        connectionString.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
        //                        connectionString.Modifier = reader["Modifier"] != null ? new Users
        //                        {
        //                            Id = Convert.ToInt32(reader["Modifier"].ToString())
        //                        } : new Users();
        //                        connectionString.ModifiedDate = Convert.ToDateTime(reader["ModifiedDate"].ToString());
        //                        connectionString.Server = new DatabaseServer
        //                        {
        //                            Id = Convert.ToInt32(reader["ServerId"].ToString()),
        //                            Name = reader["ServerName"].ToString(),
        //                            ServerIP = reader["ServerIP"].ToString()
        //                        };
        //                    }
        //                }
        //            }
        //            finally
        //            {
        //                sqlConn.Close();
        //            }


        //            return connectionString;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public IEnumerable<DatabaseServer> GetAllServer()
        //{
        //    try
        //    {
        //        using (var sqlConn = new SqlConnection(_connnection))
        //        {
        //            var command = new SqlCommand("ConnectionString_Servers_GetAll", sqlConn);
        //            command.CommandType = CommandType.StoredProcedure;

        //            sqlConn.Open();

        //            var list = new List<DatabaseServer>();

        //            try
        //            {
        //                using (var reader = command.ExecuteReader())
        //                {
        //                    while (reader.Read())
        //                    {
        //                        var server = new DatabaseServer
        //                        {
        //                            Id = Convert.ToInt32(reader["Id"].ToString()),
        //                            Name = reader["Name"].ToString(),
        //                            ServerIP = reader["ServerIP"].ToString(),
        //                            IsActive = Convert.ToBoolean(reader["IsActive"].ToString()),
        //                            Creator = reader["Creator"] != null ? new Users
        //                            {
        //                                Id = Convert.ToInt32(reader["Creator"].ToString())
        //                            } : new Users(),
        //                            CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString()),
        //                            Modifier = reader["Modifier"] != null ? new Users
        //                            {
        //                                Id = Convert.ToInt32(reader["Modifier"].ToString())
        //                            } : new Users(),
        //                            ModifiedDate = Convert.ToDateTime(reader["ModifiedDate"].ToString())
        //                        };

        //                        list.Add(server);
        //                    }
        //                }
        //            }
        //            finally
        //            {
        //                sqlConn.Close();
        //            }


        //            return list;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        #endregion

        public Dictionary<string, string> ClientGetAll()
        {
            try
            {
                var dic = new Dictionary<string, string>();

                var command = new SqlCommand("ConnectionString_ConnectionStrings_ClientGet", _sqlConn);
                command.CommandType = CommandType.StoredProcedure;

                //command.Notification = null;

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
                                Name = reader["Name"].ToString()
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

                        dic.Add(connection.Alias.Name.ToLower(), tempConnection);
                    }

                    cache.Set<Dictionary<string, string>>(ConectionStringConstanst.PRE_KEY, dic);

                    reader.Close();
                    _sqlConn.Close();
                }

                return dic;
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

            ClientGetAll();
        }
    }
}
