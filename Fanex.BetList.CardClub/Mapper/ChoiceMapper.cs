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
            const int numberBettypeForTaiXiu = 100;
            for (var i = 1; i < numberBettypeForTaiXiu; i++)
            {
                MapIdToBaseId(CardClubBetTypes.TaiXiu + i, CardClubBetTypes.TaiXiu);
            }
        }
    }
}