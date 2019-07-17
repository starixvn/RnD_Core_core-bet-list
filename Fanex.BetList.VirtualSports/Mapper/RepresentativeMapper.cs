namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class RepresentativeMapper : BaseMapper
    {
#pragma warning disable S4142 // Duplicate values should not be passed as arguments

        public RepresentativeMapper()
        {
            MapIdToBaseId(InspireVirtualSoccerBetTypes.OverUnder25Goals, InspireVirtualSoccerBetTypes.OverUnder25Goals);
            MapIdToBaseId(InspireVirtualSoccerBetTypes.FulTime1X2, InspireVirtualSoccerBetTypes.FulTime1X2);
        }

#pragma warning restore S4142 // Duplicate values should not be passed as arguments
    }
}