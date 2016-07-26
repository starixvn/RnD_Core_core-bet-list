namespace Fanex.BetList.Core.Mapper
{
    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            MappingForRNGGame();

            MappingForMiniGame();

            MappingForMobileGame();
        }

        private void MappingForRNGGame()
        {
            for (var betType = 2102; betType <= 2177; betType++)
            {
                MapIdToBaseId(betType, 2101);
            }
        }

        private void MappingForMiniGame()
        {
            for (var betType = 2201; betType <= 2206; betType++)
            {
                MapIdToBaseId(betType, 2101);
            }
        }

        private void MappingForMobileGame()
        {
            for (var betType = 2301; betType <= 2308; betType++)
            {
                MapIdToBaseId(betType, 2101);
            }
        }
    }
}