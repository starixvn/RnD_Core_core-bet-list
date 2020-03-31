namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            for (var betTypeId = TableGamesBetTypes.RepresentativeBetTypeId; betTypeId <= TableGamesBetTypes.EndingRangeBetTypeId; betTypeId++)
            {
                MapIdToBaseId(betTypeId, TableGamesBetTypes.RepresentativeBetTypeId);
            }
        }
    }
}
