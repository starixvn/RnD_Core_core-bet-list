namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            for (var betType = RNGBetTypes.GPILotteryMin + 1; betType <= RNGBetTypes.GPILotteryMax; betType++)
            {
                MapIdToBaseId(betType, RNGBetTypes.GPILotteryMin);
            }

            for (var betType = RNGBetTypes.HotDogMin + 1; betType <= RNGBetTypes.HotDogMax; betType++)
            {
                MapIdToBaseId(betType, RNGBetTypes.HotDogMin);
            }

            for (var betType = RNGBetTypes.OdinGamingMin + 1; betType <= RNGBetTypes.OdinGamingMax; betType++)
            {
                MapIdToBaseId(betType, RNGBetTypes.OdinGamingMin);
            }

            for (var betType = RNGBetTypes.RichGamingMin + 1; betType <= RNGBetTypes.RichGamingPromotion; betType++)
            {
                MapIdToBaseId(betType, RNGBetTypes.RichGamingMin);
            }

            for (var betType = RNGBetTypes.AIGamingMin + 1; betType <= RNGBetTypes.AIGamingMax; betType++)
            {
                MapIdToBaseId(betType, RNGBetTypes.AIGamingMin);
            }

            for (var betType = RNGBetTypes.ClotPlayingMin + 1; betType <= RNGBetTypes.ClotPlayingMax; betType++)
            {
                MapIdToBaseId(betType, RNGBetTypes.ClotPlayingMin);
            }

        }
    }
}