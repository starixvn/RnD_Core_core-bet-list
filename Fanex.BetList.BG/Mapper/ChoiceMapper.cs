namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            for (var betType = BGBetTypes.Baccarat + 1; betType <= BGBetTypes.HighestBetTypeIdInRange; betType++)
            {
                MapIdToBaseId(betType, BGBetTypes.Baccarat);
            }
        }
    }
}