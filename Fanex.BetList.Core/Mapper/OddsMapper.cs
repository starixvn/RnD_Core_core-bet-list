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
        }

        private void MapForScoreBox()
        {
            MapIdToBaseId(BetTypes.ScoreBoxOverUnder, BetTypes.ScoreBoxHandicap);
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
    }
}