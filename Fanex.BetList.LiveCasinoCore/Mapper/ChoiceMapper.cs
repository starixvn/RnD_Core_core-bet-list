namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            for (int betTypeId = LCBetTypes.MicrogamingMin + 1; betTypeId <= LCBetTypes.MicrogamingMax; betTypeId++)
            {
                if (betTypeId != LCBetTypes.MicrogamingPromotion)
                {
                    MapIdToBaseId(betTypeId, LCBetTypes.MicrogamingMin);
                }
            }

            for(int betTypeId = LCBetTypes.PlaytechMin + 1; betTypeId <= LCBetTypes.PlaytechMax; betTypeId++)
            {
                MapIdToBaseId(betTypeId, LCBetTypes.PlaytechMin);
            }
        }
    }
}