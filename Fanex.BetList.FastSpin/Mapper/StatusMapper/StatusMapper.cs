namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            for (int betTypeId = FastSpinBetTypes.LowestBetTypeIdInRange + 1; betTypeId < FastSpinBetTypes.FastSpinPromotionBetType; betTypeId++)
            {
                MapIdToBaseId(betTypeId, FastSpinBetTypes.LowestBetTypeIdInRange);
            }
            
            for (int betTypeId = FastSpinBetTypes.FastSpinPromotionBetType + 1; betTypeId <= FastSpinBetTypes.HighestBetTypeIdInRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, FastSpinBetTypes.LowestBetTypeIdInRange);
            }
        }
    }
}