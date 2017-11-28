namespace Fanex.BetList.Lucky3.Tests.Builder.StakeBuilder
{
    using Core.Builder.StakeBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Stake1600Tests
    {
        private IStake _stake;
        private ITicket _ticket;

        [SetUp]
        public void Setup()
        {
            _stake = new Stake1600();
            _ticket = Substitute.For<ITicket>();
        }

        [Test]
        public void BuildStake_TicketStakeIsInteger_FixTheStakeWithTwoDecimalDigits()
        {
            // Arrange.
            _ticket.Stake = 1000;

            // Act.
            var template = _stake.Render(_ticket);

            // Assert.
            const string ExpectedStakeDisplay = "1,000.00";

            Assert.AreEqual(ExpectedStakeDisplay, template.stake);
        }

        [Test]
        public void BuildStake_TicketStakeTakesMoreThanTwoDecimalPlaces_AlwaysFixToTwoDecimalDigits()
        {
            // Arrange.
            _ticket.Stake = 1000.123M;

            // Act.
            var template = _stake.Render(_ticket);

            // Assert.
            const string ExpectedStakeDisplay = "1,000.12";

            Assert.AreEqual(ExpectedStakeDisplay, template.stake);
        }

        [Test]
        public void BuildStake_TicketStakeTakesOnlyOneDecimalPlace_AlwaysFixToTwoDecimalDigits()
        {
            // Arrange.
            _ticket.Stake = 1000.5M;

            // Act.
            var template = _stake.Render(_ticket);

            // Assert.
            const string ExpectedStakeDisplay = "1,000.50";

            Assert.AreEqual(ExpectedStakeDisplay, template.stake);
        }

        [Test]
        public void BuildStake_TicketStakeTakesTwoDecimalPlaces_ShowAsWhatItHas()
        {
            // Arrange.
            _ticket.Stake = 1000.55M;

            // Act.
            var template = _stake.Render(_ticket);

            // Assert.
            const string ExpectedStakeDisplay = "1,000.55";

            Assert.AreEqual(ExpectedStakeDisplay, template.stake);
        }
    }
}