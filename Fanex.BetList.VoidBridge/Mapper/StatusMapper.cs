namespace Fanex.BetList.Core.Mapper
{
    using BetList.VoidBirdge.Constants;

    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            for (int betTypeId = BetTypeRanges.BaseBetTypeId + 1; betTypeId <= BetTypeRanges.MaxBetTypeIdRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, BetTypeRanges.BaseBetTypeId);
            }
        }
    }
}