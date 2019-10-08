namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class OddsMapper : BaseMapper
    {
        public OddsMapper()
        {
            for (int betTypeId = PragmaticPlayBetTypes.BaseBetTypeId + 1; betTypeId <= PragmaticPlayBetTypes.HighestBetTypeIdInRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, PragmaticPlayBetTypes.BaseBetTypeId);
            }
        }
    }
}