namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            MapIdToBaseId(NumberGameBetTypes.LastBallOverUnder, NumberGameBetTypes.FirstBallOverUnder);
            MapIdToBaseId(NumberGameBetTypes.LastBallOddEven, NumberGameBetTypes.FirstBallOddEven);

            MapHappy5();
        }

        private void MapHappy5()
        {
            for (var betTypeId = Happy5BetTypes.OddEven; betTypeId <= Happy5BetTypes.HighestBetTypeIdInRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, Happy5BetTypes.BigSmall);
            }
        }
    }
}