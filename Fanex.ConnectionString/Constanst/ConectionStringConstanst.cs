// --------------------------------------------------------------------------
// <copyright file="ConectionStringConstanst.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.11">Created</change>
// </history>

namespace Fanex.ConnectionString.Constanst
{
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    internal static class ConectionStringConstanst
    {
        public const int GETALL = 0;

        public const string PREKEY = "Fanex_ConnectionString_";

        public const string CONNECTIONFORMAT = "Data Source={0};Initial Catalog={1};integrated security={2}; User ID={3};Password={4};{5}";
    }
}
