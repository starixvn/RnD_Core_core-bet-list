namespace Fanex.BetList.Core.Mapper
{
    using System.Linq;
    using BetList.Core.Constants;

    public class StakeMapper : BaseMapper
    {
        public StakeMapper()
        {
            foreach (var betType in BetTypeRanges.JackpotContributionBetTypes.Union(BetTypeRanges.ProgressiveJackpotBetTypes))
            {
                MapIdToBaseId(betType, BetTypeRanges.JackpotContribution);
            }
        }
    }
};