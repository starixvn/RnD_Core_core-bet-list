// --------------------------------------------------------------------------
// <copyright file="ConnectionStringSettings.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.11">Created</change>
// </history>

namespace Fanex.ConnectionString.Configuration
{
    using System.Configuration;
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    internal class ConnectionSettings : ConfigurationSection
    {
        #region consts

        private const string __CONNECTION_STRING_SETTING = "ConnectionSettings";

        private const string __CONNECTION_STRING = "Connection";

        private const string __IS_LOCAL = "IsLocal";

        private const string __FILE_PATH = "FilePath";

        private const string __SITE_ID = "SiteId";

        #endregion

        private static ConnectionSettings _settings = ConfigurationManager.GetSection(__CONNECTION_STRING_SETTING) as ConnectionSettings;

        public static ConnectionSettings Settings
        {
            get
            {
                return _settings;
            }
        }

        [ConfigurationProperty(__CONNECTION_STRING, IsRequired = true)]
        public string Connection
        {
            get { return (string)this[__CONNECTION_STRING]; }
            set { this[__CONNECTION_STRING] = value; }
        }

        [ConfigurationProperty(__IS_LOCAL, IsRequired = false, DefaultValue = true)]
        public bool IsLocal
        {
            get { return (bool)this[__IS_LOCAL]; }
            set { this[__IS_LOCAL] = value; }
        }

        [ConfigurationProperty(__FILE_PATH, IsRequired = true)]
        public string FilePath
        {
            get { return (string)this[__FILE_PATH]; }
            set { this[__FILE_PATH] = value; }
        }

        [ConfigurationProperty(__SITE_ID, IsRequired = false, DefaultValue = 0)]
        public int SiteId
        {
            get { return (int)this[__SITE_ID]; }
            set { this[__SITE_ID] = value; }
        }
    }
}
