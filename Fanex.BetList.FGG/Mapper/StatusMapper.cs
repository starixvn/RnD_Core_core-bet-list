namespace Fanex.BetList.FGG.Mapper
{
    using Fanex.BetList.Core.Mapper;
    using Fanex.BetList.FGG.Constants;

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
