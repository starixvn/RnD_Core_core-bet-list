namespace Fanex.BetList.CashOut.Tests.Builder.StakeBuilder
{
    using Core.Builder.StakeBuilder;
    using Core.Entities;
    using NUnit.Framework;

    public class Stake911Tests
    {
        [Test]
        public void Render_Always_HideRabateBonus()
        {
            // Arrange.
            var stake = new Stake911();
            var ticket = new Ticket();

            // Act.
            var template = stake.Render(ticket);

            // Assert.
            Assert.IsFalse(template.Bonus.RebateBonusIcon.Visible);
        }
    }
}