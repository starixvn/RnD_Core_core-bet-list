namespace Fanex.BetList.Core.Builder.StakeBuilder
{
    using Fanex.BetList.Core.Entities;

    public class Stake17003 : Stake1
    {
        protected override void BuildNonPeakHoursPromotion(ITicket ticket)
            => Template.Bonus.NonPeakHoursPromotionIcon.Visible = false;
    }
}