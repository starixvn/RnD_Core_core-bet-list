namespace Fanex.BetList.Core.Mapper
{
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
            MapForOthers();
        }

        private void MapForFastMarkets()
        {
            for (var betType = BetTypes.Next5Minutes; betType <= BetTypes.WhichCombinationWillHappenFirstInNext5Mins; betType++)
            {
                MapIdToBaseId(betType, BetTypes.Next1Minute);
            }
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
            MapIdToBaseId(BetTypes.SystemParlay, BetTypes.MixParlay);
            MapIdToBaseId(BetTypes.SabaLoyaltyPointsExchange, BetTypes.SabaLuckySpin);
            MapIdToBaseId(BetTypes.SabaRedEnvelope, BetTypes.SabaLuckySpin);
        }

        private void MapForCornerMatches()
        {
            MapIdToBaseId(BetTypes.FirstHalfCornersRace, BetTypes.CornersRace);
        }
    }
}