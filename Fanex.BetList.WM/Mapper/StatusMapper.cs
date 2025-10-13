namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            for (var betType = WMBetTypes.Baccarat + 1; betType <= WMBetTypes.HighestBetTypeIdInRange; betType++)
            {
                MapIdToBaseId(betType, WMBetTypes.Baccarat);
            }
        }
    }
}