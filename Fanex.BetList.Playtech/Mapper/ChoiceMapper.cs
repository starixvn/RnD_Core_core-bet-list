namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            for (int betTypeId = PlaytechBetTypes.BuffaloBlitz + 1; betTypeId < PlaytechBetTypes.JackpotContribution; betTypeId++)
            {
                MapIdToBaseId(betTypeId, PlaytechBetTypes.BuffaloBlitz);
            }
        }
    }
}