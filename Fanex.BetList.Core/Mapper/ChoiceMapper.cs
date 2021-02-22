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
            MapForPHAIO();
            MapForPHAIO2();
            MapForPHAIO4B();
            MapForCricket();
            MapForBadmintonAIO2();
            MapForPHAIO5();
            MapForCricketAIOP1();
            MapForCricketAIOForOddEven();
            MapForCricketAIOForFancy();
        }

        private void MapForOtherBetTypes()
        {
            MapIdToBaseId(BetTypes.SecondHalfHandicap, BetTypes.Handicap);
            MapIdToBaseId(BetTypes.ResultTotalGoals, BetTypes.FirstHalfResultTotalGoals);
            MapIdToBaseId(BetTypes.FirstHalfDoubleChance, BetTypes.DoubleChance);
            MapIdToBaseId(BetTypes.SabaLoyaltyPointsExchange, BetTypes.SabaLuckySpin);
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
            MapIdToBaseId(BetTypes.SetXScoreAfter2Games, BetTypes.MatchCorrectScore3rd);
            MapIdToBaseId(BetTypes.SetXScoreAfter4Games, BetTypes.MatchCorrectScore3rd);
            MapIdToBaseId(BetTypes.SetXScoreAfter6Games, BetTypes.MatchCorrectScore3rd);
            MapIdToBaseId(BetTypes.SetXTotalDeuceGames2Way, BetTypes.TotalSets);
            MapIdToBaseId(BetTypes.SetXGameYWinner, BetTypes.SetXWinner3rd);
            MapIdToBaseId(BetTypes.SetXGameYCorrectScore, BetTypes.MatchCorrectScore3rd);
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
            for (var i = BetTypes.LastDigitScore; i <= BetTypes.AwayTeamLastDigitScore2H; i++)
            {
                MapIdToBaseId(i, BetTypes.CorrectScore);
            }

            for (var i = BetTypes.QuarterXHomeTeamLastDigitScore; i <= BetTypes.QuarterX1X2; i++)
            {
                MapIdToBaseId(i, BetTypes.QuarterXLastDigitScore);
            }

            MapIdToBaseId(BetTypes.Quarter3Quarter4Result, BetTypes.Quarter1Quarter2Result);
            MapIdToBaseId(BetTypes.QuarterToWinToNil, BetTypes.Moneyline);
            MapIdToBaseId(BetTypes.QuarterToWinFromBehind, BetTypes.Moneyline);
            MapIdToBaseId(BetTypes.QuartersWinnersHandicap, BetTypes.Handicap);
            MapIdToBaseId(BetTypes.AwayTeamQuartersWinOverUnder, BetTypes.HomeTeamQuartersWinOverUnder);
            MapIdToBaseId(BetTypes.QuarterDrawOverUnder, BetTypes.HomeTeamQuartersWinOverUnder);
            MapIdToBaseId(BetTypes.FirstHalfSecondHalfResult, BetTypes.Quarter1Quarter2Result);
        }

        private void MapForFastMarkets()
        {
            MapIdToBaseId(BetTypes.Next5Minutes, BetTypes.Next1Minute);
            MapIdToBaseId(BetTypes.WhatWillHappenFirstInNext5Minutes, BetTypes.WhatWillHappenFirstInNext1Minute);
            MapIdToBaseId(BetTypes.Next1MinuteSetPiece, BetTypes.WhatWillHappenFirstInNext1Minute);
            MapIdToBaseId(BetTypes.WhichCombinationWillHappenFirstInNext1Min, BetTypes.WhatWillHappenFirstInNext1Minute);
            MapIdToBaseId(BetTypes.WhichCombinationWillHappenFirstInNext5Mins, BetTypes.WhatWillHappenFirstInNext1Minute);
        }

        private void MapForPHAIO()
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

        private void MapForPHAIO2()
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

        private void MapForPHAIO4B()
        {
            MapIdToBaseId(BetTypes.CornersOddEvenOwn, BetTypes.FirstHalfHomeTeamOddEven);
            MapIdToBaseId(BetTypes.FirstHalfCornersOddEvenOwn, BetTypes.FirstHalfHomeTeamOddEven);
            MapIdToBaseId(BetTypes.SecondHalfCornersOddEven, BetTypes.FirstHalfHomeTeamOddEven);
            MapIdToBaseId(BetTypes.FirstHalfTotalCornersOwn, BetTypes.PHAIOTotalCorners);
            MapIdToBaseId(BetTypes.FirstHalfAlternativeCorners, BetTypes.AlternativeCorners);
            MapIdToBaseId(BetTypes.Corner3WayHandicap, BetTypes.ThreeWayHandicap);
            MapIdToBaseId(BetTypes.FirstHalfCorner3WayHandicap, BetTypes.ThreeWayHandicap);
            MapIdToBaseId(BetTypes.TimeOfFirstHalfFirstCorner, BetTypes.TimeOfFirstCorner);
            MapIdToBaseId(BetTypes.TimeOfSecondHalfFirstCorner, BetTypes.TimeOfFirstCorner);
            MapIdToBaseId(BetTypes.HomeTeamCornersOverUnder, BetTypes.HomeTeamOverUnder);
            MapIdToBaseId(BetTypes.AwayTeamCornersOverUnder, BetTypes.HomeTeamOverUnder);
            MapIdToBaseId(BetTypes.FirstHalfHomeTeamCornersOverUnder, BetTypes.HomeTeamOverUnder);
            MapIdToBaseId(BetTypes.FirstHalfAwayTeamCornersOverUnder, BetTypes.HomeTeamOverUnder);
            MapIdToBaseId(BetTypes.FirstHalfCornersRace, BetTypes.CornersRace);
            MapIdToBaseId(BetTypes.FirstHalfFirstCorner2Way, BetTypes.FirstCorner2Way);
            MapIdToBaseId(BetTypes.SecondHalfFirstCorner2Way, BetTypes.FirstCorner2Way);
            MapIdToBaseId(BetTypes.LastCorner2Way, BetTypes.FirstCorner2Way);
            MapIdToBaseId(BetTypes.FirstHalfLastCorner2Way, BetTypes.FirstCorner2Way);
            MapIdToBaseId(BetTypes.SecondHalfCorrectCorners, BetTypes.FirstHalfCorrectCorners);
            MapIdToBaseId(BetTypes.CornerHighestScoringHalf, BetTypes.HighestScoringHalf);
            MapIdToBaseId(BetTypes.CornerHighestScoringHalf2Way, BetTypes.HighestScoringHalf2Way);
        }

        private void MapForCricket()
        {
            MapIdToBaseId(BetTypes.CricketMatchWinner, BetTypes.Moneyline);
        }

        private void MapForBadmintonAIO2()
        {
            MapIdToBaseId(BetTypes.MatchHandicap, BetTypes.Handicap);
            MapIdToBaseId(BetTypes.MatchNumberOfGames, BetTypes.SecondHalfOverUnder);
            MapIdToBaseId(BetTypes.MatchPointHandicap, BetTypes.Handicap);
            MapIdToBaseId(BetTypes.MatchPointOverUnder, BetTypes.SecondHalfOverUnder);
            MapIdToBaseId(BetTypes.GameXPointYWinner, BetTypes.GameXRaceToYPoints);
        }

        private void MapForPHAIO5()
        {
            MapIdToBaseId(BetTypes.AwayPenaltyShootoutRoundX, BetTypes.HomePenaltyShootoutRoundX);
            MapIdToBaseId(BetTypes.AwayPenaltyShootoutOverUnderFirst10, BetTypes.HomeTeamOverUnder);
            MapIdToBaseId(BetTypes.HomePenaltyShootoutOverUnderFirst10, BetTypes.HomeTeamOverUnder);
            MapIdToBaseId(BetTypes.AwayPenaltyShootoutOverUnderFinalResult, BetTypes.HomeTeamOverUnder);
            MapIdToBaseId(BetTypes.HomePenaltyShootoutOverUnderFinalResult, BetTypes.HomeTeamOverUnder);
            MapIdToBaseId(BetTypes.PenaltyShootoutToGoToSuddenDeath, BetTypes.HomeToWinBothHalves);
            MapIdToBaseId(BetTypes.PenaltyShootoutOddEvenFirst10, BetTypes.FirstHalfHomeTeamOddEven);
            MapIdToBaseId(BetTypes.PenaltyShootoutOddEvenFinalResult, BetTypes.FirstHalfHomeTeamOddEven);
            MapIdToBaseId(BetTypes.ExtraTimeNextGoalMethod, BetTypes.FirstGoalMethod);
            MapIdToBaseId(BetTypes.ExtraTimeNextGoalOwn, BetTypes.NextGoal);
            MapIdToBaseId(BetTypes.ExtraTimeDoubleChance, BetTypes.DoubleChance);
            MapIdToBaseId(BetTypes.ExtraTimeCorrectScoreOwn, BetTypes.CorrectScoreAos);
            MapIdToBaseId(BetTypes.ExtraTimeOddEven, BetTypes.FirstHalfHomeTeamOddEven);
        }

        private void MapForCricketAIOP1()
        {
            MapIdToBaseId(BetTypes.TossWinner, BetTypes.SuperOverWinner);
            MapIdToBaseId(BetTypes.AwayInnsRuns, BetTypes.HomeInnsRuns);
            MapIdToBaseId(BetTypes.Home1stInnsRuns, BetTypes.HomeInnsRuns);
            MapIdToBaseId(BetTypes.Away1stInnsRuns, BetTypes.HomeInnsRuns);
            MapIdToBaseId(BetTypes.Home2ndInnsRuns, BetTypes.HomeInnsRuns);
            MapIdToBaseId(BetTypes.Away2ndInnsRuns, BetTypes.HomeInnsRuns);
            MapIdToBaseId(BetTypes.AwayGroup1XRuns, BetTypes.HomeGroup1XRuns);
            MapIdToBaseId(BetTypes.Home1stInnsGroup1XRuns, BetTypes.HomeGroup1XRuns);
            MapIdToBaseId(BetTypes.Away1stInnsGroup1XRuns, BetTypes.HomeGroup1XRuns);
            MapIdToBaseId(BetTypes.Home2ndInnsGroup1XRuns, BetTypes.HomeGroup1XRuns);
            MapIdToBaseId(BetTypes.Away2ndInnsGroup1XRuns, BetTypes.HomeGroup1XRuns);
            MapIdToBaseId(BetTypes.HomeScoreAtEndOfOverX, BetTypes.HomeGroup1XRuns);
            MapIdToBaseId(BetTypes.AwayScoreAtEndOfOverX, BetTypes.HomeGroup1XRuns);
            MapIdToBaseId(BetTypes.Home1stInnsScoreAtEndOfOverX, BetTypes.HomeGroup1XRuns);
            MapIdToBaseId(BetTypes.Away1stInnsScoreAtEndOfOverX, BetTypes.HomeGroup1XRuns);
            MapIdToBaseId(BetTypes.Home2ndInnsScoreAtEndOfOverX, BetTypes.HomeGroup1XRuns);
            MapIdToBaseId(BetTypes.Away2ndInnsScoreAtEndOfOverX, BetTypes.HomeGroup1XRuns);
            MapIdToBaseId(BetTypes.HomeScoreAfterOverXBallY, BetTypes.HomeGroup1XRuns);
            MapIdToBaseId(BetTypes.AwayScoreAfterOverXBallY, BetTypes.HomeGroup1XRuns);
            MapIdToBaseId(BetTypes.HomeFallOfWicketX, BetTypes.HomeGroup1XRuns);
            MapIdToBaseId(BetTypes.AwayFallOfWicketX, BetTypes.HomeGroup1XRuns);
            MapIdToBaseId(BetTypes.Home1stInnsFallOfWicketX, BetTypes.HomeGroup1XRuns);
            MapIdToBaseId(BetTypes.Away1stInnsFallOfWicketX, BetTypes.HomeGroup1XRuns);
            MapIdToBaseId(BetTypes.Home2ndInnsFallOfWicketX, BetTypes.HomeGroup1XRuns);
            MapIdToBaseId(BetTypes.Away2ndInnsFallOfWicketX, BetTypes.HomeGroup1XRuns);
            MapIdToBaseId(BetTypes.MatchFours, BetTypes.HomeInnsRuns);
            MapIdToBaseId(BetTypes.MatchSixes, BetTypes.HomeInnsRuns);
            MapIdToBaseId(BetTypes.MatchWickets, BetTypes.HomeInnsRuns);
            MapIdToBaseId(BetTypes.MatchExtras, BetTypes.HomeInnsRuns);
            MapIdToBaseId(BetTypes.HighestIndividualScore, BetTypes.HomeInnsRuns);
            MapIdToBaseId(BetTypes.HighestOverScore, BetTypes.HomeInnsRuns);
            MapIdToBaseId(BetTypes.HomeInnsFours, BetTypes.HomeInnsRuns);
            MapIdToBaseId(BetTypes.AwayInnsFours, BetTypes.HomeInnsRuns);
            MapIdToBaseId(BetTypes.HomeInnsSixes, BetTypes.HomeInnsRuns);
            MapIdToBaseId(BetTypes.AwayInnsSixes, BetTypes.HomeInnsRuns);
            MapIdToBaseId(BetTypes.HomeFirstInnsWicketXMethodOfDismissal, BetTypes.HomeWicketXMethodOfDismissal);
            MapIdToBaseId(BetTypes.HomeSecondInnsWicketXMethodOfDismissal, BetTypes.HomeWicketXMethodOfDismissal);
            MapIdToBaseId(BetTypes.AwayFirstInnsWicketXMethodOfDismissal, BetTypes.HomeWicketXMethodOfDismissal);
            MapIdToBaseId(BetTypes.AwaySecondInnsWicketXMethodOfDismissal, BetTypes.HomeWicketXMethodOfDismissal);
            MapIdToBaseId(BetTypes.AwayWicketXMethodOfDismissal, BetTypes.HomeWicketXMethodOfDismissal);
            MapIdToBaseId(BetTypes.AwayOverXExactRuns, BetTypes.HomeOverXExactRuns);
            MapIdToBaseId(BetTypes.HomeOverXBallYExactRuns, BetTypes.HomeOverXExactRuns);
            MapIdToBaseId(BetTypes.AwayOverXBallYExactRuns, BetTypes.HomeOverXExactRuns); 
            MapIdToBaseId(BetTypes.MatchWinnerBackLay, BetTypes.OneXTwoBackLay);
            MapIdToBaseId(BetTypes.SuperOverWinnerWinnerBackLay, BetTypes.OneXTwoBackLay);
            MapIdToBaseId(BetTypes.OverUnderDec, BetTypes.HomeInnsRuns);    
        }

        private void MapForCricketAIOForOddEven()
        {
            MapIdToBaseId(BetTypes.AwayInnsRunsOE, BetTypes.HomeInnsRunsOE);
            MapIdToBaseId(BetTypes.Home1stInnsRunsOE, BetTypes.HomeInnsRunsOE);
            MapIdToBaseId(BetTypes.Away1stInnsRunsOE, BetTypes.HomeInnsRunsOE);
            MapIdToBaseId(BetTypes.Home2ndInnsRunsOE, BetTypes.HomeInnsRunsOE);
            MapIdToBaseId(BetTypes.Away2ndInnsRunsOE, BetTypes.HomeInnsRunsOE);
            MapIdToBaseId(BetTypes.MatchFoursOE, BetTypes.HomeInnsRunsOE);
            MapIdToBaseId(BetTypes.MatchSixesOE, BetTypes.HomeInnsRunsOE);
            MapIdToBaseId(BetTypes.MatchExtrasOE, BetTypes.HomeInnsRunsOE);
            MapIdToBaseId(BetTypes.HomeInnsFoursOE, BetTypes.HomeInnsRunsOE);
            MapIdToBaseId(BetTypes.AwayInnsFoursOE, BetTypes.HomeInnsRunsOE);
            MapIdToBaseId(BetTypes.AwayGroup1XRunsOE, BetTypes.HomeGroup1XRunsOE);
            MapIdToBaseId(BetTypes.Home1stInnsGroup1X, BetTypes.HomeGroup1XRunsOE);
            MapIdToBaseId(BetTypes.Away1stInnsGroup1X, BetTypes.HomeGroup1XRunsOE);
            MapIdToBaseId(BetTypes.Home2ndInnsGroup1XRunsOE, BetTypes.HomeGroup1XRunsOE);
            MapIdToBaseId(BetTypes.Away2ndInnsGroup1XRunsOE, BetTypes.HomeGroup1XRunsOE);
            MapIdToBaseId(BetTypes.HomeScoreAtEndOfOverXOE, BetTypes.HomeGroup1XRunsOE);
            MapIdToBaseId(BetTypes.AwayScoreAtEndOfOverXOE, BetTypes.HomeGroup1XRunsOE);
            MapIdToBaseId(BetTypes.Home1stInnsScoreAtEndOfOverXOE, BetTypes.HomeGroup1XRunsOE);
            MapIdToBaseId(BetTypes.Away1stInnsScoreAtEndOfOverXOE, BetTypes.HomeGroup1XRunsOE);
            MapIdToBaseId(BetTypes.Home2ndInnsScoreAtEndOfOverXOE, BetTypes.HomeGroup1XRunsOE);
            MapIdToBaseId(BetTypes.Away2ndInnsScoreAtEndOfOverXOE, BetTypes.HomeGroup1XRunsOE);
            MapIdToBaseId(BetTypes.HomeFallOfWicketXOE, BetTypes.HomeGroup1XRunsOE);
            MapIdToBaseId(BetTypes.AwayFallOfWicketXOE, BetTypes.HomeGroup1XRunsOE);
            MapIdToBaseId(BetTypes.Home1stInnsFallOfWicketXOE, BetTypes.HomeGroup1XRunsOE);
            MapIdToBaseId(BetTypes.Away1stInnsFallOfWicketXOE, BetTypes.HomeGroup1XRunsOE);
            MapIdToBaseId(BetTypes.Home2ndInnsFallOfWicketXOE, BetTypes.HomeGroup1XRunsOE);
            MapIdToBaseId(BetTypes.Away2ndInnsFallOfWicketXOE, BetTypes.HomeGroup1XRunsOE);
            MapIdToBaseId(BetTypes.OddEvenDec, BetTypes.HomeInnsRunsOE);
        }

        private void MapForCricketAIOForFancy()
        {
            MapIdToBaseId(BetTypes.AwayInnsRunsFancy, BetTypes.HomeInnsRunsFancy);
            MapIdToBaseId(BetTypes.Home1stInnsRunsFancy, BetTypes.HomeInnsRunsFancy);
            MapIdToBaseId(BetTypes.Away1stInnsRunsFancy, BetTypes.HomeInnsRunsFancy);
            MapIdToBaseId(BetTypes.Home2ndInnsRunsFancy, BetTypes.HomeInnsRunsFancy);
            MapIdToBaseId(BetTypes.Away2ndInnsRunsFancy, BetTypes.HomeInnsRunsFancy);
            MapIdToBaseId(BetTypes.MatchFoursFancy, BetTypes.HomeInnsRunsFancy);
            MapIdToBaseId(BetTypes.MatchSixesFancy, BetTypes.HomeInnsRunsFancy);
            MapIdToBaseId(BetTypes.MatchWicketsFancy, BetTypes.HomeInnsRunsFancy);
            MapIdToBaseId(BetTypes.MatchExtrasFancy, BetTypes.HomeInnsRunsFancy);
            MapIdToBaseId(BetTypes.HighestIndividualScoreFancy, BetTypes.HomeInnsRunsFancy);
            MapIdToBaseId(BetTypes.HighestOverScoreFancy, BetTypes.HomeInnsRunsFancy);
            MapIdToBaseId(BetTypes.HomeInnsFoursFancy, BetTypes.HomeInnsRunsFancy);
            MapIdToBaseId(BetTypes.AwayInnsFoursFancy, BetTypes.HomeInnsRunsFancy);
            MapIdToBaseId(BetTypes.HomeInnsSixesFancy, BetTypes.HomeInnsRunsFancy);
            MapIdToBaseId(BetTypes.AwayInnsSixesFancy, BetTypes.HomeInnsRunsFancy);
            MapIdToBaseId(BetTypes.Fancy, BetTypes.HomeInnsRunsFancy);
            MapIdToBaseId(BetTypes.Home1stInnsGroup1XRunsFancy, BetTypes.HomeGroup1XRunsFancy);
            MapIdToBaseId(BetTypes.Home2ndInnsGroup1XRunsFancy, BetTypes.HomeGroup1XRunsFancy);
            MapIdToBaseId(BetTypes.AwayGroup1XRunsFancy, BetTypes.HomeGroup1XRunsFancy);
            MapIdToBaseId(BetTypes.Away1stInnsGroup1XRunsFancy, BetTypes.HomeGroup1XRunsFancy);
            MapIdToBaseId(BetTypes.Away2ndInnsGroup1XRunsFancy, BetTypes.HomeGroup1XRunsFancy);
            MapIdToBaseId(BetTypes.HomeScoreAtEndOfOverXFancy, BetTypes.HomeGroup1XRunsFancy);
            MapIdToBaseId(BetTypes.Home1stInnsScoreAtEndOfOverXFancy, BetTypes.HomeGroup1XRunsFancy);
            MapIdToBaseId(BetTypes.Home2ndInnsScoreAtEndOfOverXFancy, BetTypes.HomeGroup1XRunsFancy);
            MapIdToBaseId(BetTypes.AwayScoreAtEndOfOverXFancy, BetTypes.HomeGroup1XRunsFancy);
            MapIdToBaseId(BetTypes.Away1stInnsScoreAtEndOfOverXFancy, BetTypes.HomeGroup1XRunsFancy);
            MapIdToBaseId(BetTypes.Away2ndInnsScoreAtEndOfOverXFancy, BetTypes.HomeGroup1XRunsFancy);
            MapIdToBaseId(BetTypes.HomeFallOfWicketXFancy, BetTypes.HomeGroup1XRunsFancy);
            MapIdToBaseId(BetTypes.Home1stInnsFallOfWicketXFancy, BetTypes.HomeGroup1XRunsFancy);
            MapIdToBaseId(BetTypes.Home2ndInnsFallOfWicketXFancy, BetTypes.HomeGroup1XRunsFancy);
            MapIdToBaseId(BetTypes.AwayFallOfWicketXFancy, BetTypes.HomeGroup1XRunsFancy);
            MapIdToBaseId(BetTypes.Away1stInnsFallOfWicketXFancy, BetTypes.HomeGroup1XRunsFancy);
            MapIdToBaseId(BetTypes.Away2ndInnsFallOfWicketXFancy, BetTypes.HomeGroup1XRunsFancy);
        }
    }
}