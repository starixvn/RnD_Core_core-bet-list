namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            for (int betTypeId = WEBetTypes.BaseBetTypeId + 1; betTypeId <= WEBetTypes.HighestBetTypeId; betTypeId++)
            {
                MapIdToBaseId(betTypeId, WEBetTypes.BaseBetTypeId);
            }
        }
    }
}