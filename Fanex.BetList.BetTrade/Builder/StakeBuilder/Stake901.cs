namespace Fanex.BetList.Core.Builder.StakeBuilder
{
    using Entities;

    public class Stake901 : Stake1
    {
        protected override void BuildBonus(ITicket ticket, ITicketHelper ticketHelper)
        {
            base.BuildBonus(ticket, ticketHelper);

            Template.Bonus.RebateBonusIcon.Hide();
            Template.Bonus.NonPeakHoursPromotionIcon.Hide();
        }
    }
}