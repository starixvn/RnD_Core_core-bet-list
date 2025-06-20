namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {

            for (int betTypeId = LCBetTypes.MicrogamingMin + 1; betTypeId <= LCBetTypes.MicrogamingMax; betTypeId++)
            {
                if (betTypeId == LCBetTypes.MicrogamingPromotion)
                {
                    MapIdToBaseId(LCBetTypes.MicrogamingPromotion, BetTypes.SabaLuckySpin);
                }
                else
                {
                    MapIdToBaseId(betTypeId, LCBetTypes.MicrogamingMin);
                }
            }

            for (int betTypeId = LCBetTypes.PlaytechMin + 1; betTypeId < LCBetTypes.PlaytechJackpotContribution; betTypeId++)
            {
                MapIdToBaseId(betTypeId, LCBetTypes.PlaytechMin);
            }

            for (int betTypeId = LCBetTypes.WCasinoMin + 1; betTypeId <= LCBetTypes.WCasinoMax; betTypeId++)
            {
                MapIdToBaseId(betTypeId, LCBetTypes.WCasinoMin);
            }
        }
    }
}