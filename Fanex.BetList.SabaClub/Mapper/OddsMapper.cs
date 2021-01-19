namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class OddsMapper : BaseMapper
    {
        public OddsMapper()
        {
            for (var betTypeId = SabaClubBetTypes.MinRange + 1; betTypeId <= SabaClubBetTypes.JackpotPrize; betTypeId++)
            {
                MapIdToBaseId(betTypeId, SabaClubBetTypes.MinRange);
            }
        }
    }
}