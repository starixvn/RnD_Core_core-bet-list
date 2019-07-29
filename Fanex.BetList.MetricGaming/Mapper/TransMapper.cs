namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class TransMapper : BaseMapper
    {
        public TransMapper()
        {
            MapIdToBaseId(MetricGamingBetTypes.NextSetPiece, MetricGamingBetTypes.GoalWithin60Seconds);
            MapIdToBaseId(MetricGamingBetTypes.EverySingleMinute, MetricGamingBetTypes.GoalWithin60Seconds);
            MapIdToBaseId(MetricGamingBetTypes.TimeOfPossession, MetricGamingBetTypes.GoalWithin60Seconds);
            MapIdToBaseId(MetricGamingBetTypes.FirstGoal, MetricGamingBetTypes.GoalWithin60Seconds);
        }
    }
}