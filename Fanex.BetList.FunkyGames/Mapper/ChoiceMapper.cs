namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            for (var betType = FunkyGamesBetTypes.SweetBombs + 1; betType <= FunkyGamesBetTypes.HighestBetTypeIdInRange; betType++)
            {
                MapIdToBaseId(betType, FunkyGamesBetTypes.SweetBombs);
            }
        }
    }
}