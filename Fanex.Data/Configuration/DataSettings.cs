// <copyright file="DataSettings.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved. 
// </copyright>
//
// <summary>
//  Data Settings and configuration info
// </summary>
//
// <history>
//  <change who="Anders" date="2011.12.14">Created</change>
// </history>

using System;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

namespace Fanex.Data.Configuration
{
    /// <summary>
    /// Represents a configuration section that can be serialized and deserialized to XML.
    /// </summary>
    public class DataSettings : ConfigurationSection
    {

        private const string SettingCacheKey = "cacheKey";
        private const string SettingConfigFile = "configFile";

        /// <summary>
        /// The name of the configuration section.
        /// </summary>
        public static string SectionName
        {
            get { return "Fanex.Data"; }
        }

        public static DataSettings GetSettings(IConfigurationSource configurationSource)
        {
            if (configurationSource == null) throw new ArgumentNullException("configurationSource");
            return configurationSource.GetSection(SectionName) as DataSettings;
        }

        /// <summary>
        /// The path to the msmq to use.
        /// </summary>
        [ConfigurationProperty(SettingCacheKey, IsRequired = true)]
        public string CacheKey
        {
            get { return (string)base[CacheKey]; }
            set { base[CacheKey] = value; }
        }

        /// <summary>
        /// Project name can be used as an specific category
        /// </summary>
        [ConfigurationProperty(SettingConfigFile, IsRequired = true)]
        public string ConfigFile
        {
            get { return (string)base[SettingConfigFile]; }
            set { base[SettingConfigFile] = value; }
        }
    }
}
