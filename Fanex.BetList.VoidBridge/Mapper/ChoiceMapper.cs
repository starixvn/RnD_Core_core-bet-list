namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            for (int betTypeId = VoidbridgeBetTypes.BaseBetTypeIdRange1 + 1; betTypeId <= VoidbridgeBetTypes.HighestBetTypeIdInRangeRange1; betTypeId++)
            {
                MapIdToBaseId(betTypeId, VoidbridgeBetTypes.BaseBetTypeIdRange1);
            }

            for (int betTypeId = VoidbridgeBetTypes.BaseBetTypeIdRange2; betTypeId <= VoidbridgeBetTypes.HighestBetTypeIdInRangeRange2; betTypeId++)
            {
                MapIdToBaseId(betTypeId, VoidbridgeBetTypes.BaseBetTypeIdRange1);
            }
        }
    }
}