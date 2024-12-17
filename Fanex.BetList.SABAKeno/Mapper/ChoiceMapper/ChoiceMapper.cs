namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            for (int betTypeId = SABAKenoBetTypes.LowestBetTypeIdInRange + 1; betTypeId <= SABAKenoBetTypes.HighestBetTypeIdInRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, SABAKenoBetTypes.LowestBetTypeIdInRange);
            }
        }
    }
}