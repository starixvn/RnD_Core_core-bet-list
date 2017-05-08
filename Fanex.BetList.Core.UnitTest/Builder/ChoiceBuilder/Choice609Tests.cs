namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Common;
    using Constants;
    using Core.Builder.ChoiceBuilder;
    using Core.Utils;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice609Tests
    {
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private IChoice _choice;

        [SetUp]
        public void Setup()
        {
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticket = TestData.CreateTicket(609);
            _choice = new Choice609();
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_IsBetHomeTeam_ShowCorrectHandicap()
        {
            // Arrange
            _ticket.BetTeam = BetTeamValue.H;
            _ticket.Handicap2 = 2;
            _ticket.Handicap1 = 1;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            var expectedHandicap = ConvertByBetType.Hdp(2 - 1);
            Assert.AreEqual(expectedHandicap, _choice.Template.Handicap.handicap);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_IsBetAwayTeam_ShowCorrectHandicap()
        {
            // Arrange
            _ticket.BetTeam = BetTeamValue.A;
            _ticket.Handicap2 = 2;
            _ticket.Handicap1 = 1;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            var expectedHandicap = ConvertByBetType.Hdp(1 - 2);
            Assert.AreEqual(expectedHandicap, _choice.Template.Handicap.handicap);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_HandicapeIsZero_ShowUnderdogColorBetTeam()
        {
            // Arrange
            _ticket.BetTeam = BetTeamValue.H;
            _ticket.Handicap2 = 2;
            _ticket.Handicap1 = 2;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual("underdog", _choice.Template.betTeamClassName);
        }
    }
}