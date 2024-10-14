namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            for (var betTypeId = VGamingRange1BetTypes.MinRange + 1; betTypeId < VGamingRange1BetTypes.JackpotContribution; betTypeId++)
            {
                MapIdToBaseId(betTypeId, VGamingRange1BetTypes.MinRange);
            }

            for (var betTypeId = VGamingRange2BetTypes.MinRange; betTypeId <= VGamingRange2BetTypes.MaxRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, VGamingRange1BetTypes.MinRange);
            }
        }
    }
}