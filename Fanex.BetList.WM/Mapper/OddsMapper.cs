namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class OddsMapper : BaseMapper
    {
        public OddsMapper()
        {
            for (var betType = WMBetTypes.Baccarat + 1; betType <= WMBetTypes.HighestBetTypeIdInRange; betType++)
            {
                if(betType != WMBetTypes.SicBo && betType != WMBetTypes.FishPrawnCrab){
                    MapIdToBaseId(betType, WMBetTypes.Baccarat);
                }
            }
        }
    }
}