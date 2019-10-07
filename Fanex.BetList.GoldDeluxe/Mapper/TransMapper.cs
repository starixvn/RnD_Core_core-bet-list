namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class TransMapper : BaseMapper
    {
        public TransMapper()
        {
            for (int betTypeId = GoldDeluxeBetTypes.Roulette; betTypeId <= GoldDeluxeBetTypes.BidMe; betTypeId++)
            {
                MapIdToBaseId(betTypeId, GoldDeluxeBetTypes.Baccarat);
            }
        }
    }
}