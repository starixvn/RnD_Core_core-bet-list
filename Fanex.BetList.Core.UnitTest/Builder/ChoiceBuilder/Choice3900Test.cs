namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice3900Test
    {
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private IChoice _choice;

        [SetUp]
        public void Setup()
        {
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticket = new Ticket();
            _choice = new Choice3900();
        }

        [TestCase("3:0", "3-0")]
        [TestCase("2:3", "2-3")]
        public void BuildBetTeam_BetTeamValue_ShowCorrectScore(string betTeamValue, string correctScore)
        {
            // Arrange
            _ticket.BetTeam = betTeamValue;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(correctScore, _choice.Template.betTeam);
        }
        
        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_NullHandicap()
        {
            // Arrange
            _ticket.BetTeam = "3:2";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsNull(_choice.Template.Handicap.handicap);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_FavoriteBetTeamClass()
        {
            // Arrange
            _ticket.BetTeam = "3:2";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual("favorite", _choice.Template.betTeamClassName);
        }
    }
}
