using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fanex.Caching;
using Fanex.Data.Configuration;

namespace Fanex.Data
{
    public static class ObjectDBFactory
    {
        public static ObjectDB GetObjectDB(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException();

            DbSetting setting = DbSettingProviderManager.Current.GetSetting(key);

            return new ObjectDB(setting);
        }        
    }
}
