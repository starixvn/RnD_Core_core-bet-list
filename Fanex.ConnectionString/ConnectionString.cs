// --------------------------------------------------------------------------
// <copyright file="ConnectionString.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.11">Created</change>
// </history>

namespace Fanex.ConnectionString
{
    using System;
    using Fanex.Caching;
    using Fanex.ConnectionString.Constanst;
    using Fanex.ConnectionString.DataAccess;
    using System.Configuration;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ConnectionString : IConnnectionString
    {
        private readonly ConnectionStringDataAccess _dataAccess;

        private readonly ICache cache;

        private volatile static ConnectionString _connectionStringObject;

        private ConnectionType _connectionStringType;

        private ConnectionString()
        {
            _dataAccess = new ConnectionStringDataAccess();

            _connectionStringType = Fanex.ConnectionString.Configuration.ConnectionStringSettings.Settings.IsLocal ?
                ConnectionType.Local : ConnectionType.Remote;

            if (_connectionStringType == ConnectionType.Remote)
            {
                _dataAccess.ClientGetAll();
            }

            cache = CacheService.CreateInstance(CacheType.MemoryCache);
        }

        public static ConnectionString InstanceCreation()
        {
            object lockingObject = new object();
            if (_connectionStringObject == null)
            {
                lock (lockingObject)
                {
                    if (_connectionStringObject == null)
                    {
                        _connectionStringObject = new ConnectionString();
                    }
                }
            }

            return _connectionStringObject;
        }      

        public string GetConnectionString(string name)
        {
            try
            {
                if (_connectionStringType == ConnectionType.Remote)
                {
                    return cache.Get<string>(ConectionStringConstanst.PRE_KEY + name.ToLower());
                }
                else
                {
                    return ConfigurationManager.ConnectionStrings[name].ConnectionString.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Clear()
        {
            try
            {
                cache.Clear();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Reset()
        {
            try
            {
                _dataAccess.ClientGetAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Contains(string name)
        {
            try
            {
                if (_connectionStringType == ConnectionType.Remote)
                {
                    return cache.Contains(ConectionStringConstanst.PRE_KEY + name.ToLower());
                }
                else
                {
                    if (ConfigurationManager.ConnectionStrings[name] != null)
                    {
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
