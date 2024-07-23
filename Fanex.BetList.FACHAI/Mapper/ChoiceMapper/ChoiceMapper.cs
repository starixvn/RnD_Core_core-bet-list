namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            for (int betTypeId = FACHAIBetTypes.LowestBetTypeIdInRange + 1; betTypeId <= FACHAIBetTypes.HighestBetTypeIdInRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, FACHAIBetTypes.LowestBetTypeIdInRange);
            }
        }
    }
}