namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            for (var betType = RNGWarBetTypes.RNGWar + 1; betType <= RNGWarBetTypes.HighestBetTypeIdInRange; betType++)
            {
                MapIdToBaseId(betType, RNGWarBetTypes.RNGWar);
            }
        }
    }
}