namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            MapIdToBaseId(AGCasinoBetTypes.Roulette, AGCasinoBetTypes.Baccarat);
            MapIdToBaseId(AGCasinoBetTypes.DragonTiger, AGCasinoBetTypes.Baccarat);
            MapIdToBaseId(AGCasinoBetTypes.SicBo, AGCasinoBetTypes.Baccarat);
        }
    }
}