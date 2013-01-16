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
    using System.Configuration.Internal;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class ConnectionStringManager
    {
        #region Member

        private static readonly ConnectionStringDataAccess _dataAccess;

        private static readonly ICache _cache;

        private static ConnectionType _connectionStringType;

        #endregion

        static ConnectionStringManager()
        {
            _dataAccess = new ConnectionStringDataAccess();

            _connectionStringType = Fanex.ConnectionString.Configuration.FanexConnectionSettings.Settings.IsLocal ?
                ConnectionType.Local : ConnectionType.Remote;

            if (_connectionStringType == ConnectionType.Remote)
            {
                _dataAccess.GetAll();
            }

            _cache = CacheService.CreateInstance(CacheType.MemoryCache);
        }

        public static string ConnectionString(string name)
        {
            try
            {
                if (_connectionStringType == ConnectionType.Remote)
                {
                    return _cache.Get<string>(ConectionStringConstanst.PRE_KEY + name.ToLower());
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

        public static bool Contains(string name)
        {
            try
            {
                if (_connectionStringType == ConnectionType.Remote)
                {
                    return _cache.Contains(ConectionStringConstanst.PRE_KEY + name.ToLower());
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

    }
}
