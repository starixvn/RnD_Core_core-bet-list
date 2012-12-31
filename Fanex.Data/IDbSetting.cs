// <copyright file="IDbSetting.cs" company="Nexcel Solutions Vietnam" >
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
    /// define db setting properties
    /// </summary>
    public interface IDbSetting
    {
        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        /// <value>
        /// The connection string.
        /// </value>
        string ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the name of the db.
        /// </summary>
        /// <value>
        /// The name of the db.
        /// </value>
        string DbName { get; set; }

        /// <summary>
        /// Gets or sets the name of the sp.
        /// </summary>
        /// <value>
        /// The name of the sp.
        /// </value>
        string SpName { get; set; }

        /// <summary>
        /// Gets or sets the custom mapping columns.
        /// </summary>
        /// <value>
        /// The custom mapping columns.
        /// </value>
        Hashtable CustomMappingColumns { get; set; }

        /// <summary>
        /// get or set execute time out (seconds)
        /// </summary>
        int ExecuteTimeOut { get; set; }

        /// <summary>
        /// Determines whether this instance is valid.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </returns>
        bool IsValid();
    }
}
