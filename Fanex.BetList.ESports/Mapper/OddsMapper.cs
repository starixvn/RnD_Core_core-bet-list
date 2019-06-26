namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Utils;
    using Fanex.BetList.ESports.Constants;

    public class OddsMapper : BaseMapper
    {
        public OddsMapper()
        {
            RegisterMultipleOddsTypes();
        }

        private static void RegisterMultipleOddsTypes()
        {
            for (int betTypeId = BetTypes.MapXMoneyline; betTypeId <= BetTypes.MapXTotalOverlordsOddEven; betTypeId++)
            {
                ConvertByBetType.RegisterMultipleOddsTypes(betTypeId);
            }
        }
    }
}