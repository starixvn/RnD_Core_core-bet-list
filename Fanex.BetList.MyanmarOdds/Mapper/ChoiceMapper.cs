namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            MapIdToBaseId(MyanmarOddsBetTypes.FirstHalfHandicap, MyanmarOddsBetTypes.Handicap);
            MapIdToBaseId(MyanmarOddsBetTypes.FirstHalfOverUnder, MyanmarOddsBetTypes.OverUnder);
        }
    }
}