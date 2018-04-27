namespace Fanex.BetList.Core.TicketHelper
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using Mapper;
    using RemotingRef;

    /// <summary>
    /// Class CacheRef.
    /// </summary>
    public static class CacheRef
    {
        private static readonly IRemotingRefService _remotingRefService = new RemotingRefService();
        private static readonly string CurrentLanguage = Thread.CurrentThread.CurrentUICulture.Name;
        private const string DefaultValue = "";

        public static string GetBettype(object bettypeId)
        {
            return GetBettype(bettypeId, null, null);
        }

        public static string GetBettype(object bettypeId, object refNo)
        {
            return GetBettype(bettypeId, null, null, refNo);
        }

        /// <summary>
        /// Gets bet type name.
        /// </summary>
        /// <param name="bettypeId">The ID of the bet type which needs to get the name.</param>
        /// <param name="betId">In the case of single ticket, the resource ID is the bet ID.</param>
        /// <param name="betCheck">In the case of sub-ticket of a parlay ticket, the resource ID is the BetCheck.</param>
        /// <returns>The bet name of the passed bet type ID with associate resource.</returns>
        public static string GetBettype(object bettypeId, object betId, object betCheck)
        {
            return GetBettype(bettypeId, betId, betCheck, 0);
        }

        /// <summary>
        /// Gets bet type name.
        /// </summary>
        /// <param name="bettypeId">The ID of the bet type which needs to get the name.</param>
        /// <param name="betId">In the case of single ticket, the resource ID is the bet ID.</param>
        /// <param name="betCheck">In the case of sub-ticket of a parlay ticket, the resource ID is the BetCheck.</param>
        /// <returns>The bet name of the passed bet type ID with associate resource.</returns>
        public static string GetBettype(object bettypeId, object betId, object betCheck, object refNo)
        {
            var resourceId = string.Empty;
            var resource = betId ?? betCheck;
            var betTypeId = bettypeId;
            var refNoValue = refNo;

            // Remove first character of the resource.
            if (resource != null && resource.ToString().Length > 1)
            {
                resourceId = resource.ToString().Substring(1);
            }

            if (MapperCollector.BetTypeHasParent(Convert.ToInt32(betTypeId)))
            {
                betTypeId = GetParentIdByBetTypeId(betTypeId, string.Empty);
            }

            if (string.IsNullOrEmpty(refNoValue?.ToString()))
            {
                refNoValue = 0;
            }

            if (MapperCollector.BetTypeHasParent(Convert.ToInt32(betTypeId)))
            {
                betTypeId = GetParentIdByBetTypeId(betTypeId, string.Empty);
            }

            return _remotingRefService.GetBetTypeName(betTypeId.ToString(), CurrentLanguage, DefaultValue, resourceId, Convert.ToInt64(refNoValue));
        }

        public static string GetParentIdByBetTypeId(object betTypeId, object defaultParentId)
        {
            return _remotingRefService.GetParentIdByBetTypeId(betTypeId.ToString(), defaultParentId.ToString());
        }

        public static string GetSportName(object sportId)
        {
            return _remotingRefService.GetSportName(sportId.ToString(), CurrentLanguage, DefaultValue);
        }

        public static string GetTeamName(object teamId)
        {
            return _remotingRefService.GetTeamName(teamId.ToString(), CurrentLanguage, DefaultValue);
        }

        public static string GetRouletteBetItem(object itemId)
        {
            return _remotingRefService.GetRouletteBetItem(itemId.ToString(), CurrentLanguage, DefaultValue);
        }

        public static string GetCasinoBetItem(object betStatusId)
        {
            return _remotingRefService.GetCasinoBetItem(betStatusId.ToString(), CurrentLanguage, DefaultValue);
        }

        public static string GetLeagueName(object leagueId)
        {
            return _remotingRefService.GetLeagueName(leagueId.ToString(), CurrentLanguage, DefaultValue);
        }

        public static string GetHorseTeamName(object codeId)
        {
            var horseTeamName = string.Empty;

            try
            {
                horseTeamName = _remotingRefService.GetHorseTeamName(codeId.ToString(), CurrentLanguage, DefaultValue);
            }
            catch
            {
                return horseTeamName;
            }

            return horseTeamName;
        }

        public static string GetOddsTypeName(object oddsTypeId)
        {
            return _remotingRefService.GetOddsTypeName(oddsTypeId.ToString(), CurrentLanguage);
        }

        public static string GetKenoBetChoiceName(string kenoBetChoiceID, out string kenoBettypeID)
        {
            return _remotingRefService.GetKenoBetChoice(kenoBetChoiceID, CurrentLanguage, DefaultValue, out kenoBettypeID);
        }

        public static string GetKenoBetTypeName(string kenoBettypeID)
        {
            return _remotingRefService.GetKenoBetType(kenoBettypeID, CurrentLanguage, DefaultValue);
        }

        public static string GetResourceData(string refId, string resourceId)
        {
            return _remotingRefService.GetResourceDataByRefIdAndResourceId(refId, resourceId, CurrentLanguage, DefaultValue);
        }

        public static IList<string> GetCombinationTypeCombo(
                string betTeam,
                string betCheck,
                int handicap1,
                int handicap2)
        {
            return _remotingRefService.GetCombinationTypeCombo(betTeam, betCheck, handicap1, handicap2, CurrentLanguage);
        }

        public static string GetFastMarketChoice(string betType, string betTeam, int handicap1, int handicap2)
        {
            return _remotingRefService.GetFastMarketsChoice(betType, betTeam, handicap1, handicap2, CurrentLanguage);
        }

        public static string GetLotteryBetChoiceName(string kenoBetChoiceID, out string kenoBettypeID)
        {
            return _remotingRefService.GetLotteryBetChoice(kenoBetChoiceID, CurrentLanguage, DefaultValue, out kenoBettypeID);
        }

        public static string GetLotteryBetTypeName(string kenoBettypeID)
        {
            return _remotingRefService.GetLotteryBetType(kenoBettypeID, CurrentLanguage, DefaultValue);
        }

        public static string GetTeamName3rd(string team3rdId)
        {
            return _remotingRefService.GetTeamName3rd(team3rdId, CurrentLanguage, DefaultValue);
        }

        public static string GetLeagueName3rd(string league3rdId)
        {
            return _remotingRefService.GetLeagueName3rd(league3rdId, CurrentLanguage, DefaultValue);
        }

        public static string GetBetRadarMatchCodeFormat(string matchCode)
        {
            return string.Empty;
        }
    }
}