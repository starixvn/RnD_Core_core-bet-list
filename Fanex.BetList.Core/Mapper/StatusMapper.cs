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
        }

        private void MapForBadminton()
        {
            MapIdToBaseId(BetTypes.GameXRaceToYPoints, BetTypes.FirstHalfRaceToXPoints);
            MapIdToBaseId(BetTypes.GameXPointYWinner, BetTypes.FirstHalfRaceToXPoints);
        }

        private void MapForOthers()
        {
            // Parlay
            MapIdToBaseId(BetTypes.SystemParlay, BetTypes.MixParlay);
        }

        private void MapForCornerMatches()
        {
            MapIdToBaseId(BetTypes.FirstHalfCornersRace, BetTypes.CornersRace);
        }
    }
}