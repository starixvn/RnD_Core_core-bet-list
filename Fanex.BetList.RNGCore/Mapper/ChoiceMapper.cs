namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            for (var betType = RNGBetTypes.HotDogMin + 1; betType <= RNGBetTypes.HotDogMax; betType++)
            {
                MapIdToBaseId(betType, RNGBetTypes.HotDogMin);
            }

            for (var betType = RNGBetTypes.OdinGamingMin + 1; betType <= RNGBetTypes.OdinGamingMax; betType++)
            {
                MapIdToBaseId(betType, RNGBetTypes.OdinGamingMin);
            }
        }
    }
}