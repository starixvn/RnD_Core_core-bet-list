namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class OddsMapper : BaseMapper
    {
        public OddsMapper()
        {
            for (var betType = SexyGamingBetTypes.Baccarat + 1; betType <= SexyGamingBetTypes.HighestBetTypeIdInRange; betType++)
            {
                MapIdToBaseId(betType, SexyGamingBetTypes.Baccarat);
            }
        }
    }
}