namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            for (int betTypeId = SABAxDBetTypes.LowestBetTypeIdInRange + 1; betTypeId <= SABAxDBetTypes.HighestBetTypeIdInRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, SABAxDBetTypes.LowestBetTypeIdInRange);
            }
        }
    }
}