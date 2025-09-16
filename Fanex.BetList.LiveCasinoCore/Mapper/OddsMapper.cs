namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class OddsMapper : BaseMapper
    {
        public OddsMapper()
        {
            MapIdToBaseId(LCBetTypes.PlaytechJackpotPrize, LCBetTypes.PlaytechJackpotContribution);
        }
    }
}