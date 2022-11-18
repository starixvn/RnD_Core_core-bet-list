namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class TransMapper : BaseMapper
    {
        public TransMapper()
        {
            for (var id = PlayerTipsBetTypes.PlayerTips + 1; id <= PlayerTipsBetTypes.HighestBetTypeIdInRange; id++)
            {
                MapIdToBaseId(id, PlayerTipsBetTypes.PlayerTips);
            }
        }
    }
}