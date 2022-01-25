namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            for (int betTypeId = SpadegamingBetTypes.TriplePanda; betTypeId < SpadegamingBetTypes.JackpotContribution; betTypeId++)
            {
                MapIdToBaseId(betTypeId, SpadegamingBetTypes.FishingGod);
            }
        }
    }
}