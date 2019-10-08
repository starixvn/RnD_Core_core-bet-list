namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            MapIdToBaseId(AGCasinoBetTypes.DragonTiger, AGCasinoBetTypes.Baccarat);
            MapIdToBaseId(AGCasinoBetTypes.SicBo, AGCasinoBetTypes.Baccarat);
        }
    }
}