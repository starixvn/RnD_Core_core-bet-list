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
    using System.Collections.Generic;
    using System.Collections;
    using Fanex.ConnectionString.Domain;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class ConnectionStringManager
    {
        private static readonly ConnectionStringDataAccess _dataAccess;

        private static readonly ICache _cache;

        private static ConnectionType _connectionStringType;

        private static ConnectionStringSettingsCollection _collection;

        private static List<ConnectionString> _list;

        static ConnectionStringManager()
        {
            _dataAccess = ConnectionStringDataAccess.InstanceCreation();

            _connectionStringType = Fanex.ConnectionString.Configuration.ConnectionSettings.Settings.IsLocal ?
                ConnectionType.Local : ConnectionType.Remote;

            if (_connectionStringType == ConnectionType.Remote)
            {
                _dataAccess.GetAll();
            }

            _cache = CacheService.CreateInstance(CacheType.MemoryCache);

            _collection = new ConnectionStringSettingsCollection();

            _list = new List<ConnectionString>();
        }

        public static ConnectionStringSettingsCollection ConnectionStrings
        {
            get
            {
                _collection.Clear();
                _list = _cache.Get<List<ConnectionString>>(ConectionStringConstanst.PRE_KEY);

                foreach (var item in _list)
                {
                    var itemConnection = new ConnectionStringSettings
                    {
                        Name = item.Alias.Name.ToLower(),
                        ConnectionString = item.Connection
                    };

                    _collection.Add(itemConnection);
                }

                return _collection;
            }
        }

        public static bool Contains(string name)
        {
            try
            {
                if (_connectionStringType == ConnectionType.Remote)
                {
                    _collection.Clear();
                    _list = _cache.Get<List<ConnectionString>>(ConectionStringConstanst.PRE_KEY);

                    foreach (var item in _list)
                    {
                        if (item.Alias.Name == name.ToLower())
                        {
                            return true;
                        }
                    }

                    return false;
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
