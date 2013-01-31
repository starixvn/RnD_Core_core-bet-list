// --------------------------------------------------------------------------
// <copyright file="ConnectionType.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.14">Created</change>
// </history>

namespace Fanex.ConnectionString
{
    internal enum ConnectionType
    {
        Local = 0,
        Remote = 1
    }

    internal enum SqlWatcherNotificationType
    {
        Blocking = 0,
        Threaded = 1
    }
}
