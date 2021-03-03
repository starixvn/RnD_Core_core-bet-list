namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            for (var betTypeId = SportsLotteryBetTypes.RepresentativeBetTypeId; betTypeId <= SportsLotteryBetTypes.EndingRangeBetTypeId; betTypeId++)
            {
                MapIdToBaseId(betTypeId, SportsLotteryBetTypes.RepresentativeBetTypeId);
            }
        }
    }
}
