namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            for (int betTypeId = Live22BetTypes.LowestBetTypeIdInRange + 1; betTypeId < Live22BetTypes.Live22PromotionBetType; betTypeId++)
            {
                MapIdToBaseId(betTypeId, Live22BetTypes.LowestBetTypeIdInRange);
            }
            
            for (int betTypeId = Live22BetTypes.Live22PromotionBetType + 1; betTypeId <= Live22BetTypes.HighestBetTypeIdInRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, Live22BetTypes.LowestBetTypeIdInRange);
            }
        }
    }
}