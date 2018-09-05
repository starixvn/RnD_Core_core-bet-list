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
            const int startFollowingBetTypeId = 1402;
            const int endFollowingBetTypeId = 1498;
            const int baseBetTypeId = 1401;

            for (int betTypeId = startFollowingBetTypeId; betTypeId <= endFollowingBetTypeId; betTypeId++)
            {
                MapIdToBaseId(betTypeId, baseBetTypeId);
            }
        }
    }
}