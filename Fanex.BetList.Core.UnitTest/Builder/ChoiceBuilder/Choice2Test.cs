namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Constants;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice2Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice2();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.OddEven;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildBetTeam_BetTeamIsCharacterH_BetTeamIsOdd()
        {
            // Arrange
            _ticket.BetTeam = "h";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            string expectedBetTeam = "Odd";
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamIsCharacterA_BetTeamIsEven()
        {
            // Arrange
            _ticket.BetTeam = "a";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            string expectedBetTeam = "Even";
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_HandicapIsNull()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsNull(_choice.Template.Handicap.handicap);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_BetTeamIsCharacterh_BetTeamClassNameIsFavorite()
        {
            // Arrange
            _ticket.BetTeam = "h";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            string expectedBetTeamClassName = "favorite";
            Assert.AreEqual(expectedBetTeamClassName, _choice.Template.betTeamClassName);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_BetTeamIsCharactera_BetTeamClassNameIsUnderdog()
        {
            // Arrange
            _ticket.BetTeam = "a";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            string expectedBetTeamClassName = "underdog";
            Assert.AreEqual(expectedBetTeamClassName, _choice.Template.betTeamClassName);
        }
    }
}