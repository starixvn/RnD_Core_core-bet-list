// ---------------------------------------------------------------------------------
// <copyright file="ITicketHelper.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------------
// <history>
//     <change who="Marc.Bui" date="2012.11.16">Create</change>
//     <change who="Marc.Bui" date="2012.12.19">Remove GetMixParlayDetail</change>
//     <change who="Marc.Bui" date="2012.12.26">Add awayId to GetHorseTeamNameById in case some teams use awayId to resolve sport name</change>
//     <change who="Marc.Bui" date="2013.03.11">Add ClientDetailSystemParlayFunction for easy customization</change>
//     <change who="Marc.Bui" date="2013.03.19">Add ClientSystemParlaySubbetsDetailFunction & ClientMixParlaySubbetsDetailFunction</change>
//     <change who="Marc.Bui" date="2013.10.23">Refactor & apply stylecop.</change>
// </history>
// ---------------------------------------------------------------------------------

namespace Fanex.BetList.Core.Entities
{
    using System.Collections.Generic;

    public delegate string GetCachePropertyById(object id);

    public delegate IList<ITicket> GetParlayTicketsById(object id);

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface ITicketHelper
    {
        int Index { get; set; }

        string CurrentLanguage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the current render show Odds values for each ticket (Ex: in MixParlay sub-tickets).
        /// </summary>
        /// <value><c>true</c> if odds is shown with choice; otherwise, <c>false</c>.</value>
        bool IsShowOddsWithChoice { get; set; }

        /// <summary>
        /// Gets or sets custom JS function to handle SystemParlay ONCLICK event, rendering win-loss summary.
        /// If not defined, the default will be used.
        /// </summary>
        /// <value>The client detail system parlay function.</value>
        string ClientDetailSystemParlayFunction { get; set; }

        /// <summary>
        /// Gets or sets custom JS function to handle SystemParlay ONCLICK event, rendering sub-bets.
        /// If not defined, the default will be used.
        /// </summary>
        /// <value>The client system parlay sub-bets detail function.</value>
        string ClientSystemParlaySubbetsDetailFunction { get; set; }

        /// <summary>
        /// Gets or sets custom JS function to handle MixParlay ONCLICK event, rendering sub-bets.
        /// If not defined, the default will be used.
        /// </summary>
        /// <value>The client mix parlay sub-bets detail function.</value>
        string ClientMixParlaySubbetsDetailFunction { get; set; }

        bool ShowBetType { get; set; }

        bool ShowMatch { get; set; }

        bool ShowLeague { get; set; }

        bool ShowSystemParlayDetail { get; set; }

        bool ShowSystemParlayPlayerComm { get; set; }

        /// <summary>
        /// Gets or sets Method to get *Parlay details.
        /// </summary>
        /// <value>Function to get parlay detail by Id.</value>
        GetParlayTicketsById GetParlayDetailById { get; set; }

        /// <summary>
        /// Check if the input IP string is from Vietnam.
        /// </summary>
        /// <param name="ip">The IP to check.</param>
        /// <returns><c>true</c> if [IP is from VN]; otherwise, <c>false</c>.</returns>
        bool IsVnIp(string ip);

        #region METHODS TO RESOLVE NAMES FROM IDS

        string GetLeagueNameById(object leagueId);

        string GetTeamNameById(object teamId);

        string GetBetTypeNameById(object bettypeId);

        string GetBetTypeNameById(object bettypeId, object betID);

        string GetSportNameById(object sportId);

        string GetCasinoBetTeamNameById(object betteamId);

        string GetHorseTeamNameById(object homeId, object awayId);

        string GetRouletteBetItemNameById(object itemId);

        string GetCombinationMPOfferName(string key);

        #endregion
    }
}