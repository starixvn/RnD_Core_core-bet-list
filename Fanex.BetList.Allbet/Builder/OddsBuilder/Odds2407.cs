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
            "8123"
        };
    }
}