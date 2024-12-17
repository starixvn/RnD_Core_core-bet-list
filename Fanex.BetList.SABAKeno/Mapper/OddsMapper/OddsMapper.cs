namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class OddsMapper : BaseMapper
    {
        public OddsMapper()
        {
            for (int betTypeId = SABAKenoBetTypes.LowestBetTypeIdInRange + 1; betTypeId <= SABAKenoBetTypes.HighestBetTypeIdInRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, SABAKenoBetTypes.LowestBetTypeIdInRange);
            }            
        }
    }
}