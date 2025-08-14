namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Utils;

    public class OddsMapper : BaseMapper
    {
        public OddsMapper()
        {
            MapForScoreBox();

            RegisterMultipleOddsTypesForPHAIO4B();
            RegisterMultipleOddsTypesForBadmintonAIO();
            RegisterMultipleOddsTypesForPHAIO5();
            RegisterMultipleOddsTypesForVolleyballAIO();
            RegisterMultipleOddsTypesForBaseball();
            RegisterMultipleOddsTypesForBasketball();
        }

        private void MapForScoreBox()
        {
            MapIdToBaseId(BetTypes.ScoreBoxOverUnder, BetTypes.ScoreBoxHandicap);
        }

        private static void RegisterMultipleOddsTypesForBaseball()
        {
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.RunLine);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.TotalRuns);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.HomeTeamTotalRuns);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.AwayTeamTotalRuns);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.FirstXInningsMoneyline);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.FirstXInningsRunLine);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.FirstXInningsTotalRuns);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.FirstXInningsHomeTeamTotalRuns);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.FirstXInningsAwayTeamTotalRuns);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.XInningRunLine);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.XInningTotalRuns);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.BothTeamToScoreXRuns);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.ExtraInningYesNo);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.TotalHits);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.FirstTeamToScore);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.XInningHitsHandicap);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.XInningTotalHits);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.LastTeamToScoreBaseball);
        }

        private static void RegisterMultipleOddsTypesForPHAIO4B()
        {
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.CornersOddEvenOwn);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.FirstHalfCornersOddEvenOwn);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.SecondHalfCornersOddEven);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.TimeOfFirstCorner);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.TimeOfSecondHalfFirstCorner);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.FirstCorner2Way);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.FirstHalfFirstCorner2Way);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.SecondHalfFirstCorner2Way);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.LastCorner2Way);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.FirstHalfLastCorner2Way);
        }

        private static void RegisterMultipleOddsTypesForBadmintonAIO()
        {
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.MatchHandicap);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.MatchNumberOfGames);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.MatchPointHandicap);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.MatchPointOverUnder);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.MatchPointOddEven);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.GameXWinner);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.GameXPointHandicap);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.GameXPointOverUnder);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.GameXPointOddEven);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.GameXRaceToYPoints);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.GameXPointYWinner);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.GameXExtraPoints);
        }

        private static void RegisterMultipleOddsTypesForPHAIO5()
        {
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.AwayPenaltyShootoutRoundX);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.HomePenaltyShootoutRoundX);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.AwayPenaltyShootoutOverUnderFirst10);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.HomePenaltyShootoutOverUnderFirst10);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.AwayPenaltyShootoutOverUnderFinalResult);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.HomePenaltyShootoutOverUnderFinalResult);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.PenaltyShootoutToGoToSuddenDeath);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.PenaltyShootoutOddEvenFirst10);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.PenaltyShootoutOddEvenFinalResult);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.ExtraTimeOddEven);
        }

        private static void RegisterMultipleOddsTypesForVolleyballAIO()
        {
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.VolleyballFirstServe);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.VolleyballHighestScoringSetTotal);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.VolleyballSetXWinner15Points);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.VolleyballSetXPointsHandicap15Points);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.VolleyballSetXTotalPointsOverUnder15Points);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.VolleyballSetXTotalPointsOddEven);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.VolleyballSetXHomeOverUnder);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.VolleyballSetXAwayOverUnder);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.VolleyballSetXPointYWinner);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.VolleyballSetXRaceToYPoints);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.VolleyballWillThereBeA5thSet);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.VolleyballLeadingTeamAfterSetXFirstYPoints);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.VolleyballSetXMoneyline);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.SetXPointsHandicap);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.SetXTotalPointsOverUnder);
        }

        private static void RegisterMultipleOddsTypesForBasketball()
        {
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.PlayerPointsOverUnder);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.PlayerAssistsOverUnder);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.PlayerReboundsOverUnder);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.Player3PTFGMadeOverUnder);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.PlayerBlocksOverUnder);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.PlayerStealsOverUnder);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.PlayerPointsReboundsAssistsOverUnder);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.PlayerReboundsAssistsOverUnder);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.PlayerPointsReboundsOverUnder);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.PlayerStealsBlocksOverUnder);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.PlayerPerformanceDoublesAndOthers);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.PlayerPerformanceTriplesAndOthers);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.PlayerTripleDouble);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.MostPointsScored);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.MostRebounds);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.MostAssists);
            ConvertByBetType.RegisterMultipleOddsTypes(BetTypes.MostSteals);
        }
    }
}