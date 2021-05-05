namespace Fanex.BetList.Core.Builder.StakeBuilder
{
    using Fanex.BetList.Core.Entities;

    /// <summary>
    /// Jackpot Contribution
    /// </summary>
    public class Stake3898 : Stake1
    {
        private const string Hyphen = "-";

        protected override void BuildStake(ITicket ticket)
        {
            base.BuildStake(ticket);

            Template.stake = Hyphen;
        }
    }
}