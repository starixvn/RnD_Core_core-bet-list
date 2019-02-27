namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Entities;

    /// <summary>
    /// Metric Gaming Super Live
    /// </summary>
    public class Status801 : Status1
    {
        protected override bool DetectAbnormalBet(ITicket ticket) => false;
    }
}