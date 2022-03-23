namespace Fanex.BetList.Core.Mapper
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Constants;

    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            MapForFastMarkets();
            MapForBasketBall();
            MapForBadminton();
            MapForCornerMatches();
            MapForPHAIO5();
            MapForCricket();
            MapForOthers();
        }

        private void MapForCricket()
        {
            var manOutTicket = new List<int>
            {
                BetTypes.HomeNextManOut,
                BetTypes.AwayNextManOut,
                BetTypes.Away1stInnsNextManOut,
                BetTypes.Away2ndInnsNextManOut,
                BetTypes.Home1stInnsNextManOut,
                BetTypes.Home2ndInnsNextManOut
            };

            var defaultStatusBetTypes = new List<int> {
                BetTypes.MostFours, BetTypes.MostSixes,
                BetTypes.MatchFours, BetTypes.MatchSixes, BetTypes.MatchRunOuts,
                BetTypes.MatchDotBalls,
                BetTypes.HomeFirstStInnsFours, BetTypes.AwayFirstStInnsFours, BetTypes.HomeSecondInnsFours, BetTypes.AwaySecondInnsFours,
                BetTypes.Home1stInnsSixes, BetTypes.Away1stInnsSixes, BetTypes.Home2ndInnsSixes, BetTypes.Away2ndInnsSixes,
                BetTypes.MatchFoursOE, BetTypes.MatchSixesOE, BetTypes.MatchRunOutsOE,
                BetTypes.MatchDotBallsOE,
                BetTypes.Home1stInnsFoursOE, BetTypes.Away1stInnsFoursOE, BetTypes.Home2ndInnsFoursOE, BetTypes.Away2ndInnsFoursOE,
                BetTypes.HomeInnsRunsFancy,BetTypes.AwayInnsRunsFancy, BetTypes.Home1stInnsRunsFancy, BetTypes.Away1stInnsRunsFancy, BetTypes.Home2ndInnsRunsFancy, BetTypes.Away2ndInnsRunsFancy,
                BetTypes.MatchFoursFancy, BetTypes.MatchSixesFancy, BetTypes.MatchWicketsFancy,
                BetTypes.MatchExtrasFancy,BetTypes.HighestIndividualScoreFancy, BetTypes.HighestOverScoreFancy, BetTypes.HomeInnsFoursFancy, BetTypes.AwayInnsFoursFancy,
                BetTypes.Home1stInnsFoursFancy, BetTypes.Away1stInnsFoursFancy, BetTypes.Home2ndInnsFoursFancy, BetTypes.Away2ndInnsFoursFancy,
                BetTypes.HomeInnsSixesFancy, BetTypes.AwayInnsSixesFancy, BetTypes.Home1stInnsSixesFancy, BetTypes.Away1stInnsSixesFancy, BetTypes.Home2ndInnsSixesFancy, BetTypes.Away2ndInnsSixesFancy,
                BetTypes.MostRunOuts,
                BetTypes.HomeTopBatsman, BetTypes.AwayTopBatsman, BetTypes.Home1stInnsTopBatsman, BetTypes.Away1stInnsTopBatsman, BetTypes.Home2ndInnsTopBatsman,
                BetTypes.Away2ndInnsTopBatsman, BetTypes.HomeTopBowler, BetTypes.AwayTopBowler, BetTypes.Home1stInnsTopBowler, BetTypes.Away1stInnsTopBowler,
                BetTypes.Home2ndInnsTopBowler, BetTypes.Away2ndInnsTopBowler,
                BetTypes.HomePrematchTopBatsman, BetTypes.AwayPrematchTopBatsman,
                BetTypes.Home1stInnsPrematchTopBatsman, BetTypes.Away1stInnsPrematchTopBatsman, BetTypes.HomePrematchTopBowler, BetTypes.AwayPrematchTopBowler,
                BetTypes.Home1stInnsPrematchTopBowler, BetTypes.Away1stInnsPrematchTopBowler,
                BetTypes.FirstInnsHighestIndividualScore, BetTypes.FirstInnsHighestOverScore,
                BetTypes.FirstInnsHighestOverScoreFancy, BetTypes.FirstInnsHighestIndividualScoreFancy,
                BetTypes.FirstInnsHighestOpeningPartnership, BetTypes.FirstInnsHighestScoreinOver1
            };

            for (var betType = BetTypes.SuperOverWinner; betType <= BetTypes.HighestOfCricket; betType++)
            {
                if(manOutTicket.Contains(betType))
                {
                    MapIdToBaseId(betType, BetTypes.HomeNextManOut);
                }
                else
                {
                    if (!defaultStatusBetTypes.Contains(betType))
                    {
                        MapIdToBaseId(betType, BetTypes.SetXWinner);
                    }
                }
            }            
        }

        private void MapForFastMarkets()
        {
            for (var betType = BetTypes.Next5Minutes; betType <= BetTypes.WhichCombinationWillHappenFirstInNext5Mins; betType++)
            {
                MapIdToBaseId(betType, BetTypes.Next1Minute);
            }

            MapIdToBaseId(BetTypes.TimeMachineHDP, BetTypes.TimeMachineOverUnder);
        }

        private void MapForBasketBall()
        {
            MapIdToBaseId(BetTypes.SecondHalfRaceToXPoints, BetTypes.FirstHalfRaceToXPoints);
            MapIdToBaseId(BetTypes.QuarterXHandicap, BetTypes.FirstHalfRaceToXPoints);
            MapIdToBaseId(BetTypes.QuarterXOverUnder, BetTypes.FirstHalfRaceToXPoints);
            MapIdToBaseId(BetTypes.QuarterXOddEven, BetTypes.FirstHalfRaceToXPoints);
            MapIdToBaseId(BetTypes.QuarterXMoneyline, BetTypes.FirstHalfRaceToXPoints);
            MapIdToBaseId(BetTypes.QuarterXRaceToYPoints, BetTypes.FirstHalfRaceToXPoints);
            MapIdToBaseId(BetTypes.QuarterXWinningMargin7Way, BetTypes.FirstHalfRaceToXPoints);
            MapIdToBaseId(BetTypes.QuarterXHomeTeamOverUnder, BetTypes.FirstHalfRaceToXPoints);
            MapIdToBaseId(BetTypes.QuarterXAwayTeamOverUnder, BetTypes.FirstHalfRaceToXPoints);
            MapIdToBaseId(BetTypes.QuarterXWhichTeamToScoreTheLastBasket, BetTypes.FirstHalfRaceToXPoints);

            MapIdToBaseId(BetTypes.QuarterXLastDigitScore, BetTypes.FirstHalfRaceToXPoints);
            MapIdToBaseId(BetTypes.QuarterXHomeTeamLastDigitScore, BetTypes.FirstHalfRaceToXPoints);
            MapIdToBaseId(BetTypes.QuarterXAwayTeamLastDigitScore, BetTypes.FirstHalfRaceToXPoints);
            MapIdToBaseId(BetTypes.QuarterX1X2, BetTypes.FirstHalfRaceToXPoints);
            MapIdToBaseId(BetTypes.QuarterXDoubleChance, BetTypes.FirstHalfRaceToXPoints);
        }

        private void MapForBadminton()
        {
            MapIdToBaseId(BetTypes.GameXRaceToYPoints, BetTypes.FirstHalfRaceToXPoints);
            MapIdToBaseId(BetTypes.GameXPointYWinner, BetTypes.FirstHalfRaceToXPoints);
        }

        private void MapForPHAIO5()
        {
            MapIdToBaseId(BetTypes.AwayPenaltyShootoutRoundX, BetTypes.ExtraTimeNextGoalMethod);
            MapIdToBaseId(BetTypes.HomePenaltyShootoutRoundX, BetTypes.ExtraTimeNextGoalMethod);
            MapIdToBaseId(BetTypes.PenaltyShootoutWoodworkRoundX, BetTypes.ExtraTimeNextGoalMethod);
            MapIdToBaseId(BetTypes.PenaltyShootoutBothOneNeitherRoundX, BetTypes.ExtraTimeNextGoalMethod);
            MapIdToBaseId(BetTypes.PenaltyShootout1X2RoundX, BetTypes.ExtraTimeNextGoalMethod);
        }

        private void MapForOthers()
        {
            // Parlay
            MapIdToBaseId(BetTypes.SabaLoyaltyPointsExchange, BetTypes.SabaLuckySpin);
            MapIdToBaseId(BetTypes.SabaRedEnvelope, BetTypes.SabaLuckySpin);
        }

        private void MapForCornerMatches()
        {
            MapIdToBaseId(BetTypes.FirstHalfCornersRace, BetTypes.CornersRace);
        }
    }
}