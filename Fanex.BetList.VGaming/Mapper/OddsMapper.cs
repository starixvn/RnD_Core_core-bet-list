namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class OddsMapper : BaseMapper
    {
        public OddsMapper()
        {
            for (var betTypeId = VGamingBetTypes.MinRange + 1; betTypeId <= VGamingBetTypes.JackpotPrize; betTypeId++)
            {
                MapIdToBaseId(betTypeId, VGamingBetTypes.MinRange);
            }
        }
    }
}