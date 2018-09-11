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
            MapForESports2();
        }

        private void MapForESports2()
        {
            const int baseBetTypeId = 9001;
            const int startFollowingBetTypeId = 9002;
            const int endFollowingBetTypeId = 9086;

            for (int betTypeId = startFollowingBetTypeId; betTypeId <= endFollowingBetTypeId; betTypeId++)
            {
                MapIdToBaseId(betTypeId, baseBetTypeId);
            }
        }
    }
}