namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            for (int betTypeId = MaxGameBetTypes.DiceWar + 1; betTypeId <= MaxGameBetTypes.HighestBetTypeIdInRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, MaxGameBetTypes.DiceWar);
            }
        }
    }
}