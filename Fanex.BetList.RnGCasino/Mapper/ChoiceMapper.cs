namespace Fanex.BetList.Core.Mapper
{
    using BetList.Core.Constants;

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
            for (var betType = BetTypeRanges.BattleWar; betType <= BetTypeRanges.ProgressiveJackpot; betType++)
            {
                MapIdToBaseId(betType, BetTypeRanges.Baccarat);
            }
        }

        private void MappingForMiniGames()
        {
            for (var betType = BetTypeRanges.MiniBaccarat; betType <= BetTypeRanges.MiniProgressiveJackpot; betType++)
            {
                MapIdToBaseId(betType, BetTypeRanges.Baccarat);
            }
        }

        private void MappingForMobileGames()
        {
            for (var betType = BetTypeRanges.MobileBaccarat; betType <= BetTypeRanges.MobileProgressiveJackpot; betType++)
            {
                MapIdToBaseId(betType, BetTypeRanges.Baccarat);
            }
        }

        private void MappingForTabletGames()
        {
            for (var betType = BetTypeRanges.TabletBaccarat; betType <= BetTypeRanges.TabletProgressiveJackpot; betType++)
            {
                MapIdToBaseId(betType, BetTypeRanges.Baccarat);
            }
        }

        private void MappingForJackpotContribution()
        {
            foreach (var betType in BetTypeRanges.JackpotContributionBetTypes)
            {
                MapperData.Remove(betType);

                MapIdToBaseId(betType, BetTypeRanges.JackpotContribution);
            }
        }

        private void MappingForProgressiveJackpot()
        {
            foreach (var betType in BetTypeRanges.ProgressiveJackpotBetTypes)
            {
                MapperData.Remove(betType);

                MapIdToBaseId(betType, BetTypeRanges.ProgressiveJackpot);
            }
        }

        private void MappingForJackpotGame()
        {
            foreach (var betType in BetTypeRanges.JackpotGameBetTypes)
            {
                MapperData.Remove(betType);

                MapIdToBaseId(betType, BetTypeRanges.BaccaratProgressiveJPJackpotGame);
            }
        }
    }
}