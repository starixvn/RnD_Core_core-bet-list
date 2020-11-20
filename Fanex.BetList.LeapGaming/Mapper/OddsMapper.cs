namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class OddsMapper : BaseMapper
    {
        public OddsMapper()
        {
            for (var betType = LeapGamingBetTypes.VirtualFootball; betType <= LeapGamingBetTypes.HighestBettype; betType++)
            {
                MapIdToBaseId(betType, LeapGamingBetTypes.VirtualFootball);
            }
        }
    }
}