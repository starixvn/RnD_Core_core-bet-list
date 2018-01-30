namespace Fanex.BetList.Core.Mapper
{
    using System.Linq;

    public class StakeMapper : BaseMapper
    {
        public StakeMapper()
        {
            foreach (var betType in BetTypeData.JackpotContributionBetTypes.Union(BetTypeData.ProgressiveJackpotBetTypes))
            {
                MapIdToBaseId(betType, 2198);
            }
        }
    }
};