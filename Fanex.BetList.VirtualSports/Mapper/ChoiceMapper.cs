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
            MapForVirtualSports3();
            MapForVirtualSports4();
            MapForVirtualSports5();
            MapForBetby();
        }

        private void MapForVirtualSports3()
        {
            MapIdToBaseId(VirtualSports3BetTypes.FirstOneXTwo, VirtualSports3BetTypes.FulltimeOneXTwo);
        }
        
        private void MapForVirtualSports4()
        {
            for (int bettype = VirtualSports4BetTypes.MatchWinner + 1; bettype < VirtualSports4BetTypes.MixParlay; bettype++)
            {
                MapIdToBaseId(bettype, VirtualSports4BetTypes.MatchWinner);
            }

            MapIdToBaseId(VirtualSports4BetTypes.MixParlay, BetRadarSharedBetTypes.MixParlay);
        }

        private void MapForVirtualSports5()
        {
            MapIdToBaseId(VirtualSports5BetTypes.FirstHalf1X2, VirtualSports5BetTypes.FullTime1X2);
            MapIdToBaseId(VirtualSports5BetTypes.MixParlay, BetRadarSharedBetTypes.MixParlay);
        }
        
        private void MapForBetby()
        {
            MapIdToBaseId(BetbyBetTypes.MatchWinner, BetbyBetTypes.FT1X2);
            MapIdToBaseId(BetbyBetTypes.HomeOverUnder, BetbyBetTypes.OverUnder);
            MapIdToBaseId(BetbyBetTypes.AwayOverUnder, BetbyBetTypes.OverUnder);
            MapIdToBaseId(BetbyBetTypes.QuarterXHomeOverUnder, BetbyBetTypes.QuarterXOverUnder);
            MapIdToBaseId(BetbyBetTypes.QuarterXAwayOverUnder, BetbyBetTypes.QuarterXOverUnder);
            
            for (var id = BetbyBetTypes.QuarterXAwayOverUnder + 1; id <= BetbyBetTypes.HighestBetTypeIdInRange; id++)
            {
                MapIdToBaseId(id, BetbyBetTypes.FT1X2);
            }
        }
    }
}