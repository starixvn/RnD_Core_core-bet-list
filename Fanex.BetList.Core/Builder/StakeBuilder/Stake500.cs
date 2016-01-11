namespace Fanex.BetList.Core.Builder.StakeBuilder
{
    using Fanex.BetList.Core.Entities;

    /// <summary>
    /// Charge Commission.
    /// </summary>
    public class Stake500 : Stake1
    {
        private const string DashStake = "-";

        protected override void BuildStake(ITicket ticket)
        {
            Template.stake = DashStake;
        }
    }
}