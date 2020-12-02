namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            for (var betType = GDGBetTypes.Baccarat + 1; betType <= GDGBetTypes.HighestBetTypeIdInRange; betType++)
            {
                MapIdToBaseId(betType, GDGBetTypes.Baccarat);
            }
        }
    }
}