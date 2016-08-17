namespace Fanex.BetList.Core.Mapper
{
    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            for (var betType = 2402; betType <= 2405; betType++)
            {
                MapIdToBaseId(betType, 2401);
            }
        }
    }
}