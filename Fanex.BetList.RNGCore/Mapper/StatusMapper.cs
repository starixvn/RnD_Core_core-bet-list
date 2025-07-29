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

            StatusForAIGaming();

            StatusForClotPlaying();

            StatusForGPILottery();
        }

        private void StatusForOdinGaming()
        {
            for (var betType = RNGBetTypes.OdinGamingMin + 1; betType <= RNGBetTypes.OdinGamingMax; betType++)
            {
                MapIdToBaseId(betType, RNGBetTypes.OdinGamingMin);
            }
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

        private void StatusForGPILottery()
        {
            for (var betType = RNGBetTypes.GPILotteryMin + 1; betType <= RNGBetTypes.GPILotteryMax; betType++)
            {
                MapIdToBaseId(betType, RNGBetTypes.AIGamingMin);
            }   
        }

        private void StatusForAIGaming()
        {
            for (var betType = RNGBetTypes.AIGamingMin + 1; betType <= RNGBetTypes.AIGamingMax; betType++)
            {
                MapIdToBaseId(betType, RNGBetTypes.AIGamingMin);
            }   
        }

        private void StatusForClotPlaying()
        {
            for (var betType = RNGBetTypes.ClotPlayingMin + 1; betType <= RNGBetTypes.ClotPlayingMax; betType++)
            {
                MapIdToBaseId(betType, RNGBetTypes.ClotPlayingMin);
            }
        }
    }
}