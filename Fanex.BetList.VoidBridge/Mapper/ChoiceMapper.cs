namespace Fanex.BetList.Core.Mapper
{
    using BetList.VoidBirdge.Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            for (int betTypeId = BetTypeRanges.BaseBetTypeId + 1; betTypeId <= BetTypeRanges.MaxBetTypeIdRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, BetTypeRanges.BaseBetTypeId);
            }
        }
    }
}