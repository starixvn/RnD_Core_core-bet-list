namespace Fanex.BetList.Core.Builder.StakeBuilder
{
    using Fanex.BetList.Core.Entities;

    public class Stake17000 : Stake1
    {
        protected override void BuildBonus(ITicket ticket)
        {
            Template.Bonus.Hide();
        }
    }
}