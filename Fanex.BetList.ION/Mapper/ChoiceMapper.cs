namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            for (var betType = IonCasinoBetTypes.Baccarat + 1; betType <= IonCasinoBetTypes.HighestBetTypeIdInRange; betType++)
            {
                MapIdToBaseId(betType, IonCasinoBetTypes.Baccarat);
            }
        }
    }
}