namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class OddsMapper : BaseMapper
    {
        public OddsMapper()
        {
            MapIdToBaseId(PlaytechBetTypes.JackpotPrize, PlaytechBetTypes.JackpotContribution);
        }
    }
}