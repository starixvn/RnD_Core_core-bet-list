//---------------------------------------------------------------------------------
// <copyright file="MultipleXmlDbSettingProvider.cs" company="Nexcel Solutions Vietnam">
//      Copyright (c) Nexcel Solutions Vietnam. All rights reserved. 
// </copyright>
// <summary>
// </summary>
// <history>
// <change who="Anders" date="2011.20.3">Created & Implement</change>
// </history>
//---------------------------------------------------------------------------------
namespace Fanex.Data.Configuration
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;

    /// <summary>
    /// Support multi setting xml resource files
    /// </summary>
    public class XmlDbSettingProvider2 : XmlDbSettingProvider
    {
        /// <summary>
        /// start run file system watcher to fire event Resource Changed Event
        /// whenever the resource change
        /// </summary>
        /// <param name="path">resource path</param>
        protected override void StartWatchResource(string path)
        {
            this.EnsureTargetFileAccessible(path);

            // remember to check security on the folter to watch
            var resourceWatcher = new FileSystemWatcherEx();
            resourceWatcher.Path = Path.GetDirectoryName(path);

            // just monitor files whose extension is xml only
            resourceWatcher.Filter = "*.xml";
            resourceWatcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.Size;

            // 2s is recent change events
            resourceWatcher.Interval = 2000;

            // Add event handler
            resourceWatcher.Changed += new FileSystemEventHandler(this.ResourceChangedEvent);
            resourceWatcher.Created += new FileSystemEventHandler(this.ResourceChangedEvent);
            resourceWatcher.Deleted += new FileSystemEventHandler(this.ResourceChangedEvent);
            resourceWatcher.IncludeSubdirectories = false;

            // Enable watching.
            resourceWatcher.EnableRaisingEvents = true;
        }

        /// <summary>
        /// Get all db setting configuration from the data source and than add to cache
        /// </summary>
        /// <param name="resourcePath">path to resource file for folder</param>        
        protected override void AddDataSourceToCache(string resourcePath)
        {
            lock (syncRoot)
            {
                string[] listOfResouces;
                if (IsFolder(resourcePath))
                {
                    // just get xml file
                    listOfResouces = Directory.GetFiles(resourcePath);
                    listOfResouces = listOfResouces.ToList<string>()
                         .Where(path
                             => Path.GetExtension(path).Equals(".xml", StringComparison.OrdinalIgnoreCase))
                         .Select(s => s).ToArray<string>();
                }
                else
                {
                    listOfResouces = new string[] { resourcePath };
                }

                int resourceLength = listOfResouces.Length;

                try
                {
                    var settingDataSource = new Dictionary<string, IDbSetting>();

                    for (int i = 0; i < resourceLength; i++)
                    {
                        var document = XDocument.Load(listOfResouces[i]);
                        var functions = document.Descendants("Function");

                        foreach (var functionItem in functions)
                        {
                            var settingList = functionItem.Descendants("Setting");
                            foreach (var setting in settingList)
                            {
                                var databaseSetting = new DbSetting
                                {
                                    SpName = setting.Attribute("SpName").Value.Trim(),
                                    DbName = setting.Attribute("DbName").Value.Trim(),
                                    ExecuteTimeOut = setting.Attribute("TimeOut") != null ? Convert.ToInt32(setting.Attribute("TimeOut").Value.Trim()) : TimeOutDefault
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
    }
}
