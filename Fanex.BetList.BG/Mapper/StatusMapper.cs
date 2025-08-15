namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            for (var betType = BGBetTypes.Baccarat + 1; betType <= BGBetTypes.HighestBetTypeIdInRange; betType++)
            {
                MapIdToBaseId(betType, BGBetTypes.Baccarat);
            }
        }
    }
}