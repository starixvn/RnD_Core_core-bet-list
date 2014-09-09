namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Common.Enums;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Unit test for Choice10 class.
    /// </summary>
    [TestFixture]
    public class Choice10Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice10();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.Outright;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        /// <summary>
        /// The value of Template.Handicap.handicap is always null.
        /// </summary>
        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetHandicapIsNull()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNull(_choice.Template.Handicap.handicap);
        }

        /// <summary>
        /// The value of Template.Match.homeTeam is always null.
        /// </summary>
        [Test]
        public void BuildMatch_Always_SetHomeTeamIsNull()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNull(_choice.Template.Match.homeTeam);
        }

        /// <summary>
        /// The value of Template.Match.awayTeam is always null.
        /// </summary>
        [Test]
        public void BuildMatch_Always_SetAwayTeamIsNull()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNull(_choice.Template.Match.awayTeam);
        }

        /// <summary>
        /// The value of Template.Match.VS is always null.
        /// </summary>
        [Test]
        public void BuildMatch_Always_SetMatchVSBlockIsNull()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNull(_choice.Template.Match.VS);
        }

        /// <summary>
        /// The value of Template.Match.home_firstGoal_lastGoal is always null.
        /// </summary>
        [Test]
        public void BuildMatch_Always_SetHomeFGLGIsNull()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNull(_choice.Template.Match.home_firstGoal_lastGoal);
        }

        /// <summary>
        /// The value of Template.Match.away_firstGoal_lastGoal is always null.
        /// </summary>
        [Test]
        public void BuildMatch_Always_SetAwayFGLGIsNull()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNull(_choice.Template.Match.away_firstGoal_lastGoal);
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

        /// <summary>
        /// The value of Template.BetType.betTypeName is always bet type name get by identifier.
        /// </summary>
        [Test]
        public void BuildBetType_Always_SetBetTypeIsBetTypeName()
        {
            // Arrange
            const string BET_TYPE_NAME = "Bet type name";
            _ticketHelper.GetBetTypeNameById(Arg.Any<int>()).Returns<string>(BET_TYPE_NAME);

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(BET_TYPE_NAME, _choice.Template.BetType.betTypeName);
        }

        /// <summary>
        /// The value of Template.betTeam is always home team name when home id != 0.
        /// </summary>
        [Test]
        public void BuildBetTeam_HomeIdNotZero_BetTeamIsHomeTeamName()
        {
            // Arrange
            const string HOME_TEAM_NAME = "Home team name";
            _ticket.HomeId = 1;
            _ticketHelper.GetTeamNameById(Arg.Any<long>()).Returns<string>(HOME_TEAM_NAME);

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(HOME_TEAM_NAME, _choice.Template.betTeam);
        }

        /// <summary>
        /// The value of Template.betTeam is always away team name when home id = 0.
        /// </summary>
        [Test]
        public void BuildBetTeam_HomeIdIsZero_BetTeamIsAwayTeamName()
        {
            // Arrange
            const string AWEAY_TEAM_NAME = "Away team name";
            _ticket.HomeId = 0;
            _ticketHelper.GetTeamNameById(Arg.Any<long>()).Returns<string>(AWEAY_TEAM_NAME);

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(AWEAY_TEAM_NAME, _choice.Template.betTeam);
        }

        /// <summary>
        /// The value of Template.betTeamClassName is always favorite.
        /// </summary>
        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetBetTeamClassNameIsFavorite()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            const string CSS_CLASS_FAVORITE = "favorite";
            Assert.AreEqual(CSS_CLASS_FAVORITE, _choice.Template.betTeamClassName);
        }
    }
}