namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class OddsMapper : BaseMapper
    {
        public OddsMapper()
        {
            for (var betType = WMBetTypes.Baccarat + 1; betType <= WMBetTypes.HighestBetTypeIdInRange; betType++)
            {
                MapIdToBaseId(betType, WMBetTypes.Baccarat);
            }
        }
    }
}