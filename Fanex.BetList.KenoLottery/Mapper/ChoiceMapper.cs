namespace Fanex.BetList.Core.Mapper
{
    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            var beginningRangeBetTypeId = 1031;
            var endingRangeBetTypeId = 1049;

            for (var betTypeIdInRange = beginningRangeBetTypeId + 1; betTypeIdInRange <= endingRangeBetTypeId; betTypeIdInRange++)
            {
                MapIdToBaseId(betTypeIdInRange, beginningRangeBetTypeId);
            }
        }
    }
}