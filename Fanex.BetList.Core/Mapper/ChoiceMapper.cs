namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            Initialize();
        }

        private void Initialize()
        {
            MapForFastMarkets();
            MapForBasketBall();
            MapForPoolHouseAllInOne();

            MapIdToBaseId(BetTypes.SecondHalfHandicap, BetTypes.Handicap);
            MapIdToBaseId(BetTypes.FirstHalfDoubleChance, BetTypes.DoubleChance);
            MapIdToBaseId(BetTypes.ResultTotalGoals, BetTypes.FirstHalfResultTotalGoals);
            MapIdToBaseId(BetTypes.SecondHalfOverUnder3rd, BetTypes.SecondHalfOverUnder);
            MapIdToBaseId(BetTypes.SecondHalfHandicap3rd, BetTypes.Handicap);
            MapIdToBaseId(BetTypes.HomeTeamTotalCornersOverUnder, BetTypes.SecondHalfOverUnder);
            MapIdToBaseId(BetTypes.AwayTeamTotalCornersOverUnder, BetTypes.SecondHalfOverUnder);
            MapIdToBaseId(BetTypes.FirstHalfHomeCornerOverUnder, BetTypes.SecondHalfOverUnder);
            MapIdToBaseId(BetTypes.FirstHalfAwayCornerOverUnder, BetTypes.SecondHalfOverUnder);
            MapIdToBaseId(BetTypes.ResultAndFirstTeamToScoreOwn, BetTypes.ResultAndFirstTeamToScore);
            MapIdToBaseId(BetTypes.HomeTeamWhichHalfFirstGoal, BetTypes.WhichHalfFirstGoal);
            MapIdToBaseId(BetTypes.AwayTeamWhichHalfFirstGoal, BetTypes.WhichHalfFirstGoal);
            MapIdToBaseId(BetTypes.FirstTeam3Goals, BetTypes.FirstTeam2Goals);
            MapIdToBaseId(BetTypes.FirstHalfBothTeamsToScoreOwn, BetTypes.FirstHalfBothTeamsToScore);
            MapIdToBaseId(BetTypes.SecondHalfOddEvenOwn, BetTypes.SecondHalfOddEven);
            MapIdToBaseId(BetTypes.SecondHalf1X2Own, BetTypes.SecondHalf1X2);
            MapIdToBaseId(BetTypes.SecondHalfDoubleChanceOwn, BetTypes.SecondHalfDoubleChance);
            MapIdToBaseId(BetTypes.SecondHalfDrawNoBetOwn, BetTypes.SecondHalfDrawNoBet);
            MapIdToBaseId(BetTypes.SecondHalfBothTeamsToScoreOwn, BetTypes.SecondHalfBothTeamsToScore);
            MapIdToBaseId(BetTypes.BothHalvesOver15YesNoOwn, BetTypes.BothHalvesOver15YesNo);
            MapIdToBaseId(BetTypes.BothHalvesUnder15YesNoOwn, BetTypes.BothHalvesUnder15YesNo);
            MapIdToBaseId(BetTypes.HomeToScoreInBothHalvesOwn, BetTypes.HomeToScoreInBothHalves);
            MapIdToBaseId(BetTypes.AwayToScoreInBothHalvesOwn, BetTypes.AwayToScoreInBothHalves);
            MapIdToBaseId(BetTypes.HomeToWinBothHalvesOwn, BetTypes.HomeToWinBothHalves);
            MapIdToBaseId(BetTypes.AwayToWinBothHalvesOwn, BetTypes.AwayToWinBothHalves);
            MapIdToBaseId(BetTypes.HomeToWinEitherHalfOwn, BetTypes.HomeToWinEitherHalf);
            MapIdToBaseId(BetTypes.AwayToWinEitherHalfOwn, BetTypes.AwayToWinEitherHalf);
            MapIdToBaseId(BetTypes.HighestScoringHalfOwn, BetTypes.HighestScoringHalf);
            MapIdToBaseId(BetTypes.HighestScoringHalfHomeTeamOwn, BetTypes.HighestScoringHalfHomeTeam);
            MapIdToBaseId(BetTypes.HighestScoringHalfAwayTeamOwn, BetTypes.HighestScoringHalfAwayTeam);
            MapIdToBaseId(BetTypes.HomeFirstHalfToScoreSecondHalfToScore, BetTypes.BothTeamsToScoreIn1H2H);
            MapIdToBaseId(BetTypes.AwayFirstHalfToScoreSecondHalfToScore, BetTypes.BothTeamsToScoreIn1H2H);
        }

        private void MapForBasketBall()
        {
            MapIdToBaseId(BetTypes.WhichTeamToScoreTheFirstBasket, BetTypes.WhichTeamToScoreTheHighestQuarter);
            MapIdToBaseId(BetTypes.WhichTeamToScoreTheLastBasket, BetTypes.WhichTeamToScoreTheHighestQuarter);
            MapIdToBaseId(BetTypes.SecondHalfRaceToXPoints, BetTypes.FirstHalfRaceToXPoints);
            MapIdToBaseId(BetTypes.QuarterXRaceToYPoints, BetTypes.FirstHalfRaceToXPoints);
            MapIdToBaseId(BetTypes.QuarterXHomeTeamOverUnder, BetTypes.QuarterXOverUnder);
            MapIdToBaseId(BetTypes.QuarterXAwayTeamOverUnder, BetTypes.QuarterXOverUnder);
            MapIdToBaseId(BetTypes.QuarterXWhichTeamToScoreTheLastBasket, BetTypes.FirstHalfRaceToXPoints);
        }

        private void MapForFastMarkets()
        {
            MapIdToBaseId(BetTypes.Next5Minutes, BetTypes.Next1Minute);
            MapIdToBaseId(BetTypes.WhatWillHappenFirstInNext5Minutes, BetTypes.WhatWillHappenFirstInNext1Minute);
            MapIdToBaseId(BetTypes.Next1MinuteSetPiece, BetTypes.WhatWillHappenFirstInNext1Minute);
            MapIdToBaseId(BetTypes.WhichCombinationWillHappenFirstInNext1Min, BetTypes.WhatWillHappenFirstInNext1Minute);
            MapIdToBaseId(BetTypes.WhichCombinationWillHappenFirstInNext5Mins, BetTypes.WhatWillHappenFirstInNext1Minute);
        }

        private void MapForPoolHouseAllInOne()
        {
            MapIdToBaseId(BetTypes.AwayTeamOverUnder, BetTypes.HomeTeamOverUnder);
            MapIdToBaseId(BetTypes.FirstHalfHomeTeamOverUnder, BetTypes.HomeTeamOverUnder);
            MapIdToBaseId(BetTypes.FirstHalfAwayTeamOverUnder, BetTypes.HomeTeamOverUnder);
            MapIdToBaseId(BetTypes.ExactAwayTeamGoalsOwn, BetTypes.ExactHomeTeamGoalsOwn);
            MapIdToBaseId(BetTypes.FirstHalfCorrectScoreAos, BetTypes.CorrectScoreAos);
            MapIdToBaseId(BetTypes.AwayTeamOverUnderDec, BetTypes.HomeTeamOverUnderDec);
            MapIdToBaseId(BetTypes.FirstHalfHomeTeamOverUnderDec, BetTypes.HomeTeamOverUnderDec);
            MapIdToBaseId(BetTypes.FirstHalfAwayTeamOverUnderDec, BetTypes.HomeTeamOverUnderDec);
            MapIdToBaseId(BetTypes.FirstHalf3WayHandicap, BetTypes.ThirdWayHandicap);
            MapIdToBaseId(BetTypes.FirstHalfBothTeamsToScoreResult, BetTypes.BothTeamsToScoreResult);
            MapIdToBaseId(BetTypes.FirstHalfBothTeamsToScoreTotalGoals, BetTypes.BothTeamsToScoreTotalGoals);
            MapIdToBaseId(BetTypes.Asian1X2Handicap, BetTypes.Handicap);
            MapIdToBaseId(BetTypes.FirstHalfAsian1X2Handicap, BetTypes.Handicap);
        }
    }
}