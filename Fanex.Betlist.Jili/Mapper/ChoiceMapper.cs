namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            for (int betTypeId = JiliBettypes.LowestBetTypeIdInRange + 1; betTypeId <= JiliBettypes.HighestBetTypeIdInRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, JiliBettypes.LowestBetTypeIdInRange);
            }
        }
    }
}
