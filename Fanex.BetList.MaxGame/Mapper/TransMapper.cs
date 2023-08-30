namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class TransMapper : BaseMapper
    {
        public TransMapper()
        {
            for (int betTypeId = MaxGameBetTypes.DiceWar + 1; betTypeId <= MaxGameBetTypes.HighestBetTypeIdInRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, MaxGameBetTypes.DiceWar);
            }
        }
    }
}
