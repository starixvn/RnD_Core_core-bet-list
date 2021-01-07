namespace Fanex.BetList.Core.Builder.OddsBuilder
{
    using System.Collections.Generic;

    public class Odds2408 : Odds2401
    {
        public override List<string> HideOddsForBetChoices => new List<string>
        {
            "5001",
            "5002",
            "5003",
            "5004",
            "5005"
        };
    }
}