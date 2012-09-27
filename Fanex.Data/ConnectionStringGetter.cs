// <copyright file="ConnectionStringGetter.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved. 
// </copyright>
//
// <summary>
//  Get connection string, decrypt if required 
// </summary>
//
// <history>
//  <change who="Anders" date="2011.12.14">Created</change>
//  <change who="Anders" date="2011.12.29">Change class name to ConnectionStringHelper, refactor</change>
//  <change who="Anders" date="2011.12.29">Enhance caching mechanism</change>
//  <change who="Anders.le" date="2012.3.22">Correct stylecop</change>
// </history>

namespace Fanex.Data
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Configuration;
    using Fanex.Common;

    /// <summary>
    /// Use connection string name to get connection string and decrypt it if required
    /// Save to cache for reuse
    /// </summary>
    public static class ConnectionStringGetter
    {
        /// <summary>
        /// Cache key of connection string
        /// </summary>
        private const string CacheKeyFormat = "{0}:ConnectionString";

        /// <summary>
        /// contain connection string key by key
        /// </summary>
        private static volatile StringDictionary connDictionary = new StringDictionary();

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <param name="connectionStringName">Name of the db.</param>
        /// <param name="isEncryptedConnection">if set to <c>true</c> [is encrypted connection].</param>
        /// <returns>plain connection string</returns>
        public static string GetConnectionString(string connectionStringName, bool isEncryptedConnection = true)
        {
            if (string.IsNullOrWhiteSpace(connectionStringName))
            {
                throw new ArgumentNullException();
            }

            var connectionString = string.Empty;
            string cacheKey = string.Format(CacheKeyFormat, connectionStringName);

            if (connDictionary.ContainsKey(cacheKey))
            {
                connectionString = connDictionary[cacheKey];
            }
            else
            {
                if (ConfigurationManager.ConnectionStrings[connectionStringName] == null)
                {
                    throw new NullReferenceException(Resources.CannotFindConnectionStringName + connectionStringName);
                }

                connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;

                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    throw new NullReferenceException(connectionStringName + Resources.ConnectionStringIsNullOrEmpty);
                }

                // decrypt connection string
                if (isEncryptedConnection)
                {
                    connectionString = Cryptography.DeCryptConnectionString(connectionString);
                }

                if (!connDictionary.ContainsKey(cacheKey))
                {
                    connDictionary.Add(cacheKey, connectionString);
                }
            }

            return connectionString;
        }
    }
}
