namespace Fanex.BetList.Core.Mapper
{
    using System.Linq;

    public class OddsMapper : BaseMapper
    {
        public OddsMapper()
        {
            foreach (var betType in BetTypeData.BacarratGameBetTypes
                .Union(BetTypeData.JackpotGameBetTypes)
                .Union(BetTypeData.JackpotContributionBetTypes)
                .Union(BetTypeData.ProgressiveJackpotBetTypes))
            {
                MapIdToBaseId(betType, 2178);
            }
        }
    }
};