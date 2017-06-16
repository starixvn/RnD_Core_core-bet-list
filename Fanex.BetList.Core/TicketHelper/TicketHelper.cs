namespace Fanex.BetList.Core.TicketHelper
{
    using Entities;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Utilities for resolving Names from IDs.
    /// </summary>
    public class TicketHelper : ITicketHelper
    {
        public TicketHelper()
        {
            ShowBetType = true;
            ShowMatch = true;
            ShowLeague = true;
            ShowSystemParlayDetail = true;
            ShowSystemParlayPlayerComm = false;
        }

        public int Index { get; set; }

        public string CurrentLanguage { get; set; }

        public bool IsShowOddsWithChoice { get; set; }

        public string ClientDetailSystemParlayFunction { get; set; }

        public string ClientSystemParlaySubbetsDetailFunction { get; set; }

        public string ClientMixParlaySubbetsDetailFunction { get; set; }

        public bool ShowBetType { get; set; }

        public bool ShowMatch { get; set; }

        public bool ShowLeague { get; set; }

        public bool ShowSystemParlayDetail { get; set; }

        public bool ShowSystemParlayPlayerComm { get; set; }

        public Func<string, bool> IsVnIp { get; set; }

        /// <summary>
        /// Gets or sets Method to get *Parlay details.
        /// </summary>
        /// <value>Function to get parlay detail by Id.</value>
        public GetParlayTicketsById GetParlayDetailById { get; set; }

        /// <summary>
        /// Show Cash Out History in Status Column
        /// </summary>
        public bool HideStatusCashOutHistory { get; set; }

        public string GetBetTypeNameById(object bettypeId)
        {
            return CacheRef.GetBettype(bettypeId);
        }

        public string GetBetTypeNameById(object bettypeId, object refNo)
        {
            if (IsShowOddsWithChoice)
            {
                return CacheRef.GetBettype(bettypeId);
            }

            return CacheRef.GetBettype(bettypeId, refNo);
        }

        public string GetCasinoBetTeamNameById(object betteamId)
        {
            return CacheRef.GetCasinoBetItem(betteamId);
        }

        public string GetLeagueNameById(object leagueId)
        {
            return CacheRef.GetLeagueName(leagueId);
        }

        public string GetSportNameById(object sportId)
        {
            return CacheRef.GetSportName(sportId);
        }

        public string GetTeamNameById(object teamId)
        {
            return CacheRef.GetTeamName(teamId);
        }

        public virtual string GetHorseTeamNameById(object homeId, object awayId)
        {
            return CacheRef.GetHorseTeamName(homeId);
        }

        public string GetBetTypeNameById(object bettypeId, object betId, object betCheck = null)
        {
            return CacheRef.GetBettype(bettypeId, betId, betCheck);
        }

        public string GetBetTypeNameById(object bettypeId, object refNo, object betId, object betCheck = null)
        {
            if (IsShowOddsWithChoice)
            {
                return CacheRef.GetBettype(bettypeId, betId, betCheck);
            }

            return CacheRef.GetBettype(bettypeId, betId, betCheck, refNo);
        }

        public string GetParentIdByBetTypeId(object betTypeId, object defaultParentId = null)
        {
            var parentId = defaultParentId ?? string.Empty;

            return CacheRef.GetParentIdByBetTypeId(betTypeId, parentId);
        }

        public string GetRouletteBetItemNameById(object itemId)
        {
            return CacheRef.GetRouletteBetItem(itemId);
        }

        /// <summary>
        /// Gets the name of the offered combination mix-parlay.
        /// </summary>
        /// <param name="key">The key of MP Combination.</param>
        /// <returns>MP combination offered name.</returns>
        public string GetCombinationMPOfferName(string key)
        {
            Dictionary<string, string> combinationOfferName = new Dictionary<string, string>
            {
                {
                    "110",
                    "Trixie"
                },
                {
                    "1110",
                    "Yankee"
                },
                {
                    "11110",
                    "Canadian"
                },
                {
                    "111110",
                    "Heinz"
                },
                {
                    "1111110",
                    "Super Heinz"
                },
                {
                    "11111110",
                    "Goliath"
                }
            };

            return combinationOfferName.ContainsKey(key) ? combinationOfferName[key] : string.Empty;
        }

        public IList<string> GetCombinationMPOfferNameList(
                string betTeam,
                string betCheck,
                decimal handicap1,
                decimal handicap2)
        {
            return CacheRef.GetCombinationTypeCombo(betTeam, betCheck, Convert.ToInt32(handicap1), Convert.ToInt32(handicap2));
        }

        public string GetKenoBetChoiceName(string kenoBetChoiceID, out string kenoBettypeID)
        {
            return CacheRef.GetKenoBetChoiceName(kenoBetChoiceID, out kenoBettypeID);
        }

        public string GetKenoBetTypeName(string kenoBettypeID)
        {
            return CacheRef.GetKenoBetTypeName(kenoBettypeID);
        }

        public string GetResourceData(string refId, string resourceId)
        {
            return CacheRef.GetResourceData(refId, resourceId);
        }

        public string GetFastMarketSituation(string betType, string betTeam, decimal handicap1, decimal handicap2)
        {
            return CacheRef.GetFastMarketChoice(betType, betTeam, Convert.ToInt32(handicap1), Convert.ToInt32(handicap2));
        }

        public string GetLotteryBetChoiceName(string lotteryBetChoiceId, out string lotteryBettypeId)
        {
            return CacheRef.GetLotteryBetChoiceName(lotteryBetChoiceId, out lotteryBettypeId);
        }

        public string GetLotteryBetTypeName(string lotteryBettypeId)
        {
            return CacheRef.GetLotteryBetTypeName(lotteryBettypeId);
        }
    }
}