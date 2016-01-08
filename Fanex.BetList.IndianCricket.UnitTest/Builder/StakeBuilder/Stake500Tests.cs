namespace Fanex.BetList.Core.UnitTest.Builder.StakeBuilder
{
    using Fanex.BetList.Core.Builder.StakeBuilder;
    using Fanex.BetList.Core.Entities;
    using NUnit.Framework;
    using NSubstitute;

    [TestFixture]
    public class Stake500Tests
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

        [Test]
        public void BuildStake_Always_StakeIsDashSymbol()
        {
            ////Act
            var template = _stake.Render(_ticket);

            ////Assert
            var stake = template.stake;
            Assert.AreEqual("-", stake);
        }
    }
}