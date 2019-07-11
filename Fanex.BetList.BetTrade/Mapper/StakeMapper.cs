namespace Fanex.BetList.Core.Mapper
{
    using BetList.BetTrade.Constants;

    public class StakeMapper : BaseMapper
    {
        public StakeMapper()
        {
            MapIdToBaseId(BetTypes.Bought, BetTypes.Sold);
        }
    }
}