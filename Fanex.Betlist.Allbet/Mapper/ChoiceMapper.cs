namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            for (var betType = AllbetBetTypes.VipBaccarat; betType < AllbetBetTypes.Promotion; betType++)
            {
                MapIdToBaseId(betType, AllbetBetTypes.Baccarat);
            }
        }
    }
}