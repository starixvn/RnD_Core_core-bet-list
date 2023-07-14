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
        }

        private void MapForVirtualSports3()
        {
            for (var id = VirtualSports3BetTypes.FulltimeOneXTwo; id <= VirtualSports3BetTypes.HighestBetTypeIdInRange; id++)
            {
                ConvertByBetType.RegisterMultipleOddsTypes(id);
            }
        }
    }
}