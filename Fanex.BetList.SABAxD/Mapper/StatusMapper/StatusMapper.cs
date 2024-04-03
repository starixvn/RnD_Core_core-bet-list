namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            for (int betTypeId = SABAxDBetTypes.LowestBetTypeIdInRange + 1; betTypeId < SABAxDBetTypes.HighestBetTypeIdInRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, SABAxDBetTypes.LowestBetTypeIdInRange);
            }            
        }
    }
}