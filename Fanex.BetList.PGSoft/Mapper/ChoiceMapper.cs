namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            for (int betTypeId = PGSoftBetTypes.CaiShenWins + 1; betTypeId <= PGSoftBetTypes.PromotionPrize; betTypeId++)
            {
                MapIdToBaseId(betTypeId, PGSoftBetTypes.CaiShenWins);
            }
        }
    }
}