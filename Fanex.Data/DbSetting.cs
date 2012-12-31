// <copyright file="DbSetting.cs" company="Nexcel Solutions Vietnam" >
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
//
// <history>
//   <change who="quangle" date="2012.2.11">Created</change>
//   <change who="Edward.Nguyen" date="2012.12.31">add execute time out</change>
// </history>
namespace Fanex.Data
{
    using System;
    using System.Collections;

    /// <summary>
    /// Describe database setting info:
    /// <para>- SpName: Stored Procedure name</para>
    /// <para>- DBName: The connection string name <see cref="ConnectionString"/></para>
    /// <para>- ConnectionString: decrypted connectionstring of DBName</para>
    /// </summary>
    [Serializable]
    public class DbSetting : IDbSetting
    {
        /// <summary>
        /// Gets or sets the name of the sp.
        /// </summary>
        /// <value>
        /// The name of the sp.
        /// </value>
        public string SpName { get; set; }

        /// <summary>
        /// Gets or sets the name of the db.
        /// </summary>
        /// <value>
        /// The name of the db.
        /// </value>
        public string DbName { get; set; }

        /// <summary>
        /// Gets or sets the custom mapping columns.
        /// </summary>
        /// <value>
        /// The custom mapping columns.
        /// </value>
        public Hashtable CustomMappingColumns { get; set; }

        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        /// <value>
        /// The connection string.
        /// </value>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Get or set execute time out  (seconds)
        /// </summary>
        public int ExecuteTimeOut { get; set; }

        /// <summary>
        /// Determines whether this instance is valid.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </returns>
        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(this.SpName)
                && !string.IsNullOrWhiteSpace(this.DbName)
                && !string.IsNullOrWhiteSpace(this.ConnectionString);
        }
    }
}
