namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            for (var betType = GTCasinoBetTypes.Baccarat + 1; betType <= GTCasinoBetTypes.HighestBetTypeIdInRange; betType++)
            {
                MapIdToBaseId(betType, GTCasinoBetTypes.Baccarat);
            }
        }
    }
}