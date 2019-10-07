namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            for (int betTypeId = VoidBridgeBetTypes.BaseBetTypeId + 1; betTypeId <= VoidBridgeBetTypes.HighestBetTypeIdInRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, VoidBridgeBetTypes.BaseBetTypeId);
            }
        }
    }
}