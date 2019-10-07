namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            MapCardClub();
        }

        private void MapCardClub()
        {
            for (int betType = CardClubBetTypes.VideoPoker + 1; betType <= CardClubBetTypes.HighestBetTypeId; betType++)
            {
                MapIdToBaseId(betType, CardClubBetTypes.VideoPoker);
            }
        }
    }
}