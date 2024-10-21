namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            for (int betTypeId = PlaytechBetTypes.BuffaloBlitz + 1; betTypeId < PlaytechBetTypes.HighestBetTypeIdInRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, PlaytechBetTypes.BuffaloBlitz);
            }
        }
    }
}