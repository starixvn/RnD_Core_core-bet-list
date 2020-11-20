namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class TransMapper : BaseMapper
    {
        public TransMapper()
        {
            for (var betType = LeapGamingBetTypes.VirtualFootball; betType <= LeapGamingBetTypes.HighestBettype; betType++)
            {
                MapIdToBaseId(betType, LeapGamingBetTypes.VirtualFootball);
            }
        }
    }
}
