namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class StakeMapper : BaseMapper
    {
        public StakeMapper()
        {
            MapIdToBaseId(BetTradeBetTypes.Bought, BetTradeBetTypes.Sold);
        }
    }
}