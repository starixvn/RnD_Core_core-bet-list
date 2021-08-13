namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class TransMapper : BaseMapper
    {
        public TransMapper()
        {
            for (int betTypeId = BitcoinBetTypes.OverUnder + 1; betTypeId <= BitcoinBetTypes.HighestBetTypeIdInRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, BitcoinBetTypes.OverUnder);
            }
        }
    }
}