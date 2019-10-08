namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            for (int betTypeId = PragmaticPlayBetTypes.BaseBetTypeId + 1; betTypeId < PragmaticPlayBetTypes.HighestBetTypeIdInRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, PragmaticPlayBetTypes.BaseBetTypeId);
            }
        }
    }
}