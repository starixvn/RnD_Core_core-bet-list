// ---------------------------------------------------------------------------------
// <copyright file="BetListHTMLFactory.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------------
// <history>
//     <change who="Marc.Bui" date="2012.11.16">Create</change>
// </history>
// ---------------------------------------------------------------------------------

namespace Fanex.BetList.Core.Factories
{
    using Fanex.BetList.Core.Builder;
    using Fanex.BetList.Core.Entities;    

    /// <summary>
    /// Class BetListHTMLFactory.
    /// </summary>
    public class BetListHTMLFactory : IBetListFactory
    {
        /// <summary>
        /// Creates the builder.
        /// </summary>
        /// <returns>BetListHTMLBuilder object.</returns>
        public BetListHTMLBuilder CreateBuilder()
        {
            var builder = new BetListHTMLBuilder();
            return builder;
        }
    }
}