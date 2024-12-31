namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            for (int betTypeId = PlayStarBetTypes.MyLord + 1; betTypeId <= PlayStarBetTypes.HighestBetTypeIdInRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, PlayStarBetTypes.MyLord);
            }
        }
    }
}