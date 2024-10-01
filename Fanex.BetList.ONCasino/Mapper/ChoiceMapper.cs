namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            for (int betTypeId = ONBetTypes.BaseBetTypeId + 1; betTypeId <= ONBetTypes.HighestBetTypeId; betTypeId++)
            {
                MapIdToBaseId(betTypeId, ONBetTypes.BaseBetTypeId);
            }
        }
    }
}