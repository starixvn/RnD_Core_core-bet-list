namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            for (var betType = YeeBetBetTypes.LowestBetTypeIdInRange + 1; betType <= YeeBetBetTypes.HighestBetTypeIdInRange; betType++)
            {
                MapIdToBaseId(betType, YeeBetBetTypes.LowestBetTypeIdInRange);
            }
        }
    }
}