namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            for (int betTypeId = VoidBridgeBetTypes.BaseBetTypeId + 1; betTypeId <= VoidBridgeBetTypes.HighestBetTypeIdInRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, VoidBridgeBetTypes.BaseBetTypeId);
            }
        }
    }
}