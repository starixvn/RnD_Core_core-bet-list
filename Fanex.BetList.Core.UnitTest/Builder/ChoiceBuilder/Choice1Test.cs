namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.UnitTest.Common.Enums;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Unit test for Choice1 class.
    /// </summary>
    [TestFixture]
    public class Choice1Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice1();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.NumberWheel;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildChoice_Ticket_HomeScoreIsLiveHomeScore()
        {
            // Arrange
            _ticket.LiveHomeScore = 1;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedHomeScore = _ticket.LiveHomeScore.ToString();
            Assert.AreEqual(expectedHomeScore, _choice.Template.Score.homeScore);
        }

        [Test]
        public void BuildChoice_Ticket_AwayScoreIsLiveAwayScore()
        {
            // Arrange
            _ticket.LiveAwayScore = 1;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedAwayScore = _ticket.LiveAwayScore.ToString();
            Assert.AreEqual(expectedAwayScore, _choice.Template.Score.awayScore);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void BuildChoice_Ticket_ScoreVisibleEqualTicketIsLive(bool isLive)
        {
            // Arrange
            _ticket.IsLive = isLive;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            bool expectedScoreVisible = _ticket.IsLive;
            Assert.AreEqual(expectedScoreVisible, _choice.Template.Score.Visible);
        }

        [Test]
        public void BuildChoice_Ticket_LeagueNameIsGetLeagueNameById()
        {
            // Arrange
            _ticketHelper.GetLeagueNameById(Arg.Any<int>()).Returns<string>("League name");

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedLeagueName = _ticketHelper.GetLeagueNameById(_ticket.LeagueId);
            Assert.AreEqual(expectedLeagueName, _choice.Template.League.LeagueName.leagueName);
        }

        [Test]
        public void BuildChoice_Ticket_BetTypeIsGetBetTypeNameById()
        {
            // Arrange
            _ticketHelper.GetBetTypeNameById(Arg.Any<int>()).Returns<string>("Bet type name");

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedBetTypeName = _ticketHelper.GetBetTypeNameById(_ticket.BetTypeId);
            Assert.AreEqual(expectedBetTypeName, _choice.Template.BetType.betTypeName);
        }
    }
}
