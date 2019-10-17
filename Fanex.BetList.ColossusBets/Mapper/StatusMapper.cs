namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            for (int betTypeId = ColossusBetsBetTypes.PoolBet; betTypeId <= ColossusBetsBetTypes.BonusPrize; betTypeId++)
            {
                MapIdToBaseId(betTypeId, ColossusBetsBetTypes.ColossusBets);
            }
        }
    }
}