using Fanex.BetList.Core.Constants;

namespace Fanex.BetList.Core.Mapper
{
    public class TransMapper : BaseMapper
    {
        public TransMapper()
        {
            for (var betTypeId = VGamingBetTypes.MinRange + 1; betTypeId < VGamingBetTypes.JackpotContribution; betTypeId++)
            {
                MapIdToBaseId(betTypeId, VGamingBetTypes.MinRange);
            }
        }
    }
}
