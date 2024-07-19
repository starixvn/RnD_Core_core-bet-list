namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class StakeMapper : BaseMapper
    {
        public StakeMapper()
        {
            MapIdToBaseId(VGamingBetTypes.JackpotPrize, VGamingBetTypes.JackpotContribution);
            MapIdToBaseId(VGamingBetTypes.PromotionPrize, VGamingBetTypes.JackpotContribution);
        }
    }
}