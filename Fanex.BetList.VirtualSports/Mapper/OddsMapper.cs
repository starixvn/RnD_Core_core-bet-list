namespace Fanex.BetList.Core.Mapper
{
    using Constants;
    using Fanex.BetList.Core.Utils;
    public class OddsMapper : BaseMapper
    {
        public OddsMapper()
        {
            MapIdToBaseId(InspireVirtualRacingBetTypes.Place, InspireVirtualRacingBetTypes.Win);
            MapForVirtualSports3();
            MapForVirtualSports4();
        }

        private void MapForVirtualSports3()
        {
            for (var id = VirtualSports3BetTypes.FulltimeOneXTwo; id <= VirtualSports3BetTypes.HighestBetTypeIdInRange; id++)
            {
                ConvertByBetType.RegisterMultipleOddsTypes(id);
            }
        }

        private void MapForVirtualSports4()
        {
            for (var id = VirtualSports4BetTypes.MatchWinner; id < VirtualSports4BetTypes.MixParlay; id++)
            {
                ConvertByBetType.RegisterMultipleOddsTypes(id);
            }
        }
    }
}