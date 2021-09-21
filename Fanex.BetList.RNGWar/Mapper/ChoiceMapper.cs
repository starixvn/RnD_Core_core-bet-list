namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            for (var betType = RNGWarBetTypes.RNGWar + 1; betType <= RNGWarBetTypes.HighestBetTypeIdInRange; betType++)
            {
                MapIdToBaseId(betType, RNGWarBetTypes.RNGWar);
            }
        }
    }
}