namespace Fanex.BetList.Core.Mapper
{
    using Constants;
    using Utils;

    public class OddsMapper : BaseMapper
    {
        public OddsMapper()
        {
            RegisterMultipleOddsTypes();
        }

        private static void RegisterMultipleOddsTypes()
        {
            for (int betTypeId = ESportsBetTypes.MapXMoneyline; betTypeId <= ESportsBetTypes.MapXFirstTurretLocationKOG; betTypeId++)
            {
                if(betTypeId != ESportsBetTypes.CorrectMapScore)
                {
                    ConvertByBetType.RegisterMultipleOddsTypes(betTypeId);
                }                
            }
        }
    }
}