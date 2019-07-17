namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            MapIdToBaseId(InspireVirtualRacingBetTypes.Place, InspireVirtualRacingBetTypes.Win);
            MapIdToBaseId(InspireVirtualRacingBetTypes.WinPlace, InspireVirtualRacingBetTypes.Win);
            MapIdToBaseId(BetRadarVirtualSoccerBetTypes.FirstHalfOverUnder, BetRadarVirtualSoccerBetTypes.OverUnder);
            MapIdToBaseId(BetRadarVirtualSoccerBetTypes.FirstHalfHandicap, BetRadarVirtualSoccerBetTypes.Handicap);
            MapIdToBaseId(BetRadarVirtualBasketballBetTypes.OverUnder, BetRadarVirtualSoccerBetTypes.OverUnder);
            MapIdToBaseId(BetRadarVirtualBasketballBetTypes.FirstHalfOverUnder, BetRadarVirtualSoccerBetTypes.OverUnder);
            MapIdToBaseId(BetRadarVirtualBasketballBetTypes.Handicap, BetRadarVirtualSoccerBetTypes.Handicap);
            MapIdToBaseId(BetRadarVirtualBasketballBetTypes.FirstHalfHandicap, BetRadarVirtualSoccerBetTypes.Handicap);
            MapIdToBaseId(BetRadarVirtualBasketballBetTypes.HomeTeamOverUnder, BetRadarVirtualSoccerBetTypes.OverUnder);
            MapIdToBaseId(BetRadarVirtualBasketballBetTypes.AwayTeamOverUnder, BetRadarVirtualSoccerBetTypes.OverUnder);
        }
    }
}