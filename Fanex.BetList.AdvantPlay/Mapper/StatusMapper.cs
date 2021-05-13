namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            for (int betTypeId = AdvantPlayBetTypes.BattleOfHeroes + 1; betTypeId <= AdvantPlayBetTypes.HighestBetTypeIdInRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, AdvantPlayBetTypes.BattleOfHeroes);
            }
        }
    }
}