namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class OddsMapper : BaseMapper
    {
        public OddsMapper()
        {
            MapIdToBaseId(AllbetBetTypes.PokDeng, BetTypes.SystemParlay);
        }
    }
}