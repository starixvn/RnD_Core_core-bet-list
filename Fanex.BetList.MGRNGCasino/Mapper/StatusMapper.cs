namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;


    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            for (int betTypeId = MGRNGCasinoBettypes.LuckyTwinsWilds + 1; betTypeId <= MGRNGCasinoBettypes.HighestBetTypeIdInRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, MGRNGCasinoBettypes.LuckyTwinsWilds);
            }
        }
    }
}
