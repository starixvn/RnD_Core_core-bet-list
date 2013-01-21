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
    using System.Collections.Generic;
    using System.Configuration;
    using System.Diagnostics.CodeAnalysis;
    using Fanex.Caching;
    using Fanex.ConnectionString.Constanst;
    using Fanex.ConnectionString.DataAccess;
    using Fanex.ConnectionString.Domain;

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public static class ConnectionStringManager
    {
        #region Member

        private static readonly ConnectionStringDataAccess _dataAccess;

        private static readonly ICache _cache;

        private static ConnectionType _connectionStringType;

        private static ConnectionStringSettingsCollection _collection;

        private static List<ConnectionString> _list;

        #endregion

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
                try
                {
                    _collection.Clear();
                    _list = _cache.Get<List<ConnectionString>>(ConectionStringConstanst.PREKEY);

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
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static bool Contains(string name)
        {
            try
            {
                if (_connectionStringType == ConnectionType.Remote)
                {
                    _collection.Clear();
                    _list = _cache.Get<List<ConnectionString>>(ConectionStringConstanst.PREKEY);

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
