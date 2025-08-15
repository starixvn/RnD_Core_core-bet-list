
namespace Fanex.BetList.Core.Builder.OddsBuilder
{
    using System.Collections.Generic;

    public class Odds3404 : Odds3401
    {
        private readonly IList<string> BetChoicesHideOdds = new List<string>
        {
            "13047",
            "13048",
            "13049",
            "13050",
            "13051",
            "13052"
        };

        public override IList<string> GetBetChoicesHideOdds()
        {
            return BetChoicesHideOdds;
        }
    }
}
