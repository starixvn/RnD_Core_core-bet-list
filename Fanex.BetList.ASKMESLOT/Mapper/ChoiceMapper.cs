namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            for (int betTypeId = ASKBettypes.MahjongLegendXmas + 1; betTypeId <= ASKBettypes.PromotionPrize; betTypeId++)
            {
                MapIdToBaseId(betTypeId, ASKBettypes.MahjongLegendXmas);
            }
        }
    }
}