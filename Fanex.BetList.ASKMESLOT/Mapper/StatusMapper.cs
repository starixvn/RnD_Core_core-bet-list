namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            for (int betTypeId = ASKBettypes.MahjongLegendXmas + 1; betTypeId < ASKBettypes.PromotionPrize; betTypeId++)
            {
                MapIdToBaseId(betTypeId, ASKBettypes.MahjongLegendXmas);
            }
        }
    }
}