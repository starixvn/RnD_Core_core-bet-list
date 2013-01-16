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

        private static Dictionary<string, string> _dic;

        static ConnectionStringManager()
        {
            _dataAccess = new ConnectionStringDataAccess();

            _connectionStringType = Fanex.ConnectionString.Configuration.ConnectionSettings.Settings.IsLocal ?
                ConnectionType.Local : ConnectionType.Remote;

            if (_connectionStringType == ConnectionType.Remote)
            {
                _dataAccess.ClientGetAll();
            }

            _cache = CacheService.CreateInstance(CacheType.MemoryCache);

            _collection = new ConnectionStringSettingsCollection();

            _dic = new Dictionary<string, string>();
        }

        //public static string GetConnectionString(string name)
        //{
        //    try
        //    {
        //        if (_connectionStringType == ConnectionType.Remote)
        //        {                   
        //            _dic = _cache.Get<Dictionary<string, string>>(ConectionStringConstanst.PRE_KEY);

        //            return _dic[name.ToLower()].ToString();
        //        }
        //        else
        //        {
        //            return ConfigurationManager.ConnectionStrings[name].ConnectionString.ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public static ConnectionStringSettingsCollection ConnectionStrings
        {
            get
            {
                _collection.Clear();
                _dic = _cache.Get<Dictionary<string, string>>(ConectionStringConstanst.PRE_KEY);

                foreach (var item in _dic)
                {
                    var itemConnection = new ConnectionStringSettings
                    {
                        Name = item.Key,
                        ConnectionString = item.Value
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
