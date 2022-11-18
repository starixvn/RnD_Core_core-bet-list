namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            for (var id = PlayerTipsBetTypes.PlayerTips; id <= PlayerTipsBetTypes.HighestBetTypeIdInRange; id++)
            {
                MapIdToBaseId(id, BetTypes.SabaLuckySpin);
            }
        }
    }
}