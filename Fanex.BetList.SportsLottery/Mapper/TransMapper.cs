namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class TransMapper : BaseMapper
    {
        public TransMapper()
        {
            for (var betTypeId = SportsLotteryBetTypes.RepresentativeBetTypeId; betTypeId <= SportsLotteryBetTypes.EndingRangeBetTypeId; betTypeId++)
            {
                MapIdToBaseId(betTypeId, SportsLotteryBetTypes.RepresentativeBetTypeId);
            }
        }
    }
}
