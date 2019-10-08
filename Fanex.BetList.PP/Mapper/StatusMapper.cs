namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            for (int betTypeId = PragmaticPlayBetTypes.BaseBetTypeId + 1; betTypeId < PragmaticPlayBetTypes.HighestBetTypeIdInRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, PragmaticPlayBetTypes.BaseBetTypeId);
            }
        }
    }
}