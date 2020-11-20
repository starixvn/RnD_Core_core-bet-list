namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            for (var betType = LeapGamingBetTypes.VirtualFootball; betType <= LeapGamingBetTypes.HighestBettype; betType++)
            {
                MapIdToBaseId(betType, LeapGamingBetTypes.VirtualFootball);
            }
        }
    }
}