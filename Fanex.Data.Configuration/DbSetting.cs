using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fanex.Caching;
using Microsoft.Practices.Unity;

namespace Fanex.Data.Configuration
{
    [Serializable]
    public class DbSetting : IDbSetting
    {
        public DbSetting(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException();
        }

        
        public string SpName { get; set; }

        public string ConnectionString
        {
            get 
            {
                //if (string.IsNullOrWhiteSpace(DbName))
                //    throw new ArgumentNullException("database name is null or empty");

                //var cacheKey = DataConfiguration.Setting.CacheKey;
                //var connectionString = Cache.Get<string>(cacheKey);

                return string.Empty;
            }
        }

        [Dependency]
        public ICache Cache
        { get; set; }
    }
}
