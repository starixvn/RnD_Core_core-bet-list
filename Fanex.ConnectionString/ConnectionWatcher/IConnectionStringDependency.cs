// --------------------------------------------------------------------------
// <copyright file="IDataAccess.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.11">Created</change>
// </history>

namespace Fanex.ConnectionString.SqlDependency
{
    using System;
    using System.Collections.Generic;    
    using System.Configuration;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal interface IConnectionStringDependency : IDisposable
    {
        ConnectionStringSettingsCollection Fetch();
    }
}
