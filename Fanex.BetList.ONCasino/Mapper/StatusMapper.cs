namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            for (int betTypeId = ONBetTypes.BaseBetTypeId + 1; betTypeId <= ONBetTypes.HighestBetTypeId; betTypeId++)
            {
                MapIdToBaseId(betTypeId, ONBetTypes.BaseBetTypeId);
            }
        }
    }
}