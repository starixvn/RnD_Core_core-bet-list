namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            for (var id = SabaClubBetTypes.MinRange + 1; id < SabaClubBetTypes.Blackjack; id++)
            {
                MapIdToBaseId(id, SabaClubBetTypes.MinRange);
            }

            for (var id = SabaClubBetTypes.Blackjack + 1; id <= SabaClubBetTypes.MaxRange; id++)
            {
                MapIdToBaseId(id, SabaClubBetTypes.MinRange);
            }
        }
    }
}