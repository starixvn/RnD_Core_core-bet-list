namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Constants;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice633Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice633();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.Quarter3Quarter4Result;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [TestCase("")]
        [TestCase("cbe")]
        public void BuildBetTeam_InvalidBetTeam_ReturnEmpty(string betteam)
        {
            // Arrange
            _ticket.BetTeam = betteam;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsEmpty(_choice.Template.betTeam);
        }       

        [TestCase("hh", "Home/Home")]
        [TestCase("hd", "Home/Draw")]
        [TestCase("ha", "Home/Away")]
        [TestCase("dh", "Draw/Home")]
        [TestCase("dd", "Draw/Draw")]
        [TestCase("da", "Draw/Away")]
        [TestCase("ah", "Away/Home")]
        [TestCase("ad", "Away/Draw")]
        [TestCase("aa", "Away/Away")]
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