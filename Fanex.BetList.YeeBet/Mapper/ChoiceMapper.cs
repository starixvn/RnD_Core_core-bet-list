namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            for (var betType = YeeBetBetTypes.LowestBetTypeIdInRange + 1; betType <= YeeBetBetTypes.HighestBetTypeIdInRange; betType++)
            {
                MapIdToBaseId(betType, YeeBetBetTypes.LowestBetTypeIdInRange);
            }
        }
    }
}