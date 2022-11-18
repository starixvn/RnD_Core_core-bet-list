namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            for (var betTypeId = PlayerTipsBetTypes.PlayerTips + 1; betTypeId <= PlayerTipsBetTypes.HighestBetTypeIdInRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, PlayerTipsBetTypes.PlayerTips);
            }
        }
    }
}