namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            for (int betTypeId = GPIBetTypes.UFOFrenzy + 1; betTypeId < GPIBetTypes.HighestBetTypeIdInRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, GPIBetTypes.UFOFrenzy);
            }
        }
    }
}