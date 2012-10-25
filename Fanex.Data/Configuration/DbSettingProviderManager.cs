namespace Fanex.Data.Configuration
{
    using System;
    using System.Configuration;
    using System.Web.Configuration;

    /// <summary>
    /// 
    /// </summary>
    public class DbSettingProviderManager
    {
        /// <summary>
        /// 
        /// </summary>
        private static DbSettingProviderBase _defaultProvider;

        /// <summary>
        /// 
        /// </summary>
        private static DbSettingProviderCollection _providers;

        /// <summary>
        /// Constructor
        /// </summary>
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
            {
                throw new ConfigurationErrorsException
                    ("Fanex.Data.DbSettingProvider configuration section is not set correctly.");
            }

            _providers = new DbSettingProviderCollection();

            ProvidersHelper.InstantiateProviders(
                configuration.Providers,
                _providers,
                typeof(DbSettingProviderBase));

            _providers.SetReadOnly();

            _defaultProvider = _providers[configuration.Default];

            if (_defaultProvider == null)
            {
                throw new Exception(configuration.Default);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static DbSettingProviderBase Current
        {
            get
            {
                return _defaultProvider;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static DbSettingProviderCollection ProviderCollection
        {
            get
            {
                return _providers;
            }
        }
    }
}
