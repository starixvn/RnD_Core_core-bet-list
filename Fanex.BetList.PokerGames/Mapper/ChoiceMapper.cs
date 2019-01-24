namespace Fanex.BetList.Core.Mapper
{
    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            Initialize();
        }

        private void Initialize()
        {
            // Map 'OMAHA' to 'Texas Holdem'.
            MapIdToBaseId(1403, 1402);

            // Map '3 Pic Switch (3 Pictures/Sakong)' to '8 Cards'.
            MapIdToBaseId(1404, 1401);

            // Map 'Shortdeck 6+' to 'Texas Holdem'.
            MapIdToBaseId(1405, 1402);

            MapVideoPokerGame();
        }

        private void MapVideoPokerGame()
        {
            for (int betType = 1902; betType <= 1999; betType++)
            {
                MapIdToBaseId(betType, 1901);
            }
        }
    }
}