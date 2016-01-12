namespace Fanex.BetList.Core.UnitTest.Builder.StakeBuilder
{
    using Fanex.BetList.Core.Builder.StakeBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Stake500Test
    {
        private Stake500 _stake;
        private ITicket _ticket;

        [SetUp]
        public void Setup()
        {
            _stake = new Stake500();
            _ticket = Substitute.For<ITicket>();
            _ticket.Stake = 1000;
        }

        [TestCase(1000.0)]
        [TestCase(0)]
        public void BuildStake_Always_StakeIsDashSymbol(decimal stake)
        {
            ////Arrange
            _ticket.Stake = stake;

            ////Act
            var template = _stake.Render(_ticket);

            ////Assert
            var actualStake = template.stake;
            Assert.AreEqual("-", actualStake);
        }
    }
}