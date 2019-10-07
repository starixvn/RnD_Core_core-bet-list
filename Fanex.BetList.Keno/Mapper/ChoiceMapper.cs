namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            for (int betTypeId = KenoBetTypes.BaseBetTypeId + 1; betTypeId <= KenoBetTypes.HighestBetTypeIdInRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, KenoBetTypes.BaseBetTypeId);
            }
        }
    }
}