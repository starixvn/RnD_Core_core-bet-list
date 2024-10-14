namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class StakeMapper : BaseMapper
    {
        public StakeMapper()
        {
            MapIdToBaseId(VGamingRange1BetTypes.JackpotPrize, VGamingRange1BetTypes.JackpotContribution);
            MapIdToBaseId(VGamingRange1BetTypes.PromotionPrize, VGamingRange1BetTypes.JackpotContribution);
        }
    }
}