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
    /// Bet Type: Handicap.
    /// </summary>
    public interface ISystemParlayDetail
    {
        /// <summary>
        /// Builds the specified ticket helper.
        /// </summary>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <param name="systemParlayData">The system parlay data.</param>
        /// <returns>System.String: HTML string represents the System Parlay detail.</returns>
        string Build(ITicketHelper ticketHelper, ISystemParlayData systemParlayData);
    }
}