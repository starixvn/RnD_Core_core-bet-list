namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class OddsMapper : BaseMapper
    {
        public OddsMapper()
        {
            for (var betTypeId = VGamingRange1BetTypes.MinRange + 1; betTypeId <= VGamingRange1BetTypes.JackpotPrize; betTypeId++)
            {
                MapIdToBaseId(betTypeId, VGamingRange1BetTypes.MinRange);
            }

            for (var betTypeId = VGamingRange2BetTypes.MinRange; betTypeId <= VGamingRange2BetTypes.MaxRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, VGamingRange1BetTypes.MinRange);
            }
        }
    }
}