namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class StakeMapper : BaseMapper
    {
        public StakeMapper()
        {
            MapIdToBaseId(BetTypes.SabaRewardRiskFreeRebate, BetTypes.SabaLoyaltyPointsExchange);

            for (var i = BetTypes.SabaCoinsLowest; i <= BetTypes.SabaCoinsHighest; i++)
            {
                MapIdToBaseId(i, BetTypes.SabaLoyaltyPointsExchange);
            }
        }
    }
}
