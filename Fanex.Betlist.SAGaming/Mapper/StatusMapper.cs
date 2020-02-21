namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            for (var betType = SAGamingBetTypes.DragonTiger; betType <= SAGamingBetTypes.MoneyWheel; betType++)
            {
                MapIdToBaseId(betType, SAGamingBetTypes.Baccarat);
            }
        }
    }
}