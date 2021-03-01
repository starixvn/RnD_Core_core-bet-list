namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            for (int betTypeId = HabaneroBetTypes.TabernaDeLosMuertos + 1; betTypeId <= HabaneroBetTypes.HighestBetTypeIdInRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, HabaneroBetTypes.TabernaDeLosMuertos);
            }
        }
    }
}