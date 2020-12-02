namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            for (var betType = GDGBetTypes.Baccarat + 1; betType <= GDGBetTypes.HighestBetTypeIdInRange; betType++)
            {
                MapIdToBaseId(betType, GDGBetTypes.Baccarat);
            }
        }
    }
}