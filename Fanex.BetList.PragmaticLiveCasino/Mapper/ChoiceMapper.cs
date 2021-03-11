namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            for (var betType = PragmaticLiveCasinoBetTypes.BaccaratA + 1; betType <= PragmaticLiveCasinoBetTypes.HighestBetTypeIdInRange; betType++)
            {
                MapIdToBaseId(betType, PragmaticLiveCasinoBetTypes.BaccaratA);
            }
        }
    }
}