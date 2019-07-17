namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class RepresentativeMapper : BaseMapper
    {
#pragma warning disable S4142 // Duplicate values should not be passed as arguments

        public RepresentativeMapper()
        {
            MapIdToBaseId(MyanmarOddsBetTypes.Handicap, MyanmarOddsBetTypes.Handicap);
            MapIdToBaseId(MyanmarOddsBetTypes.OverUnder, MyanmarOddsBetTypes.OverUnder);
            MapIdToBaseId(MyanmarOddsBetTypes.FirstHalfHandicap, MyanmarOddsBetTypes.FirstHalfHandicap);
            MapIdToBaseId(MyanmarOddsBetTypes.FirstHalfOverUnder, MyanmarOddsBetTypes.FirstHalfOverUnder);
        }

#pragma warning restore S4142 // Duplicate values should not be passed as arguments
    }
}