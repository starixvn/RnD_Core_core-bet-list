namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class OddsMapper : BaseMapper
    {
        public OddsMapper()
        {
            for (var betType = YeeBetBetTypes.LowestBetTypeIdInRange + 1; betType <= YeeBetBetTypes.HighestBetTypeIdInRange; betType++)
            {
                MapIdToBaseId(betType, YeeBetBetTypes.LowestBetTypeIdInRange);
            }
        }
    }
}