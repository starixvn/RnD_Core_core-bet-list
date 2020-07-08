namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Constants;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice632Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice632();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.QuarterXDoubleChance;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [TestCase("")]
        public void BuildBetTeam_InvalidBetTeam_ReturnEmpty(string betteam)
        {
            // Arrange
            _ticket.BetTeam = betteam;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsEmpty(_choice.Template.betTeam);
        }       

        [TestCase("oooo", "Odd/Odd/Odd/Odd")]
        [TestCase("oooe", "Odd/Odd/Odd/Even")]
        [TestCase("ooeo", "Odd/Odd/Even/Odd")]
        [TestCase("oeoo", "Odd/Even/Odd/Odd")]
        [TestCase("eooo", "Even/Odd/Odd/Odd")]
        [TestCase("eeee", "Even/Even/Even/Even")]
        [TestCase("eeeo", "Even/Even/Even/Odd")]
        [TestCase("eeoe", "Even/Even/Odd/Even")]
        [TestCase("eoee", "Even/Odd/Even/Even")]
        [TestCase("oeee", "Odd/Even/Even/Even")]
        [TestCase("ooee", "Odd/Odd/Even/Even")]
        [TestCase("oeoe", "Odd/Even/Odd/Even")]
        [TestCase("eoeo", "Even/Odd/Even/Odd")]
        [TestCase("eeoo", "Even/Even/Odd/Odd")]
        [TestCase("eooe", "Even/Odd/Odd/Even")]
        [TestCase("oeeo", "Odd/Even/Even/Odd")]
        public void BuildBetTeam_ValidBetTeam_ReturnExpectedBetChoice(string betTeam, string expectedBetChoice)
        {
            // Arrange
            _ticket.BetTeam = betTeam;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(expectedBetChoice, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetHandicapIsNull()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsNull(_choice.Template.Handicap.handicap);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetBetTeamClassNameIsFavorite()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            const string CSS_CLASS_FAVORITE = "favorite";
            Assert.AreEqual(CSS_CLASS_FAVORITE, _choice.Template.betTeamClassName);
        }
    }
}