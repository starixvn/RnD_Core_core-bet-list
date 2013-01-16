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

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal class FanexConnectionSettings : ConfigurationSection
    {
        #region consts

        private const string __CONNECTION_STRING_SETTING = "ConnectionStringSettings";

        private const string __CONNECTION_STRING = "Connnection";

        private const string __IS_LOCAL = "IsLocal";

        #endregion

        private static FanexConnectionSettings _settings = ConfigurationManager.GetSection(__CONNECTION_STRING_SETTING) as FanexConnectionSettings;

        public static FanexConnectionSettings Settings
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
    }
}
