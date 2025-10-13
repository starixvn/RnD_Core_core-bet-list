namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            for (var betType = SexyGamingBetTypes.Baccarat + 1; betType < SexyGamingBetTypes.Tips; betType++)
            {
                MapIdToBaseId(betType, SexyGamingBetTypes.Baccarat);
            }

            MapIdToBaseId(SexyGamingBetTypes.Tips, SexyGamingBetTypes.LuckyDraw);
        }
    }
}