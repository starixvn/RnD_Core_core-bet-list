namespace Fanex.BetList.RnGCasino.Tests.Builder.ChoiceBuilder
{
    using Core.Builder.ChoiceBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice2198Tests
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice2198();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildBetTeam_Always_ShowJackpotContributionInfoInBetTeam()
        {
            // Arrange
            _ticket.Odds = 0.12m;
            _ticket.OddsSpread = 100;
            _ticket.Currency = "UUS";

            // Act
            var choice = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            var expectedValue = "<div class=\"favorite\">Stake: 100 UUS @ Rate 12.0000%</div>";
            Assert.AreEqual(expectedValue, choice.betTeam);
        }

        [Test]
        public void BuildBetTeam_DetailedTicket_ShowJackpotGameRefNo()
        {
            // Arrange
            _ticket.BetCheck = "423424234";
            _ticket.BetdaqId = 1234;
            _ticket.Odds = 0.12m;
            _ticket.OddsSpread = 100;
            _ticket.Currency = "UUS";

            // Act
            var choice = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            var expectedValue = "423424234<div class=\"jackpotRefNo\">For Ref No: 1234</div><div class=\"favorite\">Stake: 100 UUS @ Rate 12.0000%</div>";
            Assert.AreEqual(expectedValue, choice.betTeam);
        }
    }
}