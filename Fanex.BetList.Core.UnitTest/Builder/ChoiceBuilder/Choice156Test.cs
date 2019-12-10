namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Constants;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice156Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice156();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.SetXTotalGameOverUnder;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildBetTeam_BetTeamIsCharacterO_BetTeamIsOver()
        {
            // Arrange
            _ticket.BetTeam = "o";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            string expectedBetTeam = "Over";
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamIsCharacterU_BetTeamIsUnder()
        {
            // Arrange
            _ticket.BetTeam = "u";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            string expectedBetTeam = "Under";
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_BetTeamIsCharacterO_BetTeamClassNameIsFavorite()
        {
            // Arrange
            _ticket.BetTeam = "o";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            string expectedBetTeamClassName = "favorite";
            Assert.AreEqual(expectedBetTeamClassName, _choice.Template.betTeamClassName);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_BetTeamIsCharacterU_BetTeamClassNameIsUnderdog()
        {
            // Arrange
            _ticket.BetTeam = "u";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            string expectedBetTeamClassName = "underdog";
            Assert.AreEqual(expectedBetTeamClassName, _choice.Template.betTeamClassName);
        }

        [Test]
        public void BuildBetType_Always_SetBetTypeNameIsBetTypeName()
        {
            // Arrange
            _ticketHelper.GetBetTypeNameById(Arg.Any<object>(), Arg.Any<object>(), Arg.Any<object>(), Arg.Any<object>()).Returns("Bet type name");

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual("Bet type name", _choice.Template.BetType.betTypeName);
        }
    }
}