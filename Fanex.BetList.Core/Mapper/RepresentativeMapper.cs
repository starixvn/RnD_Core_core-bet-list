namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class RepresentativeMapper : BaseMapper
    {
        public RepresentativeMapper()
        {
            MapIdToBaseId(BetTypes.ExactFirstHalfGoals3rd, BetTypes.ExactFirstHalfGoals);
            MapIdToBaseId(BetTypes.ExactSecondHalfGoals3rd, BetTypes.ExactSecondHalfGoals);
        }
    }
}