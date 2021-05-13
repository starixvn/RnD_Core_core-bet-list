namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            for (int betTypeId = AdvantPlayBetTypes.BattleOfHeroes + 1; betTypeId <= AdvantPlayBetTypes.HighestBetTypeIdInRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, AdvantPlayBetTypes.BattleOfHeroes);
            }
        }
    }
}