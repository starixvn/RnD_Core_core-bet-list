namespace Fanex.BetList.Core.Mapper
{
    internal class TransMapper : BaseMapper
    {
        public TransMapper()
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