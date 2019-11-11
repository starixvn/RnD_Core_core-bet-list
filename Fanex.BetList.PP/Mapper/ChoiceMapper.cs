namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            for (int betTypeId = PragmaticPlayBetTypes.Dragons888 + 1; betTypeId < PragmaticPlayBetTypes.PromotionPrize; betTypeId++)
            {
                MapIdToBaseId(betTypeId, PragmaticPlayBetTypes.Dragons888);
            }
        }
    }
}