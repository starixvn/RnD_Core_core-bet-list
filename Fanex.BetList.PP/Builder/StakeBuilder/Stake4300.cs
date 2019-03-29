namespace Fanex.BetList.Core.Builder.StakeBuilder
{
    using Fanex.BetList.Core.Entities;

    public class Stake4300 : Stake1
    {
        private const string Hyphen = "-";

        protected override void BuildStake(ITicket ticket)
        {
            base.BuildStake(ticket);

            if (ticket.Stake == 0)
            {
                Template.stake = Hyphen;
            }
        }
    }
}