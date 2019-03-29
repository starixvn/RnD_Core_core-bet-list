namespace Fanex.BetList.PP.Tests.Builder.StakeBuilder
{
    using Fanex.BetList.Core.Builder.StakeBuilder;
    using Fanex.BetList.Core.Entities;
    using NUnit.Framework;

    [TestFixture]
    public class Stake4300Tests
    {
        [Test]
        public void BuildStake_StakeIsZero_ReturnHyphen()
        {
            // Arrange
            var ticket = new Ticket { Stake = 0 };
            var stake = new Stake4300();

            // Act
            var template = stake.Render(ticket);

            // Assert
            Assert.AreEqual("-", template.stake);
        }
    }
}