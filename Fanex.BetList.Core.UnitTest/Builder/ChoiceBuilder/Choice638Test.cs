namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Constants;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice638Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice638();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.HomeTeamQuartersWinOverUnder;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }           

        [TestCase("o", "Over")]
        [TestCase("O", "Over")]
        [TestCase("u", "Under")]
        [TestCase("U", "Under")]
        public void BuildBetTeam_ValidBetTeam_ReturnExpectedBetChoice(string betTeam, string expectedBetChoice)
        {
            // Arrange
            _ticket.BetTeam = betTeam;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(expectedBetChoice, _choice.Template.betTeam);
        }

        [TestCase("o", "favorite")]
        [TestCase("O", "favorite")]
        [TestCase("u", "underdog")]
        [TestCase("U", "underdog")]
        public void BuildBetTeamClassNameAndHandicap_Always_AssertExpectedClass(string betteam, string expectedClass)
        {
            // Arrange
            _ticket.BetTeam = betteam;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(expectedClass, _choice.Template.betTeamClassName);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_AssertHandicap1()
        {
            // Arrange
            _ticket.BetTeam = "o";
            _ticket.Handicap1 = 1;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual("1", _choice.Template.Handicap.handicap);
        }
    }
}