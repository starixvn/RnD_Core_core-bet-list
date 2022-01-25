namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            for (int betTypeId = SpadegamingBetTypes.TriplePanda; betTypeId < SpadegamingBetTypes.JackpotContribution; betTypeId++)
            {
                MapIdToBaseId(betTypeId, SpadegamingBetTypes.FishingGod);
            }
        }
    }
}