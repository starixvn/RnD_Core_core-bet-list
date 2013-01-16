// --------------------------------------------------------------------------
// <copyright file="IDataAccess.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.11">Created</change>
// </history>

namespace Fanex.ConnectionString.DataAccess
{
    using System;
    using System.Collections.Generic;
    using Fanex.ConnectionString.Domain;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IConnectionStringDataAccess
    {
        #region comment code

        //DatabaseServer InsertSever(DatabaseServer serverItem);

        //ConnectionString InserverConnectionString(ConnectionString connectionStringItem);

        //bool UpdateServer(DatabaseServer serverItem);

        //bool UpdateServerStatus(int serverItemId, bool isAcitve, int modifierId, DateTime modifiedDate);

        //bool UpdateConnectionString(ConnectionString connectionStringItem);

        //bool UpdateConnectionStringStatus(int connectionStringItemId, bool isActive, int modifierId, DateTime modifiedDate);

        //DatabaseServer GetServer(int serverId);

        //ConnectionString GetConnectionString(int connectionStringId);

        //IEnumerable<DatabaseServer> GetAllServer();

        #endregion      

        IEnumerable<ConnectionString> ClientGetAll();

    }
}
