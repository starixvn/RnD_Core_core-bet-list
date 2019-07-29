namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Utils;

    public class OddsMapper : BaseMapper
    {
        public OddsMapper()
        {
            ConvertByBetType.RegisterMultipleOddsTypes(MetricGamingBetTypes.GoalWithin60Seconds);
            ConvertByBetType.RegisterMultipleOddsTypes(MetricGamingBetTypes.NextSetPiece);
            ConvertByBetType.RegisterMultipleOddsTypes(MetricGamingBetTypes.EverySingleMinute);
            ConvertByBetType.RegisterMultipleOddsTypes(MetricGamingBetTypes.TimeOfPossession);
            ConvertByBetType.RegisterMultipleOddsTypes(MetricGamingBetTypes.FirstGoal);
        }
    }
}