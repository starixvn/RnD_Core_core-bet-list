namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    ///  Unit test for Choice210.
    /// </summary>
    [TestFixture]
    public class Choice210Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice210();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildScore_Always_SetHomeScoreIsLiveHomeScore()
        {
            // Arrange
            _ticket.LiveHomeScore = 1;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(_ticket.LiveHomeScore.ToString(), _choice.Template.Score.homeScore);
        }

        [Test]
        public void BuildScore_Always_SetAwayScoreIsLiveAwayScore()
        {
            // Arrange
            _ticket.LiveAwayScore = 1;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(_ticket.LiveAwayScore.ToString(), _choice.Template.Score.awayScore);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void BuildScore_Always_SetScoreVisibleIsIsLive(bool isLive)
        {
            // Arrange
            _ticket.IsLive = isLive;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(isLive, _choice.Template.Score.Visible);
        }

        [Test]
        public void BuildScore_Always_SetScoreClassNameIsFavorite()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            const string CSS_BETTEAM_CLASS = "favorite";
            Assert.AreEqual(CSS_BETTEAM_CLASS, _choice.Template.Score.scoreClassName);
        }
    }
}
