namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class StakeMapper : BaseMapper
    {
        public StakeMapper()
        {
            MapIdToBaseId(PlaytechBetTypes.JackpotPrize, PlaytechBetTypes.JackpotContribution);
        }
    }
}