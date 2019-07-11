namespace Fanex.BetList.Core.Builder.StakeBuilder
{
    using Entities;

    public class Stake901 : Stake1
    {
        protected override void BuildBonus(ITicket ticket)
        {
            base.BuildBonus(ticket);

            Template.Bonus.RebateBonusIcon.Visible = false;
        }
    }
}