namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class StakeMapper : BaseMapper
    {
        public StakeMapper()
        {
            MapIdToBaseId(CashOutBetTypes.Bought, CashOutBetTypes.CashOut);
        }
    }
}