namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Constants;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice642Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice642();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.DoubleQuarterWinner;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [TestCase("")]
        [TestCase("abc")]
        public void BuildBetTeam_InvalidBetTeam_ReturnEmpty(string betteam)
        {
            // Arrange
            _ticket.BetTeam = betteam;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsEmpty(_choice.Template.betTeam);
        }       

        [TestCase("h12", "Home Win 1st & 2nd Quarter")]
        [TestCase("h13", "Home Win 1st & 3rd Quarter")]
        [TestCase("h14", "Home Win 1st & 4th Quarter")]
        [TestCase("h23", "Home Win 2nd & 3rd Quarter")]
        [TestCase("h24", "Home Win 2nd & 4th Quarter")]
        [TestCase("h34", "Home Win 3rd & 4th Quarter")]
        [TestCase("a12", "Away Win 1st & 2nd Quarter")]
        [TestCase("a13", "Away Win 1st & 3rd Quarter")]
        [TestCase("a14", "Away Win 1st & 4th Quarter")]
        [TestCase("a23", "Away Win 2nd & 3rd Quarter")]
        [TestCase("a24", "Away Win 2nd & 4th Quarter")]
        [TestCase("a34", "Away Win 3rd & 4th Quarter")]
        [TestCase("aos12", "AOS of 1st & 2nd Quarter")]
        [TestCase("aos13", "AOS of 1st & 3rd Quarter")]
        [TestCase("aos14", "AOS of 1st & 4th Quarter")]
        [TestCase("aos23", "AOS of 2nd & 3rd Quarter")]
        [TestCase("aos24", "AOS of 2nd & 4th Quarter")]
        [TestCase("aos34", "AOS of 3rd & 4th Quarter")]
        [TestCase("AOS34", "AOS of 3rd & 4th Quarter")]
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