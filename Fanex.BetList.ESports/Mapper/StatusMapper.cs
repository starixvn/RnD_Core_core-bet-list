namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            for (int betTypeId = ESportsBetTypes.MapXTotalKillsHandicap; betTypeId <= ESportsBetTypes.MapXFirstTurretLocationKOG; betTypeId++)
            {
                MapIdToBaseId(betTypeId, ESportsBetTypes.MapXMoneyline);
            }
        }
    }
}