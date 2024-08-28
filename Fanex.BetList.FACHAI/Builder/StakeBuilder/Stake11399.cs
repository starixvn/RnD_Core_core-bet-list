namespace Fanex.BetList.Core.Builder.StakeBuilder
{
    using Fanex.BetList.Core.Entities;

    public class Stake11399 : Stake1
    {
        private const string Hyphen = "-";

        protected override void BuildStake(ITicket ticket)
        {
            Template.stake = Hyphen;
        }
    }
}