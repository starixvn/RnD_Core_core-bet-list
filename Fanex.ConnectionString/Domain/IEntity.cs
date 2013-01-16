// --------------------------------------------------------------------------
// <copyright file="IEntity.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.11">Created</change>
// </history>

namespace Fanex.ConnectionString.Domain
{
    public interface IEntity<TId, TName>
    {
        TId Id { get; set; }

        TName Name { get; set; }
    }
}
