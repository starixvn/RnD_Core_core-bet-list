namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            MapForOtherBetTypes();
            MapForBetRadar();
            MapForSportingSolution();
            MapForFastMarkets();
            MapForBasketBall();
            MapForPoolHouseAllInOne();
            MapForPoolHouseAllInOnePhase2();
        }

        private void MapForOtherBetTypes()
        {
            MapIdToBaseId(BetTypes.SecondHalfHandicap, BetTypes.Handicap);
            MapIdToBaseId(BetTypes.ResultTotalGoals, BetTypes.FirstHalfResultTotalGoals);
            MapIdToBaseId(BetTypes.FirstHalfDoubleChance, BetTypes.DoubleChance);
        }

        private void MapForBetRadar()
        {
            MapIdToBaseId(BetTypes.OddEven3rd, BetTypes.OddEven);
            MapIdToBaseId(BetTypes.ExactAwayTeamGoals, BetTypes.ExactHomeTeamGoals);
            MapIdToBaseId(BetTypes.ExtraTimeNextGoal, BetTypes.NextGoal3rd);
            MapIdToBaseId(BetTypes.ExtraTimeCorrectScore, BetTypes.ExtraTimeFirstHalfCorrectScore);
            MapIdToBaseId(BetTypes.WhoAdvancesToNextRound, BetTypes.Moneyline);
            MapIdToBaseId(BetTypes.ExtraTimeYesNo, BetTypes.HomeToWinBothHalves);
            MapIdToBaseId(BetTypes.ExtraTimeAndGoal, BetTypes.HomeToWinBothHalves);
            MapIdToBaseId(BetTypes.SecondHalfOverUnder3rd, BetTypes.SecondHalfOverUnder);
            MapIdToBaseId(BetTypes.FirstHalfNextGoal, BetTypes.NextGoal3rd);
            MapIdToBaseId(BetTypes.FirstHalfExactHomeTeamGoals, BetTypes.ExactHomeTeamGoals);
            MapIdToBaseId(BetTypes.FirstHalfExactAwayTeamGoals, BetTypes.ExactHomeTeamGoals);
            MapIdToBaseId(BetTypes.SecondHalfHandicap3rd, BetTypes.Handicap);
            MapIdToBaseId(BetTypes.SecondHalfOddEven, BetTypes.FirstHalfHomeTeamOddEven);
            MapIdToBaseId(BetTypes.SecondHalfDrawNoBet, BetTypes.DrawNoBet);
            MapIdToBaseId(BetTypes.FirstHalfBothTeamsToScore, BetTypes.HomeToWinBothHalves);
            MapIdToBaseId(BetTypes.BothHalvesOver15YesNo, BetTypes.HomeToWinBothHalves);
            MapIdToBaseId(BetTypes.BothHalvesUnder15YesNo, BetTypes.HomeToWinBothHalves);
            MapIdToBaseId(BetTypes.FirstHalfDrawNoBet, BetTypes.DrawNoBet);
            MapIdToBaseId(BetTypes.FirstGoalSpecificTime10Min, BetTypes.NextGoalTime);
            MapIdToBaseId(BetTypes.FirstGoalSpecificTime15Min, BetTypes.NextGoalTime);
            MapIdToBaseId(BetTypes.CornersOddEven, BetTypes.FirstHalfHomeTeamOddEven);
            MapIdToBaseId(BetTypes.AwayTeamExactCorners, BetTypes.HomeTeamExactCorners);
            MapIdToBaseId(BetTypes.HomeTeamTotalCornersOverUnder, BetTypes.SecondHalfOverUnder);
            MapIdToBaseId(BetTypes.AwayTeamTotalCornersOverUnder, BetTypes.SecondHalfOverUnder);
            MapIdToBaseId(BetTypes.FirstHalfAwayTeamExactCorners, BetTypes.FirstHalfHomeTeamExactCorners);
            MapIdToBaseId(BetTypes.FirstHalfTotalCorners, BetTypes.HomeTeamExactCorners);
            MapIdToBaseId(BetTypes.FirstHalfCornersOddEven, BetTypes.FirstHalfHomeTeamOddEven);
            MapIdToBaseId(BetTypes.FirstHalfHomeCornerOverUnder, BetTypes.SecondHalfOverUnder);
            MapIdToBaseId(BetTypes.FirstHalfAwayCornerOverUnder, BetTypes.SecondHalfOverUnder);
            MapIdToBaseId(BetTypes.FirstHalfFirstCorner, BetTypes.FirstCorner);
            MapIdToBaseId(BetTypes.LastCorner, BetTypes.FirstCorner);
            MapIdToBaseId(BetTypes.FirstHalfLastCorner, BetTypes.FirstCorner);
            MapIdToBaseId(BetTypes.FirstHalfPlayerSentOff, BetTypes.HomeToWinBothHalves);
            MapIdToBaseId(BetTypes.HomeTeamPlayerSentOff, BetTypes.HomeToWinBothHalves);
            MapIdToBaseId(BetTypes.FirstHalfHomeTeamPlayerSentOff, BetTypes.HomeToWinBothHalves);
            MapIdToBaseId(BetTypes.AwayTeamPlayerSentOff, BetTypes.HomeToWinBothHalves);
            MapIdToBaseId(BetTypes.FirstHalfAwayTeamPlayerSentOff, BetTypes.HomeToWinBothHalves);
            MapIdToBaseId(BetTypes.GoalScorerFirst, BetTypes.GoalScorerAnytime);
        }

        private void MapForSportingSolution()
        {
            MapIdToBaseId(BetTypes.SSMatchWinner, BetTypes.Moneyline);
            MapIdToBaseId(BetTypes.SetHandicap, BetTypes.Handicap);
            MapIdToBaseId(BetTypes.MatchTotalGamesOddEven, BetTypes.OddEven);
            MapIdToBaseId(BetTypes.MatchTotalGamesOverUnder, BetTypes.OverUnder);
            MapIdToBaseId(BetTypes.MatchGamesHandicap3rd, BetTypes.Handicap);
            MapIdToBaseId(BetTypes.PlayerXMatchTotalGames2Way, BetTypes.TotalSets);
            MapIdToBaseId(BetTypes.SetXTotalGames3Way, BetTypes.MatchTotalGames3Way);
            MapIdToBaseId(BetTypes.SetXRaceToYGames, BetTypes.SetXWinner3rd);
            MapIdToBaseId(BetTypes.SetXScoreAfter2Games, BetTypes.MatchCorrectScore);
            MapIdToBaseId(BetTypes.SetXScoreAfter4Games, BetTypes.MatchCorrectScore);
            MapIdToBaseId(BetTypes.SetXScoreAfter6Games, BetTypes.MatchCorrectScore);
            MapIdToBaseId(BetTypes.SetXTotalDeuceGames2Way, BetTypes.TotalSets);
            MapIdToBaseId(BetTypes.SetXGameYWinner, BetTypes.SetXWinner3rd);
            MapIdToBaseId(BetTypes.SetXGameYCorrectScore, BetTypes.MatchCorrectScore);
            MapIdToBaseId(BetTypes.SetXGameYPointHandicap, BetTypes.SetXGameHandicap3rd);
            MapIdToBaseId(BetTypes.SetXGameYDueceBetting, BetTypes.SetXWillThereBeATiebreak);
            MapIdToBaseId(BetTypes.SetXGameYTotalPoints2Way, BetTypes.TotalSets);
            MapIdToBaseId(BetTypes.SetXGameYTotalPoints3Way, BetTypes.MatchTotalGames3Way);
            MapIdToBaseId(BetTypes.SetXGameYTotalPointZWinner, BetTypes.SetXWinner3rd);
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
            MapIdToBaseId(BetTypes.SecondHalfCorrectScore, BetTypes.CorrectScoreAos);
            MapIdToBaseId(BetTypes.ExactTotalGoalsOwn, BetTypes.ExactTotalGoals);
            MapIdToBaseId(BetTypes.ExactHomeTeamGoalsOwn, BetTypes.ExactHomeTeamGoals);
            MapIdToBaseId(BetTypes.ExactAwayTeamGoalsOwn, BetTypes.ExactHomeTeamGoals);
            MapIdToBaseId(BetTypes.FirstHalfDoubleChanceOwn, BetTypes.DoubleChance);
            MapIdToBaseId(BetTypes.FirstHalfDrawNoBetOwn, BetTypes.DrawNoBet);
            MapIdToBaseId(BetTypes.FirstHalfCorrectScoreAos, BetTypes.CorrectScoreAos);
            MapIdToBaseId(BetTypes.FirstHalf3WayHandicap, BetTypes.ThreeWayHandicap);
            MapIdToBaseId(BetTypes.FirstHalfBothTeamsToScoreResult, BetTypes.BothTeamsToScoreResult);
            MapIdToBaseId(BetTypes.FirstHalfBothTeamsToScoreTotalGoals, BetTypes.BothTeamsToScoreTotalGoals);
            MapIdToBaseId(BetTypes.HomeTeamOverUnderDec, BetTypes.HomeTeamOverUnder);
            MapIdToBaseId(BetTypes.AwayTeamOverUnderDec, BetTypes.HomeTeamOverUnder);
            MapIdToBaseId(BetTypes.FirstHalfHomeTeamOverUnderDec, BetTypes.HomeTeamOverUnder);
            MapIdToBaseId(BetTypes.FirstHalfAwayTeamOverUnderDec, BetTypes.HomeTeamOverUnder);
            MapIdToBaseId(BetTypes.Asian1X2Handicap, BetTypes.Handicap);
            MapIdToBaseId(BetTypes.FirstHalfAsian1X2Handicap, BetTypes.Handicap);
        }

        private void MapForPoolHouseAllInOnePhase2()
        {
            MapIdToBaseId(BetTypes.ResultAndFirstTeamToScoreOwn, BetTypes.ResultAndFirstTeamToScore);
            MapIdToBaseId(BetTypes.HomeTeamWhichHalfFirstGoal, BetTypes.WhichHalfFirstGoal);
            MapIdToBaseId(BetTypes.AwayTeamWhichHalfFirstGoal, BetTypes.WhichHalfFirstGoal);
            MapIdToBaseId(BetTypes.FirstTeam3Goals, BetTypes.FirstTeam2Goals);
            MapIdToBaseId(BetTypes.FirstHalfBothTeamsToScoreOwn, BetTypes.HomeToWinBothHalves);
            MapIdToBaseId(BetTypes.SecondHalfOddEvenOwn, BetTypes.FirstHalfHomeTeamOddEven);
            MapIdToBaseId(BetTypes.SecondHalf1X2Own, BetTypes.SecondHalf1X2);
            MapIdToBaseId(BetTypes.SecondHalfDoubleChanceOwn, BetTypes.SecondHalfDoubleChance);
            MapIdToBaseId(BetTypes.SecondHalfDrawNoBetOwn, BetTypes.DrawNoBet);
            MapIdToBaseId(BetTypes.SecondHalfBothTeamsToScoreOwn, BetTypes.SecondHalfBothTeamsToScore);
            MapIdToBaseId(BetTypes.BothHalvesOver15YesNoOwn, BetTypes.HomeToWinBothHalves);
            MapIdToBaseId(BetTypes.BothHalvesUnder15YesNoOwn, BetTypes.HomeToWinBothHalves);
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
    }
}