namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class OddsMapper : BaseMapper
    {
        public OddsMapper()
        {
            for (var betType = SAGamingBetTypes.DragonTiger; betType <= SAGamingBetTypes.Tips; betType++)
            {
                MapIdToBaseId(betType, SAGamingBetTypes.Baccarat);
            }
        }
    }
}