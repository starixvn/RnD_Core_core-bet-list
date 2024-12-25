namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            MapVirtualSportsBetTypes();

            MapVirtualSports2BetTypes();

            MapForVirtualSports3();

            MapForVirtualSports5();
            
            MapForBetby();
        }

        private void MapForVirtualSports3()
        {
            for (var id = VirtualSports3BetTypes.FulltimeOneXTwo + 1; id <= VirtualSports3BetTypes.HighestBetTypeIdInRange; id++)
            {
                MapIdToBaseId(id, InspireVirtualRacingBetTypes.Win);
            }
        }

        private void MapVirtualSportsBetTypes()
        {
            MapIdToBaseId(InspireVirtualSoccerBetTypes.Handicap, InspireVirtualRacingBetTypes.Win);
            MapIdToBaseId(InspireVirtualSoccerBetTypes.OverUnder25Goals, InspireVirtualRacingBetTypes.Win);
            MapIdToBaseId(InspireVirtualSoccerBetTypes.CorrectScore, InspireVirtualRacingBetTypes.Win);
            MapIdToBaseId(InspireVirtualSoccerBetTypes.FulTime1X2, InspireVirtualRacingBetTypes.Win);
            MapIdToBaseId(InspireVirtualSoccerBetTypes.TotalGoal, InspireVirtualRacingBetTypes.Win);            
            MapIdToBaseId(InspireVirtualSoccerBetTypes.DoubleChance, InspireVirtualRacingBetTypes.Win);
            
            MapIdToBaseId(InspireVirtualRacingBetTypes.Place, InspireVirtualRacingBetTypes.Win);

            MapIdToBaseId(InspireVirtualTennisBetTypes.Moneyline, InspireVirtualRacingBetTypes.Win);
            MapIdToBaseId(InspireVirtualTennisBetTypes.CorrectScore, InspireVirtualRacingBetTypes.Win);
            MapIdToBaseId(InspireVirtualTennisBetTypes.TotalPoints, InspireVirtualRacingBetTypes.Win);
        }

        private void MapVirtualSports2BetTypes()
        {
            for (int betTypeId = BetRadarVirtualSoccerBetTypes.FirstHalf1X2; betTypeId <= BetRadarVirtualSoccerBetTypes.CorrectScore; betTypeId++)
            {
                MapIdToBaseId(betTypeId, InspireVirtualRacingBetTypes.Win);
            }

            for (int betTypeId = BetRadarVirtualBasketballBetTypes.MatchWinner; betTypeId <= BetRadarVirtualBasketballBetTypes.FullTimeRaceToXPoints; betTypeId++)
            {
                MapIdToBaseId(betTypeId, InspireVirtualRacingBetTypes.Win);
            }

            MapIdToBaseId(BetRadarSharedBetTypes.MixParlay, BetTypes.MixParlay);
            MapIdToBaseId(BetRadarVirtualBasketballBetTypes.HomeTeamOverUnder, InspireVirtualRacingBetTypes.Win);
            MapIdToBaseId(BetRadarVirtualBasketballBetTypes.AwayTeamOverUnder, InspireVirtualRacingBetTypes.Win);
            MapIdToBaseId(BetRadarVirtualBasketballBetTypes.EndingGame, InspireVirtualRacingBetTypes.Win);
            MapIdToBaseId(VirtualSports4BetTypes.MixParlay, BetTypes.MixParlay);
        }
        
        private void MapForVirtualSports5()
        {
            for (var id = VirtualSports5BetTypes.FullTime1X2; id < VirtualSports5BetTypes.MixParlay; id++)
            {
                MapIdToBaseId(id, InspireVirtualRacingBetTypes.Win);
            }

            MapIdToBaseId(VirtualSports5BetTypes.MixParlay, BetTypes.MixParlay);
        }
        
        private void MapForBetby()
        {
            for (var id = BetbyBetTypes.FT1X2; id < BetbyBetTypes.HighestBetTypeIdInRange; id++)
            {
                MapIdToBaseId(id, InspireVirtualRacingBetTypes.Win);
            }

        }
    }
}
