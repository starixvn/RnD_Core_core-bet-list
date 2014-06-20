namespace Fanex.BetList.Core.Builder.StakeBuilder
{
    using Entities;
    using Utils;

    /// <summary>
    /// Combination Mix Parlay.
    /// </summary>
    public class Stake29 : Stake1
    {
        protected override void BuildStake(ITicket ticket)
        {
            decimal stake = Cast.AsDecimal(ticket.Stake);
            Template.stake = ConvertByBetType.Stake(ticket.ActualStake);

            if (stake != 0)
            {
                string stakeContent = ConvertByBetType.Stake(ticket.Stake);

                Template.stake = string.Join(null, new string[] { Template.stake, "<br/>", "&nbsp;(", stakeContent, ")" });
            }
        }
    }
}