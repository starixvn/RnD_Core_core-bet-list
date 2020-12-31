namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            for (int betTypeId = MaxGameBetTypes.DiceWar + 1; betTypeId <= MaxGameBetTypes.HighestBetTypeIdInRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, MaxGameBetTypes.DiceWar);
            }
        }
    }
}