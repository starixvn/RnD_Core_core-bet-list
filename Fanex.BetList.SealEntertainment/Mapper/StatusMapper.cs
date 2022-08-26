namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            for (int betTypeId = SealEntertainmentBetTypes.LowestBetTypeIdInRange + 1; betTypeId <= SealEntertainmentBetTypes.HighestBetTypeIdInRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, SealEntertainmentBetTypes.LowestBetTypeIdInRange);
            }
        }
    }
}
