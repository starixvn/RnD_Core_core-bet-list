namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;


    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            for (int betTypeId = JiliBettypes.LowestBetTypeIdInRange + 1; betTypeId <= JiliBettypes.HighestBetTypeIdInRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, JiliBettypes.LowestBetTypeIdInRange);                
            }
        }
    }
}
