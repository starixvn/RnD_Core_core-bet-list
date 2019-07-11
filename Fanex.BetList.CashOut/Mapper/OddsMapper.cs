namespace Fanex.BetList.Core.Mapper
{
    using BetList.CashOut.Constants;

    public class OddsMapper : BaseMapper
    {
        public OddsMapper()
        {
            MapIdToBaseId(BetTypes.Bought, BetTypes.CashOut);
        }
    }
}