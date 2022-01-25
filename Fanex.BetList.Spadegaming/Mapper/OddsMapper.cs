namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class OddsMapper : BaseMapper
    {
        public OddsMapper()
        {
            for (int betTypeId = SpadegamingBetTypes.TriplePanda; betTypeId <= SpadegamingBetTypes.JackpotPrize; betTypeId++)
            {
                MapIdToBaseId(betTypeId, SpadegamingBetTypes.FishingGod);
            }
        }
    }
}