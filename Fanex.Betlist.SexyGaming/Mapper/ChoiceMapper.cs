namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            for (var betType = SexyGamingBetTypes.Baccarat + 1; betType <= SexyGamingBetTypes.HighestBetTypeIdInRange; betType++)
            {
                MapIdToBaseId(betType, SexyGamingBetTypes.Baccarat);
            }
        }
    }
}