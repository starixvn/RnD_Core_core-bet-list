namespace Fanex.BetList.Core.Mapper
{
    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            MappingForRNGGames();
            MappingForMiniGames();
            MappingForMobileGames();
            MappingForTabletGames();
            MappingForProgressiveJackpot();
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

        private void MappingForProgressiveJackpot()
        {
            MapperData.Remove(2199);
            MapperData.Remove(2399);
            MapperData.Remove(2999);
            MapIdToBaseId(2399, 2199);
            MapIdToBaseId(2999, 2199);
        }
    }
}