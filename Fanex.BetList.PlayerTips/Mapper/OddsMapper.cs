namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class OddsMapper : BaseMapper
    {
        public OddsMapper()
        {
            for (var id = PlayerTipsBetTypes.PlayerTips; id <= PlayerTipsBetTypes.HighestBetTypeIdInRange; id++)
            {
                MapIdToBaseId(id, BetTypes.SystemParlay);
            }
        }
    }
}