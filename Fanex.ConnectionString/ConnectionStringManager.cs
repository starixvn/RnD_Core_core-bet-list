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
    using Fanex.ConnectionString.Constant;
    using Fanex.ConnectionString.ConnectionWatcher;
    using Fanex.ConnectionString.Configuration;

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public static class ConnectionStringManager
    {
        #region Member

        private static readonly ConnectionStringWatcher _dataAccess;

        private static readonly ICache _cache;

        private static ConnectionType _connectionStringType;

        private static int _siteId;

        #endregion

        static ConnectionStringManager()
        {
            _siteId = ConnectionSettings.Settings.SiteId;

            _dataAccess = new ConnectionStringWatcher(_siteId);

            _connectionStringType = ConnectionSettings.Settings.IsLocal ? ConnectionType.Local : ConnectionType.Remote;

            if (_connectionStringType == ConnectionType.Remote)
            {
                _dataAccess.Fetch();
            }

            _cache = CacheService.CreateInstance(CacheType.MemoryCache);
        }

        public static ConnectionStringSettingsCollection ConnectionStrings
        {
            get
            {
                try
                {
                    var collection = new ConnectionStringSettingsCollection();

                    if (_connectionStringType == ConnectionType.Remote)
                    {
                        collection = _cache.Get<ConnectionStringSettingsCollection>(ConnectionStringConstant.PREKEY);
                    }
                    else
                    {
                        collection = ConfigurationManager.ConnectionStrings;
                    }

                    return collection;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static void Stop()
        {
            _dataAccess.Dispose();
        }
    }
}
