namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            for (int betTypeId = WEBetTypes.BaseBetTypeId + 1; betTypeId <= WEBetTypes.HighestBetTypeId; betTypeId++)
            {
                MapIdToBaseId(betTypeId, WEBetTypes.BaseBetTypeId);
            }
        }
    }
}