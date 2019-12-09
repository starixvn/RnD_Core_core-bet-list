namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            for (var betTypeId = VirtualGamesBetTypes.XocDia; betTypeId <= VirtualGamesBetTypes.MaxRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, VirtualGamesBetTypes.Baccarat);
            }
        }
    }
}