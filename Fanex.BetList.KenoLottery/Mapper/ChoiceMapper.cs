namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            for (var betTypeId = KenoLotteryBetTypes.Max5D90; betTypeId <= KenoLotteryBetTypes.EndingRangeBetTypeId; betTypeId++)
            {
                MapIdToBaseId(betTypeId, KenoLotteryBetTypes.Max5D60);
            }
        }
    }
}