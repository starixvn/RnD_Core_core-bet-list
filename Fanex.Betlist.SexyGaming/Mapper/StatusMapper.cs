namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            for (var betType = SexyGamingBetTypes.Baccarat + 1; betType <= SexyGamingBetTypes.HighestBetTypeIdInRange; betType++)
            {
                MapIdToBaseId(betType, SexyGamingBetTypes.Baccarat);
            }
        }
    }
}