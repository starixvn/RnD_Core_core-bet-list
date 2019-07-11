namespace Fanex.BetList.Core.Builder.StakeBuilder
{
    using Entities;

    public class Stake911 : Stake1
    {
        protected override void BuildBonus(ITicket ticket)
        {
            base.BuildBonus(ticket);

            Template.Bonus.RebateBonusIcon.Visible = false;
        }
    }
}