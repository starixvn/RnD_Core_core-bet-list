namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
           StatusForHotDog();

           StatusForOdinGaming();
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
    }
}