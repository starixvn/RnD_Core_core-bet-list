namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class OddsMapper : BaseMapper
    {
        public OddsMapper()
        {
            for (var betType = GDGBetTypes.Baccarat + 1; betType <= GDGBetTypes.HighestBetTypeIdInRange; betType++)
            {
                MapIdToBaseId(betType, GDGBetTypes.Baccarat);
            }
        }
    }
}