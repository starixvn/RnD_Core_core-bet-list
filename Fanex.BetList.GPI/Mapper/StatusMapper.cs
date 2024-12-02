namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            for (int betTypeId = GPIBetTypes.UFOFrenzy + 1; betTypeId < GPIBetTypes.HighestBetTypeIdInRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, GPIBetTypes.UFOFrenzy);
            }

            for (int betTypeId = GPILCBetTypes.NoCommisionBaccarat1 + 1; betTypeId < GPILCBetTypes.HighestBetTypeIdInRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, GPILCBetTypes.NoCommisionBaccarat1);
            }
        }
    }
}