namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class OddsMapper : BaseMapper
    {
        public OddsMapper()
        {
            for (var betType = FGGBetTypes.DragonTiger; betType <= FGGBetTypes.MaxBetypeRange; betType++)
            {
                MapIdToBaseId(betType, FGGBetTypes.Baccarat);
            }
        }

    }
}
