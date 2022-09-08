namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            for (int betTypeId = MGRNGCasinoBettypes.LuckyTwinsWilds + 1; betTypeId <= MGRNGCasinoBettypes.HighestBetTypeIdInRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, MGRNGCasinoBettypes.LuckyTwinsWilds);
            }
        }
    }
}
