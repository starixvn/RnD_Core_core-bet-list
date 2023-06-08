namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            for (int betTypeId = PGSoftBetTypes.CaiShenWins + 1; betTypeId < PGSoftBetTypes.PromotionPrize; betTypeId++)
            {
                MapIdToBaseId(betTypeId, PGSoftBetTypes.CaiShenWins);
            }
        }
    }
}