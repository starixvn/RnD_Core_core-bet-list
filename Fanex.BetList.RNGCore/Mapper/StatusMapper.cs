namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            StatusForHotDog();

            StatusForOdinGaming();

            StatusForRichGaming();
        }

        private void StatusForOdinGaming()
        {
            for (var betType = RNGBetTypes.OdinGamingMin + 1; betType < RNGBetTypes.OdinGamingMax; betType++)
            {
                MapIdToBaseId(betType, RNGBetTypes.OdinGamingMin);
            }

            MapIdToBaseId(RNGBetTypes.OdinGamingMax, BetTypes.SabaLuckySpin);
        }

        private void StatusForHotDog()
        {
            for (var betType = RNGBetTypes.HotDogMin + 1; betType < RNGBetTypes.HotDogMax; betType++)
            {
                MapIdToBaseId(betType, RNGBetTypes.HotDogMin);
            }

            MapIdToBaseId(RNGBetTypes.HotDogMax, BetTypes.SabaLuckySpin);
        }

        private void StatusForRichGaming()
        {
            for (var betType = RNGBetTypes.RichGamingMin + 1; betType < RNGBetTypes.RichGamingPromotion; betType++)
            {
                MapIdToBaseId(betType, RNGBetTypes.RichGamingMin);
            }
            MapIdToBaseId(RNGBetTypes.RichGamingPromotion, BetTypes.SabaLuckySpin);
        }
    }
}