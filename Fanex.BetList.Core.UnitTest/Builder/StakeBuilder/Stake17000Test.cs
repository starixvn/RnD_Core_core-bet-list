namespace Fanex.BetList.Core.UnitTest.Builder.StakeBuilder
{
    using Fanex.BetList.Core.Builder.StakeBuilder;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.UnitTest.Common;
    using NUnit.Framework;

    public class Stake17000Test
    {
        [Test]
        public void BuildBonus_Always_HideBonus()
        {
            // Arrange.
            var ticket = TestData.CreateTicket(BetTypes.SabaLuckySpin);
            var stake = new Stake17000();

            // Act.
            var template = stake.Render(ticket);

            // Assert.
            Assert.IsFalse(template.Bonus.Visible);
        }
    }
}