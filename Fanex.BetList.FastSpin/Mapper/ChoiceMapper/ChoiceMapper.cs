namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            for (int betTypeId = FastSpinBetTypes.LowestBetTypeIdInRange + 1; betTypeId <= FastSpinBetTypes.HighestBetTypeIdInRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, FastSpinBetTypes.LowestBetTypeIdInRange);
            }
        }
    }
}