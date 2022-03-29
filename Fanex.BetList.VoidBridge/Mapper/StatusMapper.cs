namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            for (int betTypeId = VoidBridgeBetTypes.BaseBetTypeIdRange1 + 1; betTypeId <= VoidBridgeBetTypes.HighestBetTypeIdInRangeRange1; betTypeId++)
            {
                MapIdToBaseId(betTypeId, VoidBridgeBetTypes.BaseBetTypeIdRange1);
            }

            for (int betTypeId = VoidBridgeBetTypes.BaseBetTypeIdRange2; betTypeId <= VoidBridgeBetTypes.HighestBetTypeIdInRangeRange2; betTypeId++)
            {
                MapIdToBaseId(betTypeId, VoidBridgeBetTypes.BaseBetTypeIdRange1);
            }

        }
    }
}