//---------------------------------------------------------------------------------
// <copyright file="XmlDbSettingProvider.cs" company="Nexcel Solutions Vietnam">
//      Copyright (c) Nexcel Solutions Vietnam. All rights reserved. 
// </copyright>
// <summary>
// </summary>
// <history>
// <change who="Anders" date="2011.12.27">Created & Implement</change>
// <change who="Anders" date="2011.12.28">Add attribute to check Encrypted Connection</change>
// <change who="Anders" date="2011.12.31">Add file system watcher</change>
// <change who="Anders" date="2012.01.02">Add condition to check enable watching</change>
//	<change who="anders.le" date="2012.3.21">Remove Fanex.Caching and Fanex.Common.IO</change>
// </history>
//---------------------------------------------------------------------------------
namespace Fanex.Data.Configuration
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.IO;
    using System.Linq;
    using System.Runtime.Caching;
    using System.Security.Permissions;
    using System.Xml.Linq;    
    using Microsoft.Practices.EnterpriseLibrary.Caching;

    /// <summary>
    /// Xml setting provider
    /// </summary>
    public class XmlDbSettingProvider : DbSettingProviderBase
    {
        /// <summary>
        /// syncRoot to check thread safe when implementing singleton pattern
        /// </summary>
        protected static object syncRoot = new object();

        /// <summary>
        /// resource watcher to watch resource change
        /// </summary>
        protected FileSystemWatcherEx resourceWatcher;

        /// <summary>
        /// database key name
        /// </summary>
        protected const string DbSettingCacheKeyName = "XmlDbSettingProvider:DBSetting";

        /// <summary>
        /// enableWatching config key name
        /// </summary>
        private const string EnableWatchingName = "enableWatching";

        /// <summary>
        /// cache manager
        /// </summary>
        private static volatile ObjectCache cacheManager;

        /// <summary>
        /// get setting data source from cache
        /// </summary>
        public override Dictionary<string, IDbSetting> SettingDataSource
        {
            get
            {
                return this.CacheManager.Get(DbSettingCacheKeyName) as Dictionary<string, IDbSetting>;
            }
        }

        #region Caching Object
        /// <summary>
        /// Gets the memory cache instance
        /// </summary>
        protected ObjectCache CacheManager
        {
            /// <summary>
            /// Cache manager singleton
            /// </summary>
            get
            {
                if (cacheManager == null)
                {
                    lock (syncRoot)
                    {
                        if (cacheManager == null)
                        {
                            cacheManager = MemoryCache.Default;
                        }
                    }
                }

                return cacheManager;
            }
        }

        /// <summary>
        /// should run it before any event in application start event
        /// </summary>
        public override void Start()
        {
            this.AddDataSourceToCache(ResourcePath);
        }

        /// <summary>
        /// insert db setting to resource
        /// </summary>
        /// <param name="setting">setting typeof IDbSetting</param>
        public override void InsertSetting(IDbSetting setting)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// update resource setting
        /// </summary>
        /// <param name="setting">IDbSetting setting</param>
        public override void UpdateSetting(IDbSetting setting)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// delete resource setting
        /// </summary>
        /// <param name="key">string key</param>
        public override void DeleteSetting(string key)
        {
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        /// Init resource watch
        /// <see cref="http://msdn.microsoft.com/en-us/library/system.io.filesystemwatcher.aspx"/>
        /// </summary>
        /// <param name="name">name of config</param>
        /// <param name="config">config name value collection</param>
        public override void Initialize(string name, NameValueCollection config)
        {
            base.Initialize(name, config);

            // check configuration to start watch resource file
            if (config[EnableWatchingName] != null)
            {
                if (config[EnableWatchingName] == "1"
                    || config[EnableWatchingName].Equals("true", StringComparison.OrdinalIgnoreCase))
                {
                    this.StartWatchResource(ResourcePath);
                }
            }
        }

        #region protected functions
        /// <summary>
        /// check whether connection was encrypted or not
        /// </summary>
        /// <param name="element">XElement element</param>
        /// <returns>bool value, true if connection requires encrypted</returns>
        protected static bool IsEncryptedConnection(XElement element)
        {
            var attr = element.Attribute("Plain");
            var isPlaintConnection = attr != null
                           && (attr.Value.Equals("1") || attr.Value.Equals("true", StringComparison.OrdinalIgnoreCase));

            return !isPlaintConnection;
        }

        /// <summary>
        /// start run file system watcher to fire event ResourceChangedEvent
        /// whenever the resource change
        /// </summary>
        /// <param name="path">resource path</param>
        protected virtual void StartWatchResource(string path)
        {           
            this.EnsureTargetFileAccessible(path);

            // remember to check security on the folter to watch
            var resourceWatcher = new FileSystemWatcherEx();
            resourceWatcher.Path = Path.GetDirectoryName(path);

            // "*.xml";
            resourceWatcher.Filter = Path.GetFileName(path);
            resourceWatcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;
            resourceWatcher.Changed += new FileSystemEventHandler(this.ResourceChangedEvent);
            resourceWatcher.IncludeSubdirectories = false;

            // Begin watching.
            resourceWatcher.EnableRaisingEvents = true;
        }

        /// <summary>
        /// Check file is accessible
        /// </summary>
        /// <param name="path">valid path</param>
        protected void EnsureTargetFileAccessible(string path)
        {
            FileIOPermission permission = new FileIOPermission(FileIOPermissionAccess.Read, path);
            permission.Demand();
        }

        /// <summary>
        /// Resource change - reload new resource and add to cache
        /// </summary>
        /// <param name="sender">file change</param>
        /// <param name="e">FileSystemEventArgs args</param>
        protected void ResourceChangedEvent(object sender, FileSystemEventArgs e)
        {          
            this.AddDataSourceToCache(ResourcePath);
        }

        /// <summary>
        /// Get all db setting configuration from the data source and than add to cache
        /// </summary>
        /// <param name="resourcePath">path to resource file</param>        
        protected virtual void AddDataSourceToCache(string resourcePath)
        {
            lock (syncRoot)
            {
                var settingDataSource = new Dictionary<string, IDbSetting>();
                try
                {
                    var document = XDocument.Load(resourcePath);
                    var functions = document.Descendants("Function");

                    foreach (var functionItem in functions)
                    {
                        var settingList = functionItem.Descendants("Setting");
                        foreach (var setting in settingList)
                        {
                            var databaseSetting = new DbSetting
                            {
                                SpName = setting.Attribute("SpName").Value.Trim(),
                                DbName = setting.Attribute("DbName").Value.Trim()
                            };

                            // Add custom column for spname
                            var customColumList = setting.Descendants("Mapping");
                            Hashtable customMapping = new Hashtable();
                            foreach (var item in customColumList)
                            {
                                string dataField = item.Attribute("DataField").Value.Trim();
                                string objectProperty = item.Attribute("ObjectProperty").Value.Trim();
                                customMapping.Add(dataField, objectProperty);
                            }

                            databaseSetting.CustomMappingColumns = customMapping;

                            // check connection string whether require to decrypt or not
                            var isEncryptedConn = IsEncryptedConnection(setting);
                            databaseSetting.ConnectionString = ConnectionStringGetter.GetConnectionString(databaseSetting.DbName, isEncryptedConn);

                            var key = setting.Attribute("Key").Value.Trim();
                            if (!string.IsNullOrWhiteSpace(key))
                            {
                                if (databaseSetting.IsValid())
                                {
                                    if (!settingDataSource.ContainsKey(key))
                                    {
                                        settingDataSource.Add(key, databaseSetting);
                                    }
                                }
                            }
                        }
                    }

                    // update to cache
                    this.CacheManager.Set(DbSettingCacheKeyName, settingDataSource, this.GetCachePolicy());
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Gets the cache policy.
        /// </summary>
        /// <returns>return CacheItemPolicy</returns>
        protected CacheItemPolicy GetCachePolicy()
        {
            return null;
        }

        #endregion
    }
}
