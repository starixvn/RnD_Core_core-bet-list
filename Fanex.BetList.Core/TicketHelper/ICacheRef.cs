namespace Fanex.BetList.Core.TicketHelper
{
    using System.Collections.Generic;

    public interface ICacheRef
    {
        string GetBetType(string betTypeId, string resourceId, long refNo);

        string GetDisplayedBetType(string betTypeId, string resourceId, long refNo);

        string GetParentBetTypeIdByBetTypeId(string betTypeId, string defaultParentBetTypeId);

        string GetSportName(string sportId);

        string GetTeamName(string teamId);

        string GetTeamName3rd(string team3rdId);

        string GetRouletteBetItem(string itemId);

        string GetCasinoBetItem(string betStatusId);

        string GetLeagueName(string leagueId);

        string GetLeagueName3rd(string league3rdId);

        string GetHorseTeamName(string codeId);

        string GetOddsTypeName(string oddsTypeId);

#pragma warning disable S3874 // "out" and "ref" parameters should not be used

        string GetKenoBetChoiceName(string kenoBetChoiceId, out string kenoBetTypeId);

#pragma warning restore S3874 // "out" and "ref" parameters should not be used

        string GetKenoBetTypeName(string kenoBetTypeId);

        string GetResourceData(string refId, string resourceId);

        IList<string> GetCombinationTypeCombo(string betTeam, string betCheck, int handicap1, int handicap2);

        string GetFastMarketsChoice(string betTypeId, string betTeam, int handicap1, int handicap2);

#pragma warning disable S3874 // "out" and "ref" parameters should not be used

        string GetLotteryBetChoiceName(string lotteryBetChoiceId, out string lotteryBetTypeId);

#pragma warning restore S3874 // "out" and "ref" parameters should not be used

        string GetLotteryBetTypeName(string lotteryBettypeId);

        string GetBetRadarMatchCodeFormat(string matchCode);
    }
}