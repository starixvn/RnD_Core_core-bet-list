namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;


    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            for (int betTypeId = MGRNGCasinoBettypes.LuckyTwinsWilds + 1; betTypeId <= MGRNGCasinoBettypes.HighestBetTypeIdInRange; betTypeId++)
            {
                if (betTypeId != MGRNGCasinoBettypes.TouramentPrize)
                {
                    MapIdToBaseId(betTypeId, MGRNGCasinoBettypes.LuckyTwinsWilds);
                }                    
            }
        }
    }
}
