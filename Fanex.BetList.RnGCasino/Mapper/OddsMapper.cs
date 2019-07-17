namespace Fanex.BetList.Core.Mapper
{
    using System.Linq;
    using Constants;

    public class OddsMapper : BaseMapper
    {
        public OddsMapper()
        {
            foreach (var betType in RnGCasinoBetTypes.JackpotGameBetTypes
                .Union(RnGCasinoBetTypes.JackpotContributionBetTypes)
                .Union(RnGCasinoBetTypes.ProgressiveJackpotBetTypes))
            {
                MapIdToBaseId(betType, RnGCasinoBetTypes.BaccaratProgressiveJPJackpotGame);
            }
        }
    }
};