namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            MappingForRNGGames();
            MappingForMiniGames();
            MappingForMobileGames();
            MappingForTabletGames();

            MappingForJackpotContribution();
            MappingForProgressiveJackpot();
            MappingForJackpotGame();
        }

        private void MappingForRNGGames()
        {
            for (var betType = RnGCasinoBetTypes.BattleWar; betType <= RnGCasinoBetTypes.ProgressiveJackpot; betType++)
            {
                MapIdToBaseId(betType, RnGCasinoBetTypes.Baccarat);
            }
        }

        private void MappingForMiniGames()
        {
            for (var betType = RnGCasinoBetTypes.MiniBaccarat; betType <= RnGCasinoBetTypes.MiniProgressiveJackpot; betType++)
            {
                MapIdToBaseId(betType, RnGCasinoBetTypes.Baccarat);
            }
        }

        private void MappingForMobileGames()
        {
            for (var betType = RnGCasinoBetTypes.MobileBaccarat; betType <= RnGCasinoBetTypes.MobileProgressiveJackpot; betType++)
            {
                MapIdToBaseId(betType, RnGCasinoBetTypes.Baccarat);
            }
        }

        private void MappingForTabletGames()
        {
            for (var betType = RnGCasinoBetTypes.TabletBaccarat; betType <= RnGCasinoBetTypes.TabletProgressiveJackpot; betType++)
            {
                MapIdToBaseId(betType, RnGCasinoBetTypes.Baccarat);
            }
        }

        private void MappingForJackpotContribution()
        {
            foreach (var betType in RnGCasinoBetTypes.JackpotContributionBetTypes)
            {
                MapperData.Remove(betType);

                MapIdToBaseId(betType, RnGCasinoBetTypes.JackpotContribution);
            }
        }

        private void MappingForProgressiveJackpot()
        {
            foreach (var betType in RnGCasinoBetTypes.ProgressiveJackpotBetTypes)
            {
                MapperData.Remove(betType);

                MapIdToBaseId(betType, RnGCasinoBetTypes.ProgressiveJackpot);
            }
        }

        private void MappingForJackpotGame()
        {
            foreach (var betType in RnGCasinoBetTypes.JackpotGameBetTypes)
            {
                MapperData.Remove(betType);

                MapIdToBaseId(betType, RnGCasinoBetTypes.BaccaratProgressiveJPJackpotGame);
            }
        }
    }
}