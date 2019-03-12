namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Fanex.BetList.Core.Entities;

    /// <summary>
    /// Bet Builder
    /// </summary>
    public class Status38 : Status1
    {
        protected override bool DetectAbnormalBet(ITicket ticket) => false;
    }
}