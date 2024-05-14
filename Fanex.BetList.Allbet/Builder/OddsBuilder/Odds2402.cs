namespace Fanex.BetList.Core.Builder.OddsBuilder
{
    using System.Collections.Generic;

    public class Odds2402 : Odds2401
    {
        public override List<string> HideOddsForBetChoices => new List<string>
        {
            "BAC_LUCKY_SIX",
            "BAC_BANKER_BINGO",
            "BAC_PLAYER_BINGO",
            "BAC_TIGER",
            "BAC_TIGER_PAIR",
            "BAC_BANKER_PCP",
            "BAC_PLAYER_PCP"
        };
    }
}