// ---------------------------------------------------------------------------------
// <copyright file="ISystemParlayDetail.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------------
// <history>
//     <change who="Jacbo.Phan" date="2012.11.16">Create</change>
// </history>
// ---------------------------------------------------------------------------------

namespace Fanex.BetList.Core.Builder.SystemParlayBuilder
{
    using Fanex.BetList.Core.Entities;

    /// <summary>
    /// Handicap
    /// </summary>
    public interface ISystemParlayDetail
    {
        /// <summary>
        /// Build html
        /// </summary>
        /// <param name="ticketHelper"></param>
        /// <param name="systemParlayData"></param>
        /// <returns></returns>
        string Build(ITicketHelper ticketHelper, ISystemParlayData systemParlayData);
    }
}