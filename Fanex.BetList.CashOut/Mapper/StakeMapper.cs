namespace Fanex.BetList.Core.Mapper
{
    using BetList.CashOut.Constants;

    public class StakeMapper : BaseMapper
    {
        public StakeMapper()
        {
            MapIdToBaseId(BetTypes.Bought, BetTypes.CashOut);
        }
    }
}