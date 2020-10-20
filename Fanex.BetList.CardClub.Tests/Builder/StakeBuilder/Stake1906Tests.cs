namespace Fanex.BetList.PP.Tests.Builder.StakeBuilder
{
    using Fanex.BetList.Core.Builder.StakeBuilder;
    using Fanex.BetList.Core.Entities;
    using NUnit.Framework;

    [TestFixture]
    public class Stake1906Tests
    {
        [Test]
        public void BuildStake_StakeIsZero_ReturnHyphen()
        {
            // Arrange
            var ticket = new Ticket { Stake = 0 };
            var stake = new Stake1906();

            // Act
            var template = stake.Render(ticket);

            // Assert
            Assert.AreEqual("-", template.stake);
        }

        [Test]
        public void BuildStake_HasStake_ReturnHyphen()
        {
            // Arrange
            var ticket = new Ticket { Stake = 10 };
            var stake = new Stake1906();

            // Act
            var template = stake.Render(ticket);

            // Assert
            Assert.AreEqual("-", template.stake);
        }
    }
}