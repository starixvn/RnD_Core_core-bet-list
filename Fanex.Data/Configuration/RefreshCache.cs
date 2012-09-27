using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using Fanex.Caching;
using System.Collections;
using System.Xml.Linq;
using System.IO;
namespace Fanex.Data.Configuration
{
    [Serializable]
    public class RefreshCache : ICacheItemRefreshAction
    {
        public void Refresh(string keyToremove, object expireTime, CacheItemRemovedReason removeReason)
        {
            EntlibCache cache = new EntlibCache();

            cache.Remove(keyToremove);

            AddSettingDataSourceToCache(keyToremove, cache);
        }

        public void AddSettingDataSourceToCache(string resourcePath, EntlibCache cache)
        {
            //Dictionary<string, DbSetting> dataSource = new Dictionary<string, DbSetting>();
            Hashtable tableKey = new Hashtable();
            try
            {
                var document = XDocument.Load(resourcePath);
                var functions = document.Descendants("Function");

                foreach (var functionItem in functions)
                {
                    var settingList = functionItem.Descendants("Setting");
                    foreach (var setting in settingList)
                    {
                        var dbSetting = new DbSetting
                        {
                            SpName = setting.Attribute("SpName").Value.Trim(),
                            DbName = setting.Attribute("DbName").Value.Trim()
                        };

                        var isEncryptedConn = IsEncryptedConnection(setting);

                        dbSetting.ConnectionString = ConnectionStringGetter.GetConnectionString(dbSetting.DbName, isEncryptedConn);

                        var key = setting.Attribute("Key").Value.Trim();
                        if (!string.IsNullOrWhiteSpace(key))
                        {
                            //CacheManager.Set<DbSetting>(key, dbSetting);
                            tableKey.Add(key, dbSetting);
                        }
                    }
                }

                FileDependency fileDependency = new FileDependency(resourcePath);
                cache.Set<Hashtable>(resourcePath, tableKey, new ICacheItemExpiration[] { fileDependency });
            }
            catch (IOException ex)
            {
                throw ex;
            }

        }
        private static bool IsEncryptedConnection(XElement xEle)
        {
            var xAttr = xEle.Attribute("Plain");
            var isPlaintConnection = (xAttr != null
                           && (xAttr.Value.Equals("1") || xAttr.Value.Equals("false", StringComparison.OrdinalIgnoreCase)));

            return !isPlaintConnection;
        }
    }

}
