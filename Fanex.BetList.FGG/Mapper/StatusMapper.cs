namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            for (var betType = FGGBetTypes.DragonTiger; betType <= FGGBetTypes.MaxBetypeRange; betType++)
            {
                MapIdToBaseId(betType, FGGBetTypes.Baccarat);
            }
        }
    }
}
