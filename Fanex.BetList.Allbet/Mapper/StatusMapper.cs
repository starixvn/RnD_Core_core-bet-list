namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            for (var betType = AllbetBetTypes.VipBaccarat; betType < AllbetBetTypes.Promotion; betType++)
            {
                MapIdToBaseId(betType, AllbetBetTypes.Baccarat);
            }
        }
    }
}