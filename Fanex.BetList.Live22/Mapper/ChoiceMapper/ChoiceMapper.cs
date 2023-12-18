namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            for (int betTypeId = Live22BetTypes.LowestBetTypeIdInRange + 1; betTypeId <= Live22BetTypes.HighestBetTypeIdInRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, Live22BetTypes.LowestBetTypeIdInRange);
            }
        }
    }
}