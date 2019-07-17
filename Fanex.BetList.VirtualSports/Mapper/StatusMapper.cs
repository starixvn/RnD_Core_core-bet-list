namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            MapIdToBaseId(InspireVirtualRacingBetTypes.Place, InspireVirtualRacingBetTypes.Win);
            MapIdToBaseId(BetRadarSharedBetTypes.MixParlay, BetTypes.MixParlay);
            MapIdToBaseId(BetRadarVirtualBasketballBetTypes.FullTimeRaceToXPoints, BetTypes.FirstHalfRaceToXPoints);
        }
    }
}