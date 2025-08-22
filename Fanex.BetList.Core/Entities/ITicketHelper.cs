namespace Fanex.BetList.Core.Entities
{
    using System;
    using System.Collections.Generic;

    public delegate string GetCachePropertyById(object id);

    public delegate IList<ITicket> GetParlayTicketsById(object id);

    /// <summary>
    /// Represents a bet choice with its associated type information
    /// </summary>
    public class BetChoiceInfo
    {
        public string ChoiceName { get; set; }
        public string BetTypeId { get; set; }

        public BetChoiceInfo(string choiceName, string betTypeId)
        {
            ChoiceName = choiceName;
            BetTypeId = betTypeId;
        }
    }

    /// <summary>
    /// Interface ITicketHelper.
    /// </summary>
    public interface ITicketHelper
    {
        int Index { get; set; }

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
        /// Show Cash Out History in Status Column
        /// </summary>
        bool HideStatusCashOutHistory { get; set; }

        /// <summary>
        /// Check if the input IP string is from Vietnam.
        /// </summary>
        /// <returns><c>true</c> if [IP is from VN]; otherwise, <c>false</c>.</returns>
        Func<string, bool> IsVnIp { get; set; }

        bool HideSabaText { get; set; }
        
        #region METHODS TO RESOLVE NAMES FROM IDS

        string GetLeagueNameById(object leagueId);

        string GetLeagueNameById(object leagueId, object leagueGroupId);

        string GetTeamNameById(object teamId);

        string GetBetTypeNameById(object betTypeId);

        string GetBetTypeNameById(object betTypeId, object refNo);

        string GetBetTypeNameById(object betTypeId, object betId, object betCheck = null);

        string GetBetTypeNameById(object betTypeId, object refNo, object betId, object betCheck = null);

        string GetParentIdByBetTypeId(object betTypeId, object defaultParentId = null);

        string GetSportNameById(object sportId);

        string GetCasinoBetTeamNameById(object betTeamId);

        string GetHorseTeamNameById(object teamId);

        string GetRouletteBetItemNameById(object itemId);

        string GetOddsTypeNameById(object oddsTypeId);

        string GetCombinationMPOfferName(string key);

        IList<string> GetCombinationMPOfferNameList(string betTeam, string betCheck, decimal handicap1, decimal handicap2);

        /// <summary>
        /// Gets keno bet choice information including name and bet type ID
        /// </summary>
        /// <param name="kenoBetChoiceId">The keno bet choice ID</param>
        /// <returns>BetChoiceInfo containing the choice name and bet type ID</returns>
        BetChoiceInfo GetKenoBetChoiceInfo(string kenoBetChoiceId);       

        string GetKenoBetTypeName(string kenoBetTypeId);

        string GetResourceData(string refId, string resourceId);

        string GetFastMarketSituation(string betType, string betTeam, decimal handicap1, decimal handicap2);

        /// <summary>
        /// Gets lottery bet choice information including name and bet type ID
        /// </summary>
        /// <param name="lotteryBetChoiceId">The lottery bet choice ID</param>
        /// <returns>BetChoiceInfo containing the choice name and bet type ID</returns>
        BetChoiceInfo GetLotteryBetChoiceInfo(string lotteryBetChoiceId);
      
        string GetLotteryBetTypeName(string lotteryBetTypeId);

        string GetTeamName3rd(string team3rdId);

        string GetLeagueName3rd(string league3rdId);

        string GetBetRadarMatchCodeFormat(string matchCode);

        /// <summary>
        /// Gets table games bet choice information including name and bet type ID
        /// </summary>
        /// <param name="tableGamesBetChoiceId">The table games bet choice ID</param>
        /// <returns>BetChoiceInfo containing the choice name and bet type ID</returns>
        BetChoiceInfo GetTableGamesBetChoiceInfo(string tableGamesBetChoiceId);

        string GetTableGamesBetTypeName(string tableGamesBetTypeId);

        string GetVirtualSportMatchCodeFormat(string matchCode, string refId);

        #endregion METHODS TO RESOLVE NAMES FROM IDS
    }
}