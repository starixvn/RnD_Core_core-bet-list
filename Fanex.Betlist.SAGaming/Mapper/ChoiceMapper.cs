namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            for (var betType = SAGamingBetTypes.DragonTiger; betType <= SAGamingBetTypes.MoneyWheel; betType++)
            {
                MapIdToBaseId(betType, SAGamingBetTypes.Baccarat);
            }
        }
    }
}