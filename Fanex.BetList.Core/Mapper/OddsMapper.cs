namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class OddsMapper : BaseMapper
    {
        public OddsMapper()
        {
            MapForScoreBox();
        }

        private void MapForScoreBox()
        {
            MapIdToBaseId(BetTypes.ScoreBoxOverUnder, BetTypes.ScoreBoxHandicap);
        }
    }
}