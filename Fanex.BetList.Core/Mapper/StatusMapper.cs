namespace Fanex.BetList.Core.Mapper
{
    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            MapForFastMarkets();
        }

        private void MapForFastMarkets()
        {
            for (var betType = 222; betType <= 224; betType++)
            {
                MapIdToBaseId(betType, 221);
            }
        }
    }
}