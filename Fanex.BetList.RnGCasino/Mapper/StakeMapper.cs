namespace Fanex.BetList.Core.Mapper
{
    using System.Linq;
    using Constants;

    public class StakeMapper : BaseMapper
    {
        public StakeMapper()
        {
            foreach (var betType in RnGCasinoBetTypes.JackpotContributionBetTypes.Union(RnGCasinoBetTypes.ProgressiveJackpotBetTypes))
            {
                MapIdToBaseId(betType, RnGCasinoBetTypes.JackpotContribution);
            }
        }
    }
};