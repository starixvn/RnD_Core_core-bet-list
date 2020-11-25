namespace Fanex.BetList.BetTrade.Test.Builder.StakeBuilder
{
    using Core.Builder.StakeBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Stake901Tests
    {
        [Test]
        public void Render_Always_HideRabateBonusIcon()
        {
            // Arrange.
            var stake = new Stake901();
            var ticket = new Ticket();
            var ticketHelper = Substitute.For<ITicketHelper>();

            // Act.
            var template = stake.Render(ticket, ticketHelper);

            // Assert.
            Assert.IsFalse(template.Bonus.RebateBonusIcon.Visible);
        }

        [Test]
        public void Render_Always_HideNonPeakHoursPromotionIcon()
        {
            // Arrange.
            var stake = new Stake901();
            var ticket = new Ticket();
            var ticketHelper = Substitute.For<ITicketHelper>();

            // Act.
            var template = stake.Render(ticket, ticketHelper);

            // Assert.
            Assert.IsFalse(template.Bonus.NonPeakHoursPromotionIcon.Visible);
        }
    }
}