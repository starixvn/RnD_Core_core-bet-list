namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.ESports.Constants;

    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            for (int betTypeId = BetTypes.MapXTotalKillsHandicap; betTypeId <= BetTypes.MapXTotalOverlordsOddEven; betTypeId++)
            {
                MapIdToBaseId(betTypeId, BetTypes.MapXMoneyline);
            }
        }
    }
}