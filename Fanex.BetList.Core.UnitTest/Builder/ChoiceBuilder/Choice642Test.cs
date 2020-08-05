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

        [TestCase("h12", "Home To Win Quarter 1 & 2")]
        [TestCase("h13", "Home To Win Quarter 1 & 3")]
        [TestCase("h14", "Home To Win Quarter 1 & 4")]
        [TestCase("h23", "Home To Win Quarter 2 & 3")]
        [TestCase("h24", "Home To Win Quarter 2 & 4")]
        [TestCase("h34", "Home To Win Quarter 3 & 4")]
        [TestCase("a12", "Away To Win Quarter 1 & 2")]
        [TestCase("a13", "Away To Win Quarter 1 & 3")]
        [TestCase("a14", "Away To Win Quarter 1 & 4")]
        [TestCase("a23", "Away To Win Quarter 2 & 3")]
        [TestCase("a24", "Away To Win Quarter 2 & 4")]
        [TestCase("a34", "Away To Win Quarter 3 & 4")]
        [TestCase("aos12", "AOS For Quarter 1 & 2")]
        [TestCase("aos13", "AOS For Quarter 1 & 3")]
        [TestCase("aos14", "AOS For Quarter 1 & 4")]
        [TestCase("aos23", "AOS For Quarter 2 & 3")]
        [TestCase("aos24", "AOS For Quarter 2 & 4")]
        [TestCase("aos34", "AOS For Quarter 3 & 4")]
        [TestCase("AOS34", "AOS For Quarter 3 & 4")]
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