namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            for (var betType = PragmaticLiveCasinoBetTypes.BaccaratA + 1; betType <= PragmaticLiveCasinoBetTypes.MegaSicbo; betType++)
            {
                MapIdToBaseId(betType, PragmaticLiveCasinoBetTypes.BaccaratA);
            }
        }
    }
}