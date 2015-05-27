namespace Fanex.BetList.Core.Entities
{
    using System.Collections.Generic;

    public delegate string GetCachePropertyById(object id);

    public delegate IList<ITicket> GetParlayTicketsById(object id);

    /// <summary>
    /// Interface ITicketHelper.
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

        string GetBetTypeNameById(object bettypeId, object betId, object betCheck = null);

        string GetParentIdByBetTypeId(object betTypeId, object defaultParentId = null);

        string GetSportNameById(object sportId);

        string GetCasinoBetTeamNameById(object betteamId);

        string GetHorseTeamNameById(object homeId, object awayId);

        string GetRouletteBetItemNameById(object itemId);

        string GetCombinationMPOfferName(string key);

        string GetKenoBetChoiceName(string kenoBetChoiceID, out string kenoBettypeID);

        string GetKenoBetTypeName(string kenoBettypeID);

        string GetResourceData(string refId, string resourceId);

        #endregion METHODS TO RESOLVE NAMES FROM IDS
    }
}