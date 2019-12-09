namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            for (var betTypeId = VirtualGamesBetTypes.XocDia; betTypeId <= VirtualGamesBetTypes.MaxRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, VirtualGamesBetTypes.Baccarat);
            }
        }
    }
}