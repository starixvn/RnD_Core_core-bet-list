namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            for (var betTypeId = SabaClubBetTypes.MinRange + 1; betTypeId < SabaClubBetTypes.JackpotContribution; betTypeId++)
            {
                MapIdToBaseId(betTypeId, SabaClubBetTypes.MinRange);
            }

            for (var betTypeId = SabaClub2BetTypes.MinRange; betTypeId <= SabaClub2BetTypes.MaxRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, SabaClubBetTypes.MinRange);
            }
        }
    }
}