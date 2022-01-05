namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class TransMapper : BaseMapper
    {
        public TransMapper()
        {
            for (int betTypeId = BetRadarVirtualSoccerBetTypes.FullTime1X2 + 1; betTypeId <= BetRadarVirtualSoccerBetTypes.HighestBetTypeIdInRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, BetRadarVirtualSoccerBetTypes.FullTime1X2);
            }

            for (int betTypeId = BetRadarVirtualBasketballBetTypes.MatchWinner; betTypeId <= BetRadarVirtualBasketballBetTypes.HighestBetTypeIdInRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, BetRadarVirtualSoccerBetTypes.FullTime1X2);
            }

            for (int betTypeId = InspireVirtualSoccerBetTypes.LowestBetTypeIdInRange + 1; betTypeId <= InspireVirtualRacingBetTypes.HighestBetTypeIdInRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, InspireVirtualSoccerBetTypes.LowestBetTypeIdInRange);
            }
        }
    }
}
