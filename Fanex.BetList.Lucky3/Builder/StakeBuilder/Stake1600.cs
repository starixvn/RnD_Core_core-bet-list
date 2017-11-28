namespace Fanex.BetList.Core.Builder.StakeBuilder
{
    using Entities;
    using Utils;

    public class Stake1600 : Stake1
    {
        protected override void BuildStake(ITicket ticket)
        {
            Template.stake = string.Format(Formatter.DECIMALFORMAT20, ticket.Stake);
        }
    }
}