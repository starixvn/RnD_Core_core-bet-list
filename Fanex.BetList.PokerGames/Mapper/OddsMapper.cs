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
            const int startFollowingBetTypeId = 1402;
            const int endFollowingBetTypeId = 1499;

            for (int betTypeId = startFollowingBetTypeId; betTypeId <= endFollowingBetTypeId; betTypeId++)
            {
                MapIdToBaseId(betTypeId, 1401);
            }
        }
    }
}