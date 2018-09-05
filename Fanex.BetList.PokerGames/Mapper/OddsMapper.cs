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
            const int baseBetTypeId = 1401;

            for (int betTypeId = startFollowingBetTypeId; betTypeId <= endFollowingBetTypeId; betTypeId++)
            {
                MapIdToBaseId(betTypeId, baseBetTypeId);
            }
        }
    }
}