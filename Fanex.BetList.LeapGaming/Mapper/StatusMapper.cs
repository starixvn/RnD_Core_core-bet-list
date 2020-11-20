namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            for (var betType = LeapGamingBetTypes.VirtualFootball; betType <= LeapGamingBetTypes.HighestBettype; betType++)
            {
                MapIdToBaseId(betType, LeapGamingBetTypes.VirtualFootball);
            }
        }
    }
}