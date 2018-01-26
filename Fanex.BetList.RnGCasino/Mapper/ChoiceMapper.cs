namespace Fanex.BetList.Core.Mapper
{
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
            for (var betType = 2102; betType <= 2199; betType++)
            {
                MapIdToBaseId(betType, 2101);
            }
        }

        private void MappingForMiniGames()
        {
            for (var betType = 2201; betType <= 2299; betType++)
            {
                MapIdToBaseId(betType, 2101);
            }
        }

        private void MappingForMobileGames()
        {
            for (var betType = 2301; betType <= 2399; betType++)
            {
                MapIdToBaseId(betType, 2101);
            }
        }

        private void MappingForTabletGames()
        {
            for (var betType = 2901; betType <= 2999; betType++)
            {
                MapIdToBaseId(betType, 2101);
            }
        }

        private void MappingForJackpotContribution()
        {
            MapperData.Remove(2198);
            MapperData.Remove(2398);
            MapperData.Remove(2998);
            MapIdToBaseId(2398, 2198);
            MapIdToBaseId(2998, 2198);
        }

        private void MappingForProgressiveJackpot()
        {
            MapperData.Remove(2199);
            MapperData.Remove(2399);
            MapperData.Remove(2999);
            MapIdToBaseId(2399, 2199);
            MapIdToBaseId(2999, 2199);
        }

        private void MappingForJackpotGame()
        {
            MapperData.Remove(2179);
            MapperData.Remove(2320);
            MapperData.Remove(2920);
            MapIdToBaseId(2320, 2179);
            MapIdToBaseId(2920, 2179);
        }
    }
}