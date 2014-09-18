// ---------------------------------------------------------------------------------
// <copyright file="BetListHTMLBuilderExt.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------------
// <history>
//     <change who="Marc.Bui" date="2014.04.29">Create</change>
// </history>
// ---------------------------------------------------------------------------------

namespace Fanex.BetList.Example.Extension.BetListHTMLBuilderExt
{
    using Fanex.BetList.Core.Builder;
    using Fanex.BetList.Core.Entities;

    /// <summary>
    /// Class BetListHTMLBuilderExt.
    /// </summary>
    public static class BetListHTMLBuilderExt
    {
        /// <summary>
        /// Adds the no.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="ticket">The ticket.</param>
        /// <returns>Index string.</returns>
        public static string BuildUsername(this BetListHTMLBuilder factory, ITicket ticket)
        {
            return ticket.UserName;
        }
    }
}