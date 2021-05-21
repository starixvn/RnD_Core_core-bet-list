namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            for (int betTypeId = AdvantPlayMiniBetTypes.Crash + 1; betTypeId <= AdvantPlayMiniBetTypes.HighestBetTypeIdInRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, AdvantPlayMiniBetTypes.Crash);
            }
        }
    }
}