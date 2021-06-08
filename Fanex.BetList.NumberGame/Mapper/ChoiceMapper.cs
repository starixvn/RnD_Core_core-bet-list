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
            MapHappy5LinkedGames();
        }

        private void MapHappy5()
        {
            for (var betTypeId = Happy5BetTypes.OddEven; betTypeId <= Happy5BetTypes.HighestBetTypeIdInRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, Happy5BetTypes.BigSmall);
            }
        }

        private void MapHappy5LinkedGames()
        {
            for (var betTypeId = Happy5LinkedGamesBetTypes.OverUnder; betTypeId <= Happy5LinkedGamesBetTypes.HighestBetTypeIdInRange; betTypeId++)
            {
                if(betTypeId != Happy5LinkedGamesBetTypes.Soccer5)
                {
                    MapIdToBaseId(betTypeId, Happy5LinkedGamesBetTypes.OverUnder);
                }                    
            }
        }
    }
}