namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            for (int betTypeId = AdvantPlayMiniBetTypes.Crash + 1; betTypeId <= AdvantPlayMiniBetTypes.HighestBetTypeIdInRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, AdvantPlayMiniBetTypes.Crash);
            }
        }
    }
}