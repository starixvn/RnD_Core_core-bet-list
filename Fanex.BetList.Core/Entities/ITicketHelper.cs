// ---------------------------------------------------------------------------------
// <copyright file="ITicketHelper.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------------
// <history>
//     <change who="Marc.Bui" date="2012.11.16">Create</change>
//     <change who="Marc.Bui" date="2012.12.19">Remove GetMixParlayDetail</change>
//     <change who="Marc.Bui" date="2012.12.26">Add awayId to GetHorseTeamNameById 
//                                              in case some teams use awayId to 
//                                              resolve sport name</change>
//     <change who="Marc.Bui" date="2013.03.11>Add ClientDetailSystemParlayFunction
//                                             for easy customization</change>
//     <change who="Marc.Bui" date="2013.03.19>Add ClientSystemParlaySubbetsDetailFunction
//                                             & ClientMixParlaySubbetsDetailFunction
//                                             for easy customization</change>
// </history>
// ---------------------------------------------------------------------------------

namespace Fanex.BetList.Core.Entities
{
    using System.Collections.Generic;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface ITicketHelper
    {
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

        /// <summary>
        /// Check if the input IP string is from Vietnam
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        bool IsVnIp(string ip);

        int Index { get; set; }

        string CurrentLanguage { get; set; }

        /// <summary>
        /// Determine if the current render show Odds values for each ticket (Ex: in MixParlay sub-tickets)
        /// </summary>
        bool IsShowOddsWithChoice { get; set; }

        /// <summary>
        /// Custom javascripts function to handle SystemParlay onlick event, rendering winloss summary.
        /// If not defined, the default will be used
        /// </summary>
        string ClientDetailSystemParlayFunction { get; set; }

        /// <summary>
        /// Custom javascripts function to handle SystemParlay onlick event, rendering sub-bets.
        /// If not defined, the default will be used
        /// </summary>
        string ClientSystemParlaySubbetsDetailFunction { get; set; }

        /// <summary>
        /// Custom javascripts function to handle MixParlay onlick event, rendering sub-bets.
        /// If not defined, the default will be used
        /// </summary>
        string ClientMixParlaySubbetsDetailFunction { get; set; }

        bool ShowBetType { get; set; }

        bool ShowMatch { get; set; }

        bool ShowLeague { get; set; }

        bool ShowSystemParlayDetail { get; set; }

        bool ShowSystemParlayPlayerComm { get; set; }

        /// <summary>
        /// Method to get *Parlay details
        /// </summary>
        GetParlayTicketsById GetParlayDetailById { get; set; }
    }

    public delegate string GetCachePropertyById(object id);

    public delegate IList<ITicket> GetParlayTicketsById(object id);
}