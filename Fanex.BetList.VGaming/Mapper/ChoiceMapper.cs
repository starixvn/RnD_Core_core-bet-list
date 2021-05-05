namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            for (var betTypeId = VGamingBetTypes.MinRange + 1; betTypeId < VGamingBetTypes.JackpotContribution; betTypeId++)
            {
                MapIdToBaseId(betTypeId, VGamingBetTypes.MinRange);
            }
        }
    }
}