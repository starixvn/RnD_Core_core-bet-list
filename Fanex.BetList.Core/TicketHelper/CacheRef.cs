namespace Fanex.BetList.Core.TicketHelper
{
    using System.Threading;
    using RemotingRef;

    /// <summary>
    /// Class CacheRef.
    /// </summary>
    public class CacheRef
    {
        private CacheRef()
        {
        }

        public static string CurrentTWLanguage
        {
            get
            {
                switch (Thread.CurrentThread.CurrentUICulture.Name)
                {
                    case "en-US": return "en";
                    case "zh-TW": return "ch"; // Traditional
                    case "zh-CN": return "cs"; // Simplified
                    case "ja-JP": return "jp";
                    case "th-TH": return "th"; // Thai
                    case "ko-KR": return "ko"; // Korean
                    case "vi-VN": return "vn"; // Vietnamese
                    case "zh-Hans": return "zhcn"; // New Simplified Chinese
                    default: return "en";
                }
            }
        }

        public static string GetBettype(object bettypeId)
        {
            return GetBettype(bettypeId, null, null);
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
            string resourceId = string.Empty;
            object resource = betId ?? betCheck;

            // Remove first character of the resource.
            if (resource != null && resource.ToString().Length > 1)
            {
                resourceId = resource.ToString().Substring(1);
            }

            return RefData.GetBetTypeName(bettypeId.ToString(), CurrentTWLanguage, string.Empty, resourceId);
        }

        public static string GetParentIdByBetTypeId(object betTypeId, object defaultParentId)
        {
            return RefData.GetParentIdByBetTypeId(betTypeId.ToString(), defaultParentId.ToString());
        }

        public static string GetSportName(object sportId)
        {
            return RefData.GetSportName(sportId.ToString(), CurrentTWLanguage, string.Empty);
        }

        public static string GetTeamName(object teamId)
        {
            return RefData.GetTeamName(teamId.ToString(), CurrentTWLanguage, string.Empty);
        }

        public static string GetRouletteBetItem(object itemId)
        {
            return RefData.GetRouletteBetItem(itemId.ToString(), CurrentTWLanguage, string.Empty);
        }

        public static string GetCasinoBetItem(object betStatusId)
        {
            return RefData.GetCasinoBetItem(betStatusId.ToString(), CurrentTWLanguage, string.Empty);
        }

        public static string GetLeagueName(object leagueId)
        {
            return RefData.GetLeagueName(leagueId.ToString(), CurrentTWLanguage, string.Empty);
        }

        public static string GetHorseTeamName(object codeId)
        {
            return RefData.GetHorseTeamName(codeId.ToString(), CurrentTWLanguage);
        }

        public static string GetOddsTypeName(object oddsTypeId)
        {
            return RefData.GetOddsTypeName(oddsTypeId.ToString(), CurrentTWLanguage);
        }

        public static string GetKenoBetChoiceName(string kenoBetChoiceID, out string kenoBettypeID)
        {
            return RefData.GetKenoBetChoice(kenoBetChoiceID, CurrentTWLanguage, out kenoBettypeID);
        }

        public static string GetKenoBetTypeName(string kenoBettypeID)
        {
            return RefData.GetKenoBettype(kenoBettypeID, CurrentTWLanguage);
        }

        public static string GetResourceData(string refId, string resourceId)
        {
            return RefData.GetResourceDataByRefIdAndResourceId(refId, resourceId, CurrentTWLanguage, string.Empty);
        }
    }
}