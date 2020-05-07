namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class OddsMapper : BaseMapper
    {
        public OddsMapper()
        {
            for (var betTypeId = VirtualGamesBetTypes.XocDia; betTypeId <= VirtualGamesBetTypes.MaxRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, VirtualGamesBetTypes.Baccarat);
            }
        }
    }
}