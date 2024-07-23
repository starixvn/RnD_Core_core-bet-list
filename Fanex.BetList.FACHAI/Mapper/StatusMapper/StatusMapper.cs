namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            for (int betTypeId = FACHAIBetTypes.LowestBetTypeIdInRange + 1; betTypeId < FACHAIBetTypes.HighestBetTypeIdInRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, FACHAIBetTypes.LowestBetTypeIdInRange);
            }
        }
    }
}