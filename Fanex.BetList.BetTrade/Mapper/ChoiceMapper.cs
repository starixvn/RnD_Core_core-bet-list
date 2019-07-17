namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            MapIdToBaseId(BetTradeBetTypes.Bought, BetTradeBetTypes.Sold);
        }
    }
}