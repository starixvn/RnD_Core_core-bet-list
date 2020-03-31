namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            for (var betTypeId = TableGamesBetTypes.RepresentativeBetTypeId; betTypeId <= TableGamesBetTypes.EndingRangeBetTypeId; betTypeId++)
            {
                MapIdToBaseId(betTypeId, TableGamesBetTypes.RepresentativeBetTypeId);
            }
        }
    }
}
