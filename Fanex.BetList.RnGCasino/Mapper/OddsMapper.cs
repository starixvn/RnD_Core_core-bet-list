namespace Fanex.BetList.Core.Mapper
{
    using System.Linq;
    using BetList.Core.Constants;

    public class OddsMapper : BaseMapper
    {
        public OddsMapper()
        {
            foreach (var betType in BetTypeRanges.JackpotGameBetTypes
                .Union(BetTypeRanges.JackpotContributionBetTypes)
                .Union(BetTypeRanges.ProgressiveJackpotBetTypes))
            {
                MapIdToBaseId(betType, BetTypeRanges.BaccaratProgressiveJPJackpotGame);
            }
        }
    }
};