namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            for (var betType = WMBetTypes.Baccarat + 1; betType <= WMBetTypes.HighestBetTypeIdInRange; betType++)
            {
                MapIdToBaseId(betType, WMBetTypes.Baccarat);
            }
        }
    }
}