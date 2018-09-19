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
            // Map 'BidMe' to 'Baccarat'.
            MapIdToBaseId(1705, 1701);
        }
    }
}