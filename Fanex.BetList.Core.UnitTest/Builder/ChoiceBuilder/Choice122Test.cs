namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Constants;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice122Test
    {
        private const string Underdog = "underdog";
        private const string Favorite = "favorite";

        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice122();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.AwayNoBet;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildScore_Always_SetHomeScoreIsLiveHomeScore()
        {
            // Arrange
            _ticket.LiveHomeScore = 1;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(_ticket.LiveHomeScore.ToString(), _choice.Template.Score.homeScore);
        }

        [Test]
        public void BuildScore_Always_SetAwayScoreIsLiveAwayScore()
        {
            // Arrange
            _ticket.LiveAwayScore = 1;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

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
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(_ticket.IsLive, _choice.Template.Score.Visible);
        }

        [Test]
        public void BuildLeague_Always_SetLeagueNameIsLeagueName()
        {
            // Arrange
            const string LEAGUE_NAME = "League name";
            _ticketHelper.GetLeagueNameById(Arg.Any<int>(), Arg.Any<int>()).Returns<string>(LEAGUE_NAME);

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(LEAGUE_NAME, _choice.Template.League.LeagueName.leagueName);
        }

        [Test]
        public void BuildSport_Always_SetSportNameIsSportName()
        {
            // Arrange
            const string SPORT_NAME = "Sport name";
            _ticketHelper.GetSportNameById(Arg.Any<int>()).Returns<string>(SPORT_NAME);

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(SPORT_NAME, _choice.Template.League.sportTypeName);
        }

        [Test]
        public void BuildMatch_NeutralTicket_HomeTeamAreHomeTeamNameAndSpaceAndCharacterNWithinParenthese()
        {
            // Arrange
            _ticket.IsNeutral = true;
            _ticketHelper.GetTeamNameById(Arg.Any<long>()).Returns<string>("Home team name");

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            const string HOME_TEAM_NAME = "Home team name (N)";
            Assert.AreEqual(HOME_TEAM_NAME, _choice.Template.Match.homeTeam);
        }

        [Test]
        public void BuildMatch_NotNeutralTicket_HomeTeamIsHomeTeamName()
        {
            // Arrange
            const string HOME_TEAM_NAME = "Home team name";
            _ticket.IsNeutral = false;
            _ticketHelper.GetTeamNameById(Arg.Any<long>()).Returns<string>(HOME_TEAM_NAME);

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(HOME_TEAM_NAME, _choice.Template.Match.homeTeam);
        }

        [Test]
        public void BuildMatch_Always_SetAwayTeamIsAwayTeamName()
        {
            // Arrange
            const string AWAY_TEAM_NAME = "Away team name";
            _ticketHelper.GetTeamNameById(Arg.Any<long>()).Returns<string>(AWAY_TEAM_NAME);

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(AWAY_TEAM_NAME, _choice.Template.Match.awayTeam);
        }

        [TestCase("FL:0-1")]
        [TestCase("FL:2-1")]
        public void BuildFGLGLabel_EventStatus_HomeFGLGIsLableLG(string eventStatus)
        {
            // Arrange
            _ticket.EventStatus = eventStatus;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            const string HOME_FGLG = "&nbsp;<div class='lastGoal'>&nbsp;</div>";
            Assert.AreEqual(HOME_FGLG, _choice.Template.Match.home_firstGoal_lastGoal);
        }

        [TestCase("1FL:0-1")]
        [TestCase("1FL:2-1")]
        public void BuildFGLGLabel_EventStatus_HomeFGLGIsLable1LG(string eventStatus)
        {
            // Arrange
            _ticket.EventStatus = eventStatus;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            const string HOME_FGLG = "&nbsp;<div class='fhLastGoal'>&nbsp;</div>";
            Assert.AreEqual(HOME_FGLG, _choice.Template.Match.home_firstGoal_lastGoal);
        }

        [TestCase("FL:0-1;1FL:0-1")]
        [TestCase("FL:2-1;1FL:2-1")]
        public void BuildFGLGLabel_EventStatus_HomeFGLGAreLableLGAndLable1LG(string eventStatus)
        {
            // Arrange
            _ticket.EventStatus = eventStatus;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            const string HOME_FGLG = "&nbsp;<div class='lastGoal'>&nbsp;</div><div class='fhLastGoal'>&nbsp;</div>";
            Assert.AreEqual(HOME_FGLG, _choice.Template.Match.home_firstGoal_lastGoal);
        }

        [TestCase("FL:0-2")]
        [TestCase("FL:1-2")]
        public void BuildFGLGLabel_EventStatus_AwayFGLGIsLableLG(string eventStatus)
        {
            // Arrange
            _ticket.EventStatus = eventStatus;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            const string AWAY_FGLG = "&nbsp;<div class='lastGoal'>&nbsp;</div>";
            Assert.AreEqual(AWAY_FGLG, _choice.Template.Match.away_firstGoal_lastGoal);
        }

        [TestCase("1FL:0-2")]
        [TestCase("1FL:1-2")]
        public void BuildFGLGLabel_EventStatus_AwayFGLGIsLable1LG(string eventStatus)
        {
            // Arrange
            _ticket.EventStatus = eventStatus;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            const string AWAY_FGLG = "&nbsp;<div class='fhLastGoal'>&nbsp;</div>";
            Assert.AreEqual(AWAY_FGLG, _choice.Template.Match.away_firstGoal_lastGoal);
        }

        [TestCase("FL:0-2;1FL:0-2")]
        [TestCase("FL:1-2;1FL:1-2")]
        public void BuildFGLGLabel_EventStatus_AwayFGLGAreLableLGAndLable1LG(string eventStatus)
        {
            // Arrange
            _ticket.EventStatus = eventStatus;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            const string AWAY_FGLG = "&nbsp;<div class='lastGoal'>&nbsp;</div><div class='fhLastGoal'>&nbsp;</div>";
            Assert.AreEqual(AWAY_FGLG, _choice.Template.Match.away_firstGoal_lastGoal);
        }

        [TestCase("FL:1-0")]
        [TestCase("FL:1-2")]
        public void BuildFGLGLabel_EventStatus_HomeFGLGIsLableFG(string eventStatus)
        {
            // Arrange
            _ticket.EventStatus = eventStatus;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            const string HOME_FGLG = "&nbsp;<div class='firstGoal '>&nbsp;</div>";
            Assert.AreEqual(HOME_FGLG, _choice.Template.Match.home_firstGoal_lastGoal);
        }

        [TestCase("1FL:1-0")]
        [TestCase("1FL:1-2")]
        public void BuildFGLGLabel_EventStatus_HomeFGLGIsLable1FG(string eventStatus)
        {
            // Arrange
            _ticket.EventStatus = eventStatus;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            const string HOME_FGLG = "&nbsp;<div class='fhFirstGoal'>&nbsp;</div>";
            Assert.AreEqual(HOME_FGLG, _choice.Template.Match.home_firstGoal_lastGoal);
        }

        [TestCase("FL:1-0;1FL:1-0")]
        [TestCase("FL:1-2;1FL:1-2")]
        public void BuildFGLGLabel_EventStatus_HomeFGLGAreLableFGAndLable1FG(string eventStatus)
        {
            // Arrange
            _ticket.EventStatus = eventStatus;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            const string HOME_FGLG = "&nbsp;<div class='firstGoal '>&nbsp;</div><div class='fhFirstGoal'>&nbsp;</div>";
            Assert.AreEqual(HOME_FGLG, _choice.Template.Match.home_firstGoal_lastGoal);
        }

        [Test]
        public void BuildFGLGLabel_EventStatusIsFL11_HomeFGLGAreLableFGAndLableLG()
        {
            // Arrange
            _ticket.EventStatus = "FL:1-1";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            const string HOME_FGLG = "&nbsp;<div class='firstGoal '>&nbsp;</div><div class='lastGoal'>&nbsp;</div>";
            Assert.AreEqual(HOME_FGLG, _choice.Template.Match.home_firstGoal_lastGoal);
        }

        [Test]
        public void BuildFGLGLabel_EventStatusIs1FL11_HomeFGLGAreLable1FGAndLable1LG()
        {
            // Arrange
            _ticket.EventStatus = "1FL:1-1";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            const string HOME_FGLG = "&nbsp;<div class='fhFirstGoal'>&nbsp;</div><div class='fhLastGoal'>&nbsp;</div>";
            Assert.AreEqual(HOME_FGLG, _choice.Template.Match.home_firstGoal_lastGoal);
        }

        [Test]
        public void BuildFGLGLabel_EventStatusIsFL11And1FL11_HomeFGLGAreLableFGLGAndLable1FG1LG()
        {
            // Arrange
            _ticket.EventStatus = "FL:1-1;1FL:1-1";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            const string HOME_FGLG = "&nbsp;<div class='firstGoal '>&nbsp;</div><div class='lastGoal'>&nbsp;</div>"
                                   + "<div class='fhFirstGoal'>&nbsp;</div><div class='fhLastGoal'>&nbsp;</div>";
            Assert.AreEqual(HOME_FGLG, _choice.Template.Match.home_firstGoal_lastGoal);
        }

        [Test]
        public void BuildFGLGLabel_EventStatusIsFL22_AwayFGLGAreLableFGAndLableLG()
        {
            // Arrange
            _ticket.EventStatus = "FL:2-2";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            const string AWAY_FGLG = "&nbsp;<div class='firstGoal '>&nbsp;</div><div class='lastGoal'>&nbsp;</div>";
            Assert.AreEqual(AWAY_FGLG, _choice.Template.Match.away_firstGoal_lastGoal);
        }

        [Test]
        public void BuildFGLGLabel_EventStatusIs1FL22_AwayFGLGAreLable1FGAndLable1LG()
        {
            // Arrange
            _ticket.EventStatus = "1FL:2-2";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            const string AWAY_FGLG = "&nbsp;<div class='fhFirstGoal'>&nbsp;</div><div class='fhLastGoal'>&nbsp;</div>";
            Assert.AreEqual(AWAY_FGLG, _choice.Template.Match.away_firstGoal_lastGoal);
        }

        [Test]
        public void BuildFGLGLabel_EventStatusIsFL22And1FL22_AwayFGLGAreLableFGLGAndLable1FG1LG()
        {
            // Arrange
            _ticket.EventStatus = "FL:2-2;1FL:2-2";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            const string AWAY_FGLG = "&nbsp;<div class='firstGoal '>&nbsp;</div><div class='lastGoal'>&nbsp;</div>"
                                   + "<div class='fhFirstGoal'>&nbsp;</div><div class='fhLastGoal'>&nbsp;</div>";
            Assert.AreEqual(AWAY_FGLG, _choice.Template.Match.away_firstGoal_lastGoal);
        }

        [TestCase("FL:2-0")]
        [TestCase("FL:2-1")]
        public void BuildFGLGLabel_EventStatus_AwayFGLGIsLableFG(string eventStatus)
        {
            // Arrange
            _ticket.EventStatus = eventStatus;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            const string AWAY_FGLG = "&nbsp;<div class='firstGoal '>&nbsp;</div>";
            Assert.AreEqual(AWAY_FGLG, _choice.Template.Match.away_firstGoal_lastGoal);
        }

        [TestCase("1FL:2-0")]
        [TestCase("1FL:2-1")]
        public void BuildFGLGLabel_EventStatus_AwayFGLGIsLable1FG(string eventStatus)
        {
            // Arrange
            _ticket.EventStatus = eventStatus;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            const string AWAY_FGLG = "&nbsp;<div class='fhFirstGoal'>&nbsp;</div>";
            Assert.AreEqual(AWAY_FGLG, _choice.Template.Match.away_firstGoal_lastGoal);
        }

        [TestCase("FL:2-0;1FL:2-0")]
        [TestCase("FL:2-1;1FL:2-1")]
        public void BuildFGLGLabel_EventStatus_AwayFGLGAreLableFGAndLable1FG(string eventStatus)
        {
            // Arrange
            _ticket.EventStatus = eventStatus;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            const string AWAY_FGLG = "&nbsp;<div class='firstGoal '>&nbsp;</div><div class='fhFirstGoal'>&nbsp;</div>";
            Assert.AreEqual(AWAY_FGLG, _choice.Template.Match.away_firstGoal_lastGoal);
        }

        [TestCase("")]
        [TestCase(null)]
        [TestCase("FL:2-2")]
        [TestCase("FL:2-0")]
        [TestCase("FL:0-2")]
        [TestCase("1FL:2-2")]
        [TestCase("1FL:2-0")]
        [TestCase("1FL:0-2")]
        public void BuildFGLGLabel_EventStatus_HomeFGLGIsEmpty(string eventStatus)
        {
            // Arrange
            _ticket.EventStatus = eventStatus;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsEmpty(_choice.Template.Match.home_firstGoal_lastGoal);
        }

        [TestCase("")]
        [TestCase(null)]
        [TestCase("FL:1-1")]
        [TestCase("FL:1-0")]
        [TestCase("FL:0-1")]
        [TestCase("1FL:1-1")]
        [TestCase("1FL:1-0")]
        [TestCase("1FL:0-1")]
        public void BuildFGLGLabel_EventStatus_AwayFGLGIsEmpty(string eventStatus)
        {
            // Arrange
            _ticket.EventStatus = eventStatus;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsEmpty(_choice.Template.Match.away_firstGoal_lastGoal);
        }

        [Test]
        public void BuildBetTeam_BetTeamContainsCharacterh_BetTeamIsHomeTeamName()
        {
            // Arrange
            _ticket.BetTeam = "text h";
            _ticketHelper.GetTeamNameById(Arg.Any<long>()).Returns("Home team name");

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(_choice.Template.Match.homeTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamIsNull_BetTeamIsDrawResource()
        {
            // Arrange
            _ticket.BetTeam = null;
            _ticketHelper.GetTeamNameById(Arg.Any<long>()).Returns<string>("Away team name");

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(CoreBetList.draw, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamIsEmpty_BetTeamIsDrawResource()
        {
            // Arrange
            _ticket.BetTeam = string.Empty;
            _ticketHelper.GetTeamNameById(Arg.Any<long>()).Returns<string>("Away team name");

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(CoreBetList.draw, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetBetTeamClassNameIsUnderdog()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(Underdog, _choice.Template.betTeamClassName);
        }

        [Test]
        public void BuildScore_Always_SetScoreClassNameIsFavorite()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(Favorite, _choice.Template.Score.scoreClassName);
        }

        [Test]
        public void BuildTicketStatus_Always_SetTicketStatusIsEmpty()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsEmpty(_choice.Template.ticketStatus);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetHandicapIsNull()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsNull(_choice.Template.Handicap.handicap);
        }
    }
}