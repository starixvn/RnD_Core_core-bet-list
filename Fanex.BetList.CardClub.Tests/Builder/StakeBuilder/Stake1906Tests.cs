namespace Fanex.BetList.PP.Tests.Builder.StakeBuilder
{
    using Fanex.BetList.Core.Builder.StakeBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
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
            var ticketHelper = Substitute.For<ITicketHelper>();

            // Act
            var template = stake.Render(ticket, ticketHelper);

            // Assert
            Assert.AreEqual("-", template.stake);
        }

        [Test]
        public void BuildStake_HasStake_ReturnHyphen()
        {
            // Arrange
            var ticket = new Ticket { Stake = 10 };
            var stake = new Stake1906();
            var ticketHelper = Substitute.For<ITicketHelper>();

            // Act
            var template = stake.Render(ticket, ticketHelper);

            // Assert
            Assert.AreEqual("-", template.stake);
        }
    }
}