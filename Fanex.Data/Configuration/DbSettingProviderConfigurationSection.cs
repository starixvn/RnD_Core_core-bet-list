namespace Fanex.Data.Configuration
{
    using System.Configuration;

    /// <summary>
    /// 
    /// </summary>
    public class DbSettingProviderConfigurationSection : ConfigurationSection
    {
        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty("providers")]
        public ProviderSettingsCollection Providers
        {
            get
            {
                return (ProviderSettingsCollection)base["providers"];
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty("default", DefaultValue = "XmlDbSettingProvider")]
        public string Default
        {
            get
            {
                return (string)base["default"];
            }

            set
            {
                base["default"] = value;
            }
        }                
    }
}
