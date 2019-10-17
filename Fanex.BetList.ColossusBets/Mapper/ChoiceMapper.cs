namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            for (int betTypeId = ColossusBetsBetTypes.PoolBet; betTypeId <= ColossusBetsBetTypes.BonusPrize; betTypeId++)
            {
                MapIdToBaseId(betTypeId, ColossusBetsBetTypes.ColossusBets);
            }
        }
    }
}