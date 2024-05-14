namespace Fanex.BetList.Core.Builder.OddsBuilder
{
    using System.Collections.Generic;

    public class Odds2408 : Odds2401
    {
        public override List<string> HideOddsForBetChoices => new List<string>
        {
            "PD_PLAYER_1",
            "PD_PLAYER_2",
            "PD_PLAYER_3",
            "PD_PLAYER_4",
            "PD_PLAYER_5",
            "PD_PLAYER_1_PAIR",
            "PD_PLAYER_2_PAIR",
            "PD_PLAYER_3_PAIR",
            "PD_PLAYER_4_PAIR",
            "PD_PLAYER_5_PAIR",
            "PD_2S_PLAYER_1",
            "PD_2S_PLAYER_2",
            "PD_2S_PLAYER_3",
            "PD_2S_PLAYER_4",
            "PD_2S_PLAYER_5",
            "PD_BANKER_1",
            "PD_BANKER_2",
            "PD_BANKER_3",
            "PD_BANKER_4",
            "PD_BANKER_5",
            "PD_BANKER_PAIR"
        };
    }
}