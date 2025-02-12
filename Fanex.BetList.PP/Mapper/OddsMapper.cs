namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class OddsMapper : BaseMapper
    {
        public OddsMapper()
        {
            for (int betTypeId = PragmaticPlayBetTypes.Dragons888 + 1; betTypeId <= PragmaticPlayBetTypes.PromotionPrize; betTypeId++)
            {
                MapIdToBaseId(betTypeId, PragmaticPlayBetTypes.Dragons888);
            }

            for (int betTypeId = PragmaticPlayBetTypes.CandyVillage; betTypeId <= PragmaticPlayBetTypes.HighestBetTypeIdInRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, PragmaticPlayBetTypes.Dragons888);
            }
            
            for (int betTypeId = PragmaticPlayBetTypes.ExtendLowestBetTypeIdInRange; betTypeId <= PragmaticPlayBetTypes.ExtendHighestBetTypeIdInRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, PragmaticPlayBetTypes.Dragons888);
            }
        }
    }
}