namespace Fanex.BetList.Core.Mapper
{
    public class OddsMapper : BaseMapper
    {
        public OddsMapper()
        {
            Initialize();
        }

        private void Initialize()
        {
            MapIdToBaseId(912, 911);
        }
    }
}