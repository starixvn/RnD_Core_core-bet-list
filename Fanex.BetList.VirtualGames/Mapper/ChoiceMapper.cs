namespace Fanex.BetList.Core.Mapper
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            var noTableGame = new List<int>
            {
                VirtualGamesBetTypes.SoccerMines,
                VirtualGamesBetTypes.BasketballMines,
                VirtualGamesBetTypes.CricketMines,
                VirtualGamesBetTypes.SoccerTowers,
                VirtualGamesBetTypes.BasketballTowers,
                VirtualGamesBetTypes.CricketTowers
            };
            for (var betTypeId = VirtualGamesBetTypes.XocDia; betTypeId <= VirtualGamesBetTypes.MaxRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, noTableGame.Contains(betTypeId) ? VirtualGamesBetTypes.SoccerMines : VirtualGamesBetTypes.Baccarat);
            }
        }
    }
}