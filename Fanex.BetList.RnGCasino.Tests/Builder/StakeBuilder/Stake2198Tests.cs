namespace Fanex.BetList.RnGCasino.Tests.Builder.OddsBuilder
{
    using Fanex.BetList.Core.Builder.StakeBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Stake2198Tests
    {
        private IStake _stake;
        private ITicket _ticket;

        [SetUp]
        public void Setup()
        {
            _stake = new Stake2198();
            _ticket = Substitute.For<ITicket>();
        }

        [Test]
        public void BuildStake_Always_ShowHyphen()
        {
            // Arrange
            var ticketHelper = Substitute.For<ITicketHelper>();

            // Act
            var stake = _stake.Render(_ticket, ticketHelper);

            // Assert
            Assert.AreEqual("-", stake.stake);
        }
    }
}