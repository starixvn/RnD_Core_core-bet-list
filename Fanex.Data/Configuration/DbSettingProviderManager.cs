using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Web.Configuration;


namespace Fanex.Data.Configuration
{
    public class DbSettingProviderManager
    {
        private static DbSettingProviderBase _defaultProvider;
        private static DbSettingProviderCollection _providers;

        static DbSettingProviderManager()
        {
            Initialize();
        }

        /// <summary>
        /// load all configuration
        /// </summary>
        private static void Initialize()
        {
            //var exeConfiguration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var configuration = (DbSettingProviderConfigurationSection)ConfigurationManager.GetSection("Fanex.Data.DbSettingProvider");

            if (configuration == null)
                throw new ConfigurationErrorsException
                    ("Fanex.Data.DbSettingProvider configuration section is not set correctly.");

            _providers = new DbSettingProviderCollection();

            ProvidersHelper.InstantiateProviders(configuration.Providers
                , _providers, typeof(DbSettingProviderBase));

            _providers.SetReadOnly();

            _defaultProvider = _providers[configuration.Default];

            if (_defaultProvider == null)
                throw new Exception(configuration.Default);
        }

        public static DbSettingProviderBase Current
        {
            get
            {
                return _defaultProvider;
            }
        }


        public static DbSettingProviderCollection ProviderCollection
        {
            get
            {
                return _providers;
            }
        }
    }
}
