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
        }
    }
}
