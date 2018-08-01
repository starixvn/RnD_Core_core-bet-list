namespace Fanex.BetList.Core.TicketHelper
{
    using System;
    using System.Collections.Generic;
    using Entities;
    using Mapper;

    public class TicketHelper : ITicketHelper
    {
        private readonly ICacheRef _cacheRef;

        public TicketHelper(ICacheRef cacheRef = null)
        {
            _cacheRef = cacheRef ?? BetListManager.RegisteredCacheRef;

            ShowBetType = true;
            ShowMatch = true;
            ShowLeague = true;
            ShowSystemParlayDetail = true;
            ShowSystemParlayPlayerComm = false;
        }

        public int Index { get; set; }

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

#pragma warning disable S3254 // Default parameter values should not be passed as arguments

        public string GetBetTypeNameById(object betTypeId)
            => GetBetTypeNameById(betTypeId, null, null, null);

        public string GetBetTypeNameById(object betTypeId, object refNo)
            => GetBetTypeNameById(betTypeId, refNo, null, null);

#pragma warning restore S3254 // Default parameter values should not be passed as arguments

        public string GetBetTypeNameById(object betTypeId, object betId, object betCheck = null)
            => GetBetTypeNameById(betTypeId, 0, betId, betCheck);

        public string GetBetTypeNameById(object betTypeId, object refNo, object betId, object betCheck = null)
        {
            string resource = (betId ?? betCheck)?.ToString();
            string resourceId = string.Empty;
            long actualRefNo = 0;
            string actualBetTypeId = MapperCollector.BetTypeHasParent(Convert.ToInt32(betTypeId))
                ? GetParentIdByBetTypeId(betTypeId, betTypeId)
                : betTypeId.ToString();

            if (!IsShowOddsWithChoice)
            {
                long.TryParse(refNo?.ToString(), out actualRefNo);
            }

            // Remove first character of the resource.
            if (resource != null && resource.Length > 1)
            {
                resourceId = resource.Substring(1);
            }

            return _cacheRef.GetBetType(actualBetTypeId, resourceId, actualRefNo);
        }

        public string GetCasinoBetTeamNameById(object betTeamId)
            => _cacheRef.GetCasinoBetItem(betTeamId.ToString());

        public string GetLeagueNameById(object leagueId)
            => _cacheRef.GetLeagueName(leagueId.ToString());

        public string GetSportNameById(object sportId)
            => _cacheRef.GetSportName(sportId.ToString());

        public string GetTeamNameById(object teamId)
            => _cacheRef.GetTeamName(teamId.ToString());

        public string GetHorseTeamNameById(object teamId)
            => _cacheRef.GetHorseTeamName(teamId.ToString());

        public string GetParentIdByBetTypeId(object betTypeId, object defaultParentId = null)
            => _cacheRef.GetParentBetTypeIdByBetTypeId(betTypeId.ToString(), (defaultParentId ?? betTypeId).ToString());

        public string GetRouletteBetItemNameById(object itemId)
            => _cacheRef.GetRouletteBetItem(itemId.ToString());

        public string GetOddsTypeNameById(object oddsTypeId)
            => _cacheRef.GetOddsTypeName(oddsTypeId.ToString());

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
            => _cacheRef.GetCombinationTypeCombo(betTeam, betCheck, Convert.ToInt32(handicap1), Convert.ToInt32(handicap2));

        public string GetKenoBetChoiceName(string kenoBetChoiceId, out string kenoBetTypeId)
            => _cacheRef.GetKenoBetChoiceName(kenoBetChoiceId, out kenoBetTypeId);

        public string GetKenoBetTypeName(string kenoBetTypeId)
            => _cacheRef.GetKenoBetTypeName(kenoBetTypeId);

        public string GetResourceData(string refId, string resourceId)
            => _cacheRef.GetResourceData(refId, resourceId);

        public string GetFastMarketSituation(string betType, string betTeam, decimal handicap1, decimal handicap2)
            => _cacheRef.GetFastMarketsChoice(betType, betTeam, Convert.ToInt32(handicap1), Convert.ToInt32(handicap2));

        public string GetLotteryBetChoiceName(string lotteryBetChoiceId, out string lotteryBetTypeId)
            => _cacheRef.GetLotteryBetChoiceName(lotteryBetChoiceId, out lotteryBetTypeId);

        public string GetLotteryBetTypeName(string lotteryBetTypeId)
            => _cacheRef.GetLotteryBetTypeName(lotteryBetTypeId);

        public string GetTeamName3rd(string team3rdId)
            => _cacheRef.GetTeamName3rd(team3rdId);

        public string GetLeagueName3rd(string league3rdId)
            => _cacheRef.GetLeagueName3rd(league3rdId);

        public string GetBetRadarMatchCodeFormat(string matchCode)
            => _cacheRef.GetBetRadarMatchCodeFormat(matchCode);
    }
}