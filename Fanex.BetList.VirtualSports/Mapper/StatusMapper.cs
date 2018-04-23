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
            MapIdToBaseId(1232, 1231);
            MapIdToBaseId(2799, 9);
            MapIdToBaseId(2809, 606);
        }
    }
}