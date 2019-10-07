namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            MapCardClub();
        }

        private void MapCardClub()
        {
            for (int betType = CardClubBetTypes.VideoPoker + 1; betType <= CardClubBetTypes.HighestBetTypeIdInRange; betType++)
            {
                MapIdToBaseId(betType, CardClubBetTypes.VideoPoker);
            }
        }
    }
}