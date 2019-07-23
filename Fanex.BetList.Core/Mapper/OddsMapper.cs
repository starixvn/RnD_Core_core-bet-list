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
    }
}