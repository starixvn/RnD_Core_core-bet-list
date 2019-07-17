namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class OddsMapper : BaseMapper
    {
        public OddsMapper()
        {
            MapIdToBaseId(CashOutBetTypes.Bought, CashOutBetTypes.CashOut);
        }
    }
}