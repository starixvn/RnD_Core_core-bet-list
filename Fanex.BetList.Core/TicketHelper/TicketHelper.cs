namespace Fanex.BetList.Core.TicketHelper
{
    using System.Collections.Generic;
    using Entities;
    using Sunplus.Core.Common.IPs;

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

        /// <summary>
        /// Gets or sets Method to get *Parlay details.
        /// </summary>
        /// <value>Function to get parlay detail by Id.</value>
        public GetParlayTicketsById GetParlayDetailById { get; set; }

        public string GetBetTypeNameById(object bettypeId)
        {
            return CacheRef.GetBettype(bettypeId);
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

        public string GetParentIdByBetTypeId(object betTypeId, object defaultParentId = null)
        {
            defaultParentId = defaultParentId ?? string.Empty;

            return CacheRef.GetParentIdByBetTypeId(betTypeId, defaultParentId);
        }

        public string GetRouletteBetItemNameById(object itemId)
        {
            return CacheRef.GetRouletteBetItem(itemId);
        }

        public bool IsVnIp(string ip)
        {
            return IpUtils.IsVnIp(ip);
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
    }
}