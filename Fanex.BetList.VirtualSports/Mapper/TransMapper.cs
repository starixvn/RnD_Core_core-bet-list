namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class TransMapper : BaseMapper
    {
        public TransMapper()
        {
            MapVirtualSportsBetTypes();

            MapVirtualSports2BetTypes();

            MapForVirtualSports3();

            MapForVirtualSports4();
        }

        private void MapForVirtualSports4()
        {
            for (var id = VirtualSports4BetTypes.MatchWinner + 1; id <= VirtualSports4BetTypes.MixParlay; id++)
            {
                MapIdToBaseId(id, VirtualSports4BetTypes.MatchWinner);
            }
        }

        private void MapForVirtualSports3()
        {
            for (var id = VirtualSports3BetTypes.FulltimeOneXTwo + 1; id <= VirtualSports3BetTypes.HighestBetTypeIdInRange; id++)
            {
                MapIdToBaseId(id, VirtualSports3BetTypes.FulltimeOneXTwo);
            }
        }

        private void MapVirtualSportsBetTypes()
        {
            MapIdToBaseId(InspireVirtualSoccerBetTypes.OverUnder25Goals, InspireVirtualSoccerBetTypes.Handicap);
            MapIdToBaseId(InspireVirtualSoccerBetTypes.CorrectScore, InspireVirtualSoccerBetTypes.Handicap);
            MapIdToBaseId(InspireVirtualSoccerBetTypes.FulTime1X2, InspireVirtualSoccerBetTypes.Handicap);
            MapIdToBaseId(InspireVirtualSoccerBetTypes.TotalGoal, InspireVirtualSoccerBetTypes.Handicap);
            MapIdToBaseId(InspireVirtualTennisBetTypes.Moneyline, InspireVirtualSoccerBetTypes.Handicap);
            MapIdToBaseId(InspireVirtualSoccerBetTypes.DoubleChance, InspireVirtualSoccerBetTypes.Handicap);
            MapIdToBaseId(InspireVirtualRacingBetTypes.Win, InspireVirtualSoccerBetTypes.Handicap);
            MapIdToBaseId(InspireVirtualRacingBetTypes.Place, InspireVirtualSoccerBetTypes.Handicap);
            MapIdToBaseId(InspireVirtualRacingBetTypes.WinPlace, InspireVirtualSoccerBetTypes.Handicap);
            MapIdToBaseId(InspireVirtualTennisBetTypes.CorrectScore, InspireVirtualSoccerBetTypes.Handicap);
            MapIdToBaseId(InspireVirtualTennisBetTypes.TotalPoints, InspireVirtualSoccerBetTypes.Handicap);
        }

        private void MapVirtualSports2BetTypes()
        {
            for (int betTypeId = BetRadarVirtualSoccerBetTypes.FirstHalf1X2; betTypeId <= BetRadarVirtualSoccerBetTypes.CorrectScore; betTypeId++)
            {
                MapIdToBaseId(betTypeId, BetRadarVirtualSoccerBetTypes.FullTime1X2);
            }

            MapIdToBaseId(BetRadarSharedBetTypes.MixParlay, BetRadarVirtualSoccerBetTypes.FullTime1X2);

            for (int betTypeId = BetRadarVirtualBasketballBetTypes.MatchWinner; betTypeId <= BetRadarVirtualBasketballBetTypes.FullTimeRaceToXPoints; betTypeId++)
            {
                MapIdToBaseId(betTypeId, BetRadarVirtualSoccerBetTypes.FullTime1X2);
            }

            MapIdToBaseId(BetRadarVirtualBasketballBetTypes.HomeTeamOverUnder, BetRadarVirtualSoccerBetTypes.FullTime1X2);
            MapIdToBaseId(BetRadarVirtualBasketballBetTypes.AwayTeamOverUnder, BetRadarVirtualSoccerBetTypes.FullTime1X2);
            MapIdToBaseId(BetRadarVirtualSoccerBetTypes.Win, BetRadarVirtualSoccerBetTypes.FullTime1X2);
            MapIdToBaseId(BetRadarVirtualSoccerBetTypes.Place, BetRadarVirtualSoccerBetTypes.FullTime1X2);
        }
    }
}
