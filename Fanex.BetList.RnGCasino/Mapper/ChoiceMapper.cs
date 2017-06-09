namespace Fanex.BetList.Core.Mapper
{
    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            MappingForRnGGame();

            MappingForMiniGame();

            MappingForMobileGame();
        }

        private void MappingForRnGGame()
        {
            for (var betType = 2102; betType <= 2187; betType++)
            {
                MapIdToBaseId(betType, 2101);
            }
        }

        private void MappingForMiniGame()
        {
            for (var betType = 2201; betType <= 2216; betType++)
            {
                MapIdToBaseId(betType, 2101);
            }
        }

        private void MappingForMobileGame()
        {
            for (var betType = 2301; betType <= 2318; betType++)
            {
                MapIdToBaseId(betType, 2101);
            }
        }
    }
}