using Fanex.BetList.Core.Constants;

namespace Fanex.BetList.Core.Mapper
{
    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            for (int betTypeId = SealEntertainmentBetTypes.LowestBetTypeIdInRange + 1; betTypeId <= SealEntertainmentBetTypes.HighestBetTypeIdInRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, SealEntertainmentBetTypes.LowestBetTypeIdInRange);
            }
        }
    }
}
