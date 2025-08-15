
namespace Fanex.BetList.Core.Builder.OddsBuilder
{
    using System.Collections.Generic;

    public class Odds3409 : Odds3401
    {
        private readonly IList<string> BetChoicesHideOdds = new List<string>
        {
            "18026",
            "18027",
            "18028",
            "18029",
            "18030",
            "18031"
        };

        public override IList<string> GetBetChoicesHideOdds()
        {
            return BetChoicesHideOdds;
        }
    }
}
