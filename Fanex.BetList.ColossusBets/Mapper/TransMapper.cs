namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class TransMapper : BaseMapper
    {
        public TransMapper()
        {
            for (int betTypeId = ColossusBetsBetTypes.PoolBet; betTypeId <= ColossusBetsBetTypes.BonusPrize; betTypeId++)
            {
                MapIdToBaseId(betTypeId, ColossusBetsBetTypes.ColossusBets);
            }
        }
    }
}