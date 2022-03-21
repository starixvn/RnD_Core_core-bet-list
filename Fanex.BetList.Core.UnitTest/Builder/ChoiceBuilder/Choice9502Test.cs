namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Constants;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice9502Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice9502();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.HomeGroup1XRunsOE;
            _ticket.BetId = 99;
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticketHelper.GetBetTypeNameById(BetTypes.HomeGroup1XRunsOE, Arg.Any<object>(), (long?)99, Arg.Any<object>()).Returns("Bettype name 99");
        }

        [Test]
        public void BuildBetTeam_BetTeamIsCharacterH_BetTeamIsOdd()
        {
            // Arrange
            _ticket.BetTeam = "o";

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
            _ticket.BetTeam = "e";

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
            _ticket.BetTeam = "o";

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
            _ticket.BetTeam = "e";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            string expectedBetTeamClassName = "underdog";
            Assert.AreEqual(expectedBetTeamClassName, _choice.Template.betTeamClassName);
        }

        [Test]
        public void BuildBetType_Always_ShowBetTypeNameAndReplaceX()
        {
            // Arrange

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual("Bettype name 99", _choice.Template.BetType.betTypeName);
        }
    }
}