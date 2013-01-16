// --------------------------------------------------------------------------
// <copyright file="IConnnectionString.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.11">Created</change>
// </history>

namespace Fanex.ConnectionString
{
    using System;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IConnnectionString : IDisposable
    {
        string GetConnectionString(string name);

        void Clear();

        void Reset();

        bool Contains(string name);
    }
}
