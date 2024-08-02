namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            for (int betTypeId = NextspinBettypes.AztecGoldTreasure + 1; betTypeId <= NextspinBettypes.HighestBetTypeIdInRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, NextspinBettypes.AztecGoldTreasure);                 
            }
        }
    }
}
