namespace Fanex.BetList.Core.UnitTest.Builder.StakeBuilder
{
    using Fanex.BetList.Core.Builder.StakeBuilder;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.UnitTest.Common;
    using NUnit.Framework;

    public class Stake17003Test
    {
        [Test]
        public void BuildNonPeakHoursPromotion_Always_HideIcon()
        {
            // Arrange.
            var ticket = TestData.CreateTicket(BetTypes.SabaLoyaltyPointsExchange);
            var stake = new Stake17003();

            // Act.
            var template = stake.Render(ticket);

            // Assert.
            Assert.IsFalse(template.Bonus.NonPeakHoursPromotionIcon.Visible);
        }
    }
}