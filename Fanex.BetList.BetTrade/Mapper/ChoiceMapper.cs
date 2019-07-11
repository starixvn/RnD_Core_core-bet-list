namespace Fanex.BetList.Core.Mapper
{
    using BetList.BetTrade.Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            MapIdToBaseId(BetTypes.Bought, BetTypes.Sold);
        }
    }
}