namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            for (var betType = PragmaticLiveCasinoBetTypes.BaccaratA + 1; betType <= PragmaticLiveCasinoBetTypes.HighestBetTypeIdInRange; betType++)
            {
                MapIdToBaseId(betType, PragmaticLiveCasinoBetTypes.BaccaratA);
            }
        }
    }
}