namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            for (var betType = IonCasinoBetTypes.Baccarat + 1; betType <= IonCasinoBetTypes.HighestBetTypeIdInRange; betType++)
            {
                MapIdToBaseId(betType, IonCasinoBetTypes.Baccarat);
            }
        }
    }
}