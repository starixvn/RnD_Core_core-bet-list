namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class OddsMapper : BaseMapper
    {
        public OddsMapper()
        {
            MapHappy5LinkedGames();
        }

        private void MapHappy5LinkedGames()
        {
            for (var betTypeId = Happy5LinkedGamesBetTypes.OverUnder; betTypeId <= Happy5LinkedGamesBetTypes.HighestBetTypeIdInRange; betTypeId++)
            {
                if (betTypeId != Happy5LinkedGamesBetTypes.Soccer5)
                {
                    MapIdToBaseId(betTypeId, Happy5LinkedGamesBetTypes.OverUnder);
                }                
            }
        }
    }
}