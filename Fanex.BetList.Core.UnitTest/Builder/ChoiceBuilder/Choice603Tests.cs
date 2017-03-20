namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Constants;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice603Tests
    {
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private IChoice _choice;

        [SetUp]
        public void Setup()
        {
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticket = new Ticket();
            _choice = new Choice603();
        }

        [Test]
        public void BuildBetTeam_BetTeamIsHomeTeam_ShowHomeTeamName()
        {
            // Arrange
            _ticket.BetTeam = "h";
            _ticket.HomeId = 1;
            _ticketHelper.GetTeamNameById(_ticket.HomeId).Returns("Home Team Name");

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual("Home Team Name", _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamIsAwayTeam_ShowAwayTeamName()
        {
            // Arrange
            _ticket.BetTeam = "a";
            _ticket.AwayId = 1;
            _ticketHelper.GetTeamNameById(_ticket.AwayId).Returns("Away Team Name");

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual("Away Team Name", _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_IsBetHomeTeam_BetTeamClassNameIsFavorite()
        {
            // Arrange
            _ticket.BetTeam = BetTeamValue.H;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual("favorite", _choice.Template.betTeamClassName);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_IsBetAwayTeam_BetTeamClassNameIsUnderdog()
        {
            // Arrange
            _ticket.BetTeam = BetTeamValue.A;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual("underdog", _choice.Template.betTeamClassName);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_HandicapIsNull()
        {
            // Arrange
            _ticket.BetTeam = BetTeamValue.A;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Arrange
            Assert.IsNull(_choice.Template.Handicap.handicap);
        }
    }
}