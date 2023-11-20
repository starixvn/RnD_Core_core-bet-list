namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class OddsMapper : BaseMapper
    {
        public OddsMapper()
        {
            for (int betTypeId = WEBetTypes.BaseBetTypeId + 1; betTypeId <= WEBetTypes.HighestBetTypeId; betTypeId++)
            {
                MapIdToBaseId(betTypeId, WEBetTypes.BaseBetTypeId);
            }
        }
    }
}