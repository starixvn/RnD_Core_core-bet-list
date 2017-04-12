namespace Fanex.BetList.Core.Mapper
{
    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            MapForFastMarkets();
            MapForBasketBallAIO();
        }

        private void MapForFastMarkets()
        {
            for (var betType = 222; betType <= 225; betType++)
            {
                MapIdToBaseId(betType, 221);
            }
        }

        private void MapForBasketBallAIO()
        {
            // [607] 2H Race to X Points
            MapIdToBaseId(607, 606);

            // [609] Quarter X Handicap
            MapIdToBaseId(609, 606);

            // [610] Quarter X Over/Under
            MapIdToBaseId(610, 606);

            // [611] Quarter X Odd/Even
            MapIdToBaseId(611, 606);

            // [612] Quarter X Moneyline
            MapIdToBaseId(612, 606);

            // [613] Quarter X Race to Y Points
            MapIdToBaseId(613, 606);

            // [614] Quarter X Winning Margin 7 Way
            MapIdToBaseId(614, 606);

            // [615] Quarter X Home Team Over/Under
            MapIdToBaseId(615, 606);

            // [616] Quarter X Away Team Over/Under
            MapIdToBaseId(616, 606);

            // [617] Quarter X Which team to score the last basket
            MapIdToBaseId(617, 606);
        }
    }
}