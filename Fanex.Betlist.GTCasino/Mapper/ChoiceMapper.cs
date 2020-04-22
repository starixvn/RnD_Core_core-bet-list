namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            for (var betType = GTCasinoBetTypes.Baccarat + 1; betType <= GTCasinoBetTypes.HighestBetTypeIdInRange; betType++)
            {
                MapIdToBaseId(betType, GTCasinoBetTypes.Baccarat);
            }
        }
    }
}