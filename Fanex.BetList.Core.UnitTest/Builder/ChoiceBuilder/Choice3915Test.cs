namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice3915Test
    {
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private IChoice _choice;

        [SetUp]
        public void Setup()
        {
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticket = new Ticket();
            _choice = new Choice3915();
            _ticket.HomeId = 1;
            _ticket.AwayId = 2;
            _ticketHelper.GetTeamNameById(_ticket.HomeId).Returns("Team Home");
            _ticketHelper.GetTeamNameById(_ticket.AwayId).Returns("Team Away");
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_NullHandicap()
        {
            // Arrange
            _ticket.BetTeam = "h";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsNull(_choice.Template.Handicap.handicap);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_FavoriteBetTeamClass()
        {
            // Arrange
            _ticket.BetTeam = "h";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual("favorite", _choice.Template.betTeamClassName);
        }

        [TestCase(80140, "Leading Team After Set 1 First 40 Points Played")]
        public void BuildBetType_BetChoice_CorrectBetTypeName(long betId, string betTypeName)
        {
            //Arrange
            _ticket.BetId = betId;
            _ticket.BetTypeId = 3915;
            _ticket.BetTeam = "a";
            _ticketHelper.GetBetTypeNameById(3915, Arg.Any<string>(), betId: betId).Returns(betTypeName);

            //Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(betTypeName, _choice.Template.BetType.betTypeName);
        }

        [TestCase("h", "Team Home")]
        [TestCase("a", "Team Away")]
        [TestCase("d", "Draw")]
        [TestCase("", "Draw")]
        public void BuildBetTeam_BetTeamValue_CorrectTeamName(string betTeamValue, string teamName)
        {
            //Arrange
            _ticket.BetTeam = betTeamValue;

            //Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(teamName, _choice.Template.betTeam);
        }
    }
}
