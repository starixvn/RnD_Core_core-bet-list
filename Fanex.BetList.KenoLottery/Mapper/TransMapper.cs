namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class TransMapper : BaseMapper
    {
        public TransMapper()
        {
            for (var betTypeId = KenoLotteryBetTypes.Max5D90; betTypeId <= KenoLotteryBetTypes.EndingRangeBetTypeId; betTypeId++)
            {
                MapIdToBaseId(betTypeId, KenoLotteryBetTypes.Max5D60);
            }
        }
    }
}