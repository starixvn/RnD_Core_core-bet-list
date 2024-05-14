namespace Fanex.BetList.Core.Builder.OddsBuilder
{
    using System.Collections.Generic;

    public class Odds2407 : Odds2401
    {
        public override List<string> HideOddsForBetChoices => new List<string>
        {
            "8011",
            "8111",
            "8012",
            "8112",
            "8013",
            "8113",
            "8021",
            "8022",
            "8023",
            "8121",
            "8122",
            "8123",
            "BB_BANKER1_DBL",
            "BB_BANKER2_DBL",
            "BB_BANKER3_DBL",
            "BB_BANKER1_SPR",
            "BB_BANKER2_SPR",
            "BB_BANKER3_SPR",
            "BB_PLAYER1_DBL",
            "BB_PLAYER2_DBL",
            "BB_PLAYER3_DBL",
            "BB_PLAYER1_SPR",
            "BB_PLAYER2_SPR",
            "BB_PLAYER3_SPR"
        };
    }
}