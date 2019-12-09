namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class TransMapper : BaseMapper
    {
        public TransMapper()
        {
            for (var betTypeId = VirtualGamesBetTypes.XocDia; betTypeId <= VirtualGamesBetTypes.MaxRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, VirtualGamesBetTypes.Baccarat);
            }
        }
    }
}