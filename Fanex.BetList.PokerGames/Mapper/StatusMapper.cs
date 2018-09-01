namespace Fanex.BetList.Core.Mapper
{
    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            Initialize();
        }

        private void Initialize()
        {
            // Map '6 Cards (MASUK)' to '8 Cards'.
            MapIdToBaseId(1402, 1401);

            // Map '4 Treasures' to '8 Cards'.
            MapIdToBaseId(1403, 1401);

            // Map '3 Pic Switch (3 Pictures/Sakong)' to '8 Cards'.
            MapIdToBaseId(1404, 1401);

            MapIdToBaseId(1405, 1401);
        }
    }
}