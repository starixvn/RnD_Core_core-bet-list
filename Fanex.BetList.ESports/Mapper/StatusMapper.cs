namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            for (int betTypeId = ESportsBetTypes.MapXTotalKillsHandicap; betTypeId <= ESportsBetTypes.CorrectMapScore; betTypeId++)
            {
                MapIdToBaseId(betTypeId, ESportsBetTypes.MapXMoneyline);
            }
        }
    }
}