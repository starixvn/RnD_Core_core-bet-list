namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class TransMapper : BaseMapper
    {
        public TransMapper()
        {
            MapHappy5LinkedGames();
        }

        private void MapHappy5LinkedGames()
        {
            for (var betTypeId = Happy5LinkedGamesBetTypes.OverUnder; betTypeId <= Happy5LinkedGamesBetTypes.HighestBetTypeIdInRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, Happy5LinkedGamesBetTypes.OverUnder);
            }

            for (var betTypeId = NumberGameBetTypes.LastBallOverUnder; betTypeId <= NumberGameBetTypes.NextRedBlue; betTypeId++)
            {
                MapIdToBaseId(betTypeId, NumberGameBetTypes.FirstBallOverUnder);
            }

            for (var betTypeId = Happy5BetTypes.BigSmall; betTypeId <= Happy5BetTypes.HighestBetTypeIdInRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, NumberGameBetTypes.FirstBallOverUnder);
            }
        }
    }
}
