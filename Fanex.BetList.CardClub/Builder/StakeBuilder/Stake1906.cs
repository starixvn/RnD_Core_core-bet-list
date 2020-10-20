namespace Fanex.BetList.Core.Builder.StakeBuilder
{
    using Fanex.BetList.Core.Entities;

    public class Stake1906 : Stake1
    {
        protected override void BuildStake(ITicket ticket)
        {
            base.BuildStake(ticket);

            Template.stake = "-";
        }
    }
}