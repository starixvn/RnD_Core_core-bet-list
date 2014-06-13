namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.UnitTest.Common.Enums;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Unit test for Choice123 class.
    /// </summary>
    [TestFixture]
    public class Choice123Test
    {
        private const string LabelFirstGoal = "<div class='firstGoal '>&nbsp;</div>";
        private const string LabelLastGoal = "<div class='lastGoal'>&nbsp;</div>";
        private const string Label1FirstGoal = "<div class='fhFirstGoal'>&nbsp;</div>";
        private const string Label1LastGoal = "<div class='fhLastGoal'>&nbsp;</div>";
        private const string Space = "&nbsp;";

        /// <summary>
        /// Underdog = "underdog".
        /// </summary>
        private const string Underdog = "underdog";

        /// <summary>
        /// Favorite = "favorite".
        /// </summary>
        private const string Favorite = "favorite";

        /// <summary>
        /// VoidStatusId = 102.
        /// </summary>
        private const int VoidStatusId = 102;

        /// <summary>
        /// VoidStatus = "void".
        /// </summary>
        private const string VoidStatus = "void";

        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice123();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.Draw_NoDraw;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        /// <summary>
        /// The value of Template.Score.homeScore is always Ticket.LiveHomeScore.
        /// </summary>
        [Test]
        public void BuildChoice_Ticket_HomeScoreIsLiveHomeScore()
        {
            // Arrange
            _ticket.LiveHomeScore = 1;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(_ticket.LiveHomeScore.ToString(), _choice.Template.Score.homeScore);
        }

        /// <summary>
        /// The value of Template.Score.awayScore is always Ticket.LiveAwayScore.
        /// </summary>
        [Test]
        public void BuildChoice_Ticket_AwayScoreIsLiveAwayScore()
        {
            // Arrange
            _ticket.LiveAwayScore = 1;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(_ticket.LiveAwayScore.ToString(), _choice.Template.Score.awayScore);
        }

        /// <summary>
        /// The value of Template.Score.Visible is always Ticket.IsLive.
        /// </summary>
        /// <param name="isLive">If set to <c>true</c> [is live].</param>
        [TestCase(true)]
        [TestCase(false)]
        public void BuildChoice_Ticket_ScoreVisibleEqualTicketIsLive(bool isLive)
        {
            // Arrange
            _ticket.IsLive = isLive;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(_ticket.IsLive, _choice.Template.Score.Visible);
        }

        /// <summary>
        /// The value of Template.League.LeagueName.leagueName is always league name get by identifier.
        /// </summary>
        [Test]
        public void BuildChoice_Ticket_LeagueNameIsLeagueName()
        {
            // Arrange
            _ticketHelper.GetLeagueNameById(Arg.Any<int>()).Returns<string>("League name");

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedLeagueName = _ticketHelper.GetLeagueNameById(_ticket.LeagueId);
            Assert.AreEqual(expectedLeagueName, _choice.Template.League.LeagueName.leagueName);
        }

        /// <summary>
        /// The value of Template.BetType.betTypeName is always bet type name get by identifier.
        /// </summary>
        [Test]
        public void BuildChoice_Ticket_BetTypeIsBetTypeName()
        {
            // Arrange
            _ticketHelper.GetBetTypeNameById(Arg.Any<int>()).Returns<string>("Bet type name");

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedBetTypeName = _ticketHelper.GetBetTypeNameById(_ticket.BetTypeId);
            Assert.AreEqual(expectedBetTypeName, _choice.Template.BetType.betTypeName);
        }

        /// <summary>
        /// The value of Template.Match.homeTeam is always [HomeTeamName (N)] if the ticket is neutral.
        /// </summary>
        [Test]
        public void BuildChoice_NeutralTicket_HomeTeamAreHomeTeamNameAndSpaceAndCharacterNWithinParenthese()
        {
            // Arrange
            _ticket.IsNeutral = true;
            _ticketHelper.GetTeamNameById(Arg.Any<long>()).Returns<string>("Home team name");

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedHomeTeamName = string.Format("{0} (N)", _ticketHelper.GetTeamNameById(_ticket.HomeId));
            Assert.AreEqual(expectedHomeTeamName, _choice.Template.Match.homeTeam);
        }

        /// <summary>
        /// The value of Template.Match.homeTeam is always [HomeTeamName] if the ticket is not neutral.
        /// </summary>
        [Test]
        public void BuildChoice_NotNeutralTicket_HomeTeamIsHomeTeamName()
        {
            // Arrange
            _ticket.IsNeutral = false;
            _ticketHelper.GetTeamNameById(Arg.Any<long>()).Returns<string>("Home team name");

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedHomeTeamName = _ticketHelper.GetTeamNameById(_ticket.HomeId);
            Assert.AreEqual(expectedHomeTeamName, _choice.Template.Match.homeTeam);
        }

        /// <summary>
        /// The value of Template.Match.awayTeam is always away team name.
        /// </summary>
        [Test]
        public void BuildChoice_Ticket_AwayTeamIsAwayTeamName()
        {
            // Arrange
            _ticketHelper.GetTeamNameById(Arg.Any<long>()).Returns<string>("Away team name");

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedAwayTeamName = _ticketHelper.GetTeamNameById(_ticket.AwayId);
            Assert.AreEqual(expectedAwayTeamName, _choice.Template.Match.awayTeam);
        }

        /// <summary>
        /// The value of Template.Match.home_firstGoal_lastGoal is always label (L) with these event status.
        /// </summary>
        /// <param name="eventStatus">The event status.</param>
        [TestCase("FL:0-1")]
        [TestCase("FL:2-1")]
        public void CalculateFGLG_EventStatusOfTicket_HomeFGLGIsLableLG(string eventStatus)
        {
            // Arrange
            _ticket.EventStatus = eventStatus;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedHomeFGLG = string.Format("{0}{1}", Space, LabelLastGoal);
            Assert.AreEqual(expectedHomeFGLG, _choice.Template.Match.home_firstGoal_lastGoal);
        }

        /// <summary>
        /// The value of Template.Match.home_firstGoal_lastGoal is always label (1L) with these event status.
        /// </summary>
        /// <param name="eventStatus">The event status.</param>
        [TestCase("1FL:0-1")]
        [TestCase("1FL:2-1")]
        public void CalculateFGLG_EventStatusOfTicket_HomeFGLGIsLable1LG(string eventStatus)
        {
            // Arrange
            _ticket.EventStatus = eventStatus;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedHomeFGLG = string.Format("{0}{1}", Space, Label1LastGoal);
            Assert.AreEqual(expectedHomeFGLG, _choice.Template.Match.home_firstGoal_lastGoal);
        }

        /// <summary>
        /// The value of Template.Match.home_firstGoal_lastGoal is always label (L)(1L) with these event status.
        /// </summary>
        /// <param name="eventStatus">The event status.</param>
        [TestCase("FL:0-1;1FL:0-1")]
        [TestCase("FL:2-1;1FL:2-1")]
        public void CalculateFGLG_EventStatusOfTicket_HomeFGLGAreLableLGAndLable1LG(string eventStatus)
        {
            // Arrange
            _ticket.EventStatus = eventStatus;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedHomeFGLG = string.Format("{0}{1}{2}", Space, LabelLastGoal, Label1LastGoal);
            Assert.AreEqual(expectedHomeFGLG, _choice.Template.Match.home_firstGoal_lastGoal);
        }

        /// <summary>
        /// The value of Template.Match.away_firstGoal_lastGoal is always label (L) with these event status.
        /// </summary>
        /// <param name="eventStatus">The event status.</param>
        [TestCase("FL:0-2")]
        [TestCase("FL:1-2")]
        public void CalculateFGLG_EventStatusOfTicket_AwayFGLGIsLableLG(string eventStatus)
        {
            // Arrange
            _ticket.EventStatus = eventStatus;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedAwayFGLG = string.Format("{0}{1}", Space, LabelLastGoal);
            Assert.AreEqual(expectedAwayFGLG, _choice.Template.Match.away_firstGoal_lastGoal);
        }

        /// <summary>
        /// The value of Template.Match.away_firstGoal_lastGoal is always label (1L) with these event status.
        /// </summary>
        /// <param name="eventStatus">The event status.</param>
        [TestCase("1FL:0-2")]
        [TestCase("1FL:1-2")]
        public void CalculateFGLG_EventStatusOfTicket_AwayFGLGIsLable1LG(string eventStatus)
        {
            // Arrange
            _ticket.EventStatus = eventStatus;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedAwayFGLG = string.Format("{0}{1}", Space, Label1LastGoal);
            Assert.AreEqual(expectedAwayFGLG, _choice.Template.Match.away_firstGoal_lastGoal);
        }

        /// <summary>
        /// The value of Template.Match.away_firstGoal_lastGoal is always label (L)(1L) with these event status.
        /// </summary>
        /// <param name="eventStatus">The event status.</param>
        [TestCase("FL:0-2;1FL:0-2")]
        [TestCase("FL:1-2;1FL:1-2")]
        public void CalculateFGLG_EventStatusOfTicket_AwayFGLGAreLableLGAndLable1LG(string eventStatus)
        {
            // Arrange
            _ticket.EventStatus = eventStatus;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedAwayFGLG = string.Format("{0}{1}{2}", Space, LabelLastGoal, Label1LastGoal);
            Assert.AreEqual(expectedAwayFGLG, _choice.Template.Match.away_firstGoal_lastGoal);
        }

        /// <summary>
        /// The value of Template.Match.home_firstGoal_lastGoal is always label (F) with these event status.
        /// </summary>
        /// <param name="eventStatus">The event status.</param>
        [TestCase("FL:1-0")]
        [TestCase("FL:1-2")]
        public void CalculateFGLG_EventStatusOfTicket_HomeFGLGIsLableFG(string eventStatus)
        {
            // Arrange
            _ticket.EventStatus = eventStatus;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedHomeFGLG = string.Format("{0}{1}", Space, LabelFirstGoal);
            Assert.AreEqual(expectedHomeFGLG, _choice.Template.Match.home_firstGoal_lastGoal);
        }

        /// <summary>
        /// The value of Template.Match.home_firstGoal_lastGoal is always label (1F) with these event status.
        /// </summary>
        /// <param name="eventStatus">The event status.</param>
        [TestCase("1FL:1-0")]
        [TestCase("1FL:1-2")]
        public void CalculateFGLG_EventStatusOfTicket_HomeFGLGIsLable1FG(string eventStatus)
        {
            // Arrange
            _ticket.EventStatus = eventStatus;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedHomeFGLG = string.Format("{0}{1}", Space, Label1FirstGoal);
            Assert.AreEqual(expectedHomeFGLG, _choice.Template.Match.home_firstGoal_lastGoal);
        }

        /// <summary>
        /// The value of Template.Match.home_firstGoal_lastGoal is always label (F)(1F) with these event status.
        /// </summary>
        /// <param name="eventStatus">The event status.</param>
        [TestCase("FL:1-0;1FL:1-0")]
        [TestCase("FL:1-2;1FL:1-2")]
        public void CalculateFGLG_EventStatusOfTicket_HomeFGLGAreLableFGAndLable1FG(string eventStatus)
        {
            // Arrange
            _ticket.EventStatus = eventStatus;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedHomeFGLG = string.Format("{0}{1}{2}", Space, LabelFirstGoal, Label1FirstGoal);
            Assert.AreEqual(expectedHomeFGLG, _choice.Template.Match.home_firstGoal_lastGoal);
        }

        /// <summary>
        /// The value of Template.Match.home_firstGoal_lastGoal is always label (F)(L) with these event status.
        /// </summary>
        [Test]
        public void CalculateFGLG_EventStatusOfTicketIsFL11_HomeFGLGAreLableFGAndLableLG()
        {
            // Arrange
            _ticket.EventStatus = "FL:1-1";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedHomeFGLG = string.Format("{0}{1}{2}", Space, LabelFirstGoal, LabelLastGoal);
            Assert.AreEqual(expectedHomeFGLG, _choice.Template.Match.home_firstGoal_lastGoal);
        }

        /// <summary>
        /// The value of Template.Match.home_firstGoal_lastGoal is always label (1F)(1L) with these event status.
        /// </summary>
        [Test]
        public void CalculateFGLG_EventStatusOfTicketIs1FL11_HomeFGLGAreLable1FGAndLable1LG()
        {
            // Arrange
            _ticket.EventStatus = "1FL:1-1";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedHomeFGLG = string.Format("{0}{1}{2}", Space, Label1FirstGoal, Label1LastGoal);
            Assert.AreEqual(expectedHomeFGLG, _choice.Template.Match.home_firstGoal_lastGoal);
        }

        /// <summary>
        /// The value of Template.Match.home_firstGoal_lastGoal is always label (F)(L)(1F)(1L) with these event status.
        /// </summary>
        [Test]
        public void CalculateFGLG_EventStatusOfTicketIsFL11And1FL11_HomeFGLGAreLableFGLGAndLable1FG1LG()
        {
            // Arrange
            _ticket.EventStatus = "FL:1-1;1FL:1-1";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedHomeFGLG = string.Format("{0}{1}{2}{3}{4}", Space, LabelFirstGoal, LabelLastGoal, Label1FirstGoal, Label1LastGoal);
            Assert.AreEqual(expectedHomeFGLG, _choice.Template.Match.home_firstGoal_lastGoal);
        }

        /// <summary>
        /// The value of Template.Match.away_firstGoal_lastGoal is always label (F)(L) with these event status.
        /// </summary>
        [Test]
        public void CalculateFGLG_EventStatusOfTicketIsFL22_AwayFGLGAreLableFGAndLableLG()
        {
            // Arrange
            _ticket.EventStatus = "FL:2-2";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedAwayFGLG = string.Format("{0}{1}{2}", Space, LabelFirstGoal, LabelLastGoal);
            Assert.AreEqual(expectedAwayFGLG, _choice.Template.Match.away_firstGoal_lastGoal);
        }

        /// <summary>
        /// The value of Template.Match.away_firstGoal_lastGoal is always label (1F)(1L) with these event status.
        /// </summary>
        [Test]
        public void CalculateFGLG_EventStatusOfTicketIs1FL22_AwayFGLGAreLable1FGAndLable1LG()
        {
            // Arrange
            _ticket.EventStatus = "1FL:2-2";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedAwayFGLG = string.Format("{0}{1}{2}", Space, Label1FirstGoal, Label1LastGoal);
            Assert.AreEqual(expectedAwayFGLG, _choice.Template.Match.away_firstGoal_lastGoal);
        }

        /// <summary>
        /// The value of Template.Match.away_firstGoal_lastGoal is always label (F)(L)(1F)(1L) with these event status.
        /// </summary>
        [Test]
        public void CalculateFGLG_EventStatusOfTicketIsFL22And1FL22_AwayFGLGAreLableFGLGAndLable1FG1LG()
        {
            // Arrange
            _ticket.EventStatus = "FL:2-2;1FL:2-2";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedAwayFGLG = string.Format("{0}{1}{2}{3}{4}", Space, LabelFirstGoal, LabelLastGoal, Label1FirstGoal, Label1LastGoal);
            Assert.AreEqual(expectedAwayFGLG, _choice.Template.Match.away_firstGoal_lastGoal);
        }

        /// <summary>
        /// The value of Template.Match.away_firstGoal_lastGoal is always label (F) with these event status.
        /// </summary>
        /// <param name="eventStatus">The event status.</param>
        [TestCase("FL:2-0")]
        [TestCase("FL:2-1")]
        public void CalculateFGLG_EventStatusOfTicket_AwayFGLGIsLableFG(string eventStatus)
        {
            // Arrange
            _ticket.EventStatus = eventStatus;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedAwayFGLG = string.Format("{0}{1}", Space, LabelFirstGoal);
            Assert.AreEqual(expectedAwayFGLG, _choice.Template.Match.away_firstGoal_lastGoal);
        }

        /// <summary>
        /// The value of Template.Match.away_firstGoal_lastGoal is always label (1F) with these event status.
        /// </summary>
        /// <param name="eventStatus">The event status.</param>
        [TestCase("1FL:2-0")]
        [TestCase("1FL:2-1")]
        public void CalculateFGLG_EventStatusOfTicket_AwayFGLGIsLable1FG(string eventStatus)
        {
            // Arrange
            _ticket.EventStatus = eventStatus;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedAwayFGLG = string.Format("{0}{1}", Space, Label1FirstGoal);
            Assert.AreEqual(expectedAwayFGLG, _choice.Template.Match.away_firstGoal_lastGoal);
        }

        /// <summary>
        /// The value of Template.Match.away_firstGoal_lastGoal is always label (F)(1L) with these event status.
        /// </summary>
        /// <param name="eventStatus">The event status.</param>
        [TestCase("FL:2-0;1FL:2-0")]
        [TestCase("FL:2-1;1FL:2-1")]
        public void CalculateFGLG_EventStatusOfTicket_AwayFGLGAreLableFGAndLable1FG(string eventStatus)
        {
            // Arrange
            _ticket.EventStatus = eventStatus;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedAwayFGLG = string.Format("{0}{1}{2}", Space, LabelFirstGoal, Label1FirstGoal);
            Assert.AreEqual(expectedAwayFGLG, _choice.Template.Match.away_firstGoal_lastGoal);
        }

        /// <summary>
        /// The value of Template.Match.home_firstGoal_lastGoal is always empty with these event status.
        /// </summary>
        /// <param name="eventStatus">The event status.</param>
        [TestCase("")]
        [TestCase(null)]
        [TestCase("FL:2-2")]
        [TestCase("FL:2-0")]
        [TestCase("FL:0-2")]
        [TestCase("1FL:2-2")]
        [TestCase("1FL:2-0")]
        [TestCase("1FL:0-2")]
        public void CalculateFGLG_EventStatusOfTicket_HomeFGLGIsEmpty(string eventStatus)
        {
            // Arrange
            _ticket.EventStatus = eventStatus;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsEmpty(_choice.Template.Match.home_firstGoal_lastGoal);
        }

        /// <summary>
        /// The value of Template.Match.away_firstGoal_lastGoal is always empty with these event status.
        /// </summary>
        /// <param name="eventStatus">The event status.</param>
        [TestCase("")]
        [TestCase(null)]
        [TestCase("FL:1-1")]
        [TestCase("FL:1-0")]
        [TestCase("FL:0-1")]
        [TestCase("1FL:1-1")]
        [TestCase("1FL:1-0")]
        [TestCase("1FL:0-1")]
        public void CalculateFGLG_EventStatusOfTicket_AwayFGLGIsEmpty(string eventStatus)
        {
            // Arrange
            _ticket.EventStatus = eventStatus;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsEmpty(_choice.Template.Match.away_firstGoal_lastGoal);
        }

        /// <summary>
        /// The value of Template.betTeam is always Draw when bet team of ticket contains character h.
        /// </summary>
        [Test]
        public void BuildChoice_BetTeamOfTicketEqualCharacterH_BetTeamIsDrawResource()
        {
            // Arrange
            _ticket.BetTeam = "h";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(CoreLang.lblDraw, _choice.Template.betTeam);
        }

        /// <summary>
        /// The value of Template.betTeam is always No Draw when bet team of ticket is null.
        /// </summary>
        [Test]
        public void BuildChoice_BetTeamOfTicketIsNull_BetTeamIsNoDrawResource()
        {
            // Arrange
            _ticket.BetTeam = null;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(CoreLang.lblNoDraw, _choice.Template.betTeam);
        }

        /// <summary>
        /// The value of Template.betTeam is always No Draw when bet team of ticket is empty or not contains character h.
        /// </summary>
        [Test]
        public void BuildChoice_BetTeamOfTicketIsEmpty_BetTeamIsNoDrawResource()
        {
            // Arrange
            _ticket.BetTeam = string.Empty;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(CoreLang.lblNoDraw, _choice.Template.betTeam);
        }

        /// <summary>
        /// The value of Template.betTeamClassName is always underdog.
        /// </summary>
        [Test]
        public void BuildChoice_Ticket_BetTeamClassNameIsUnderdog()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(Underdog, _choice.Template.betTeamClassName);
        }

        /// <summary>
        /// The value of Template.Score.scoreClassName is always favorite.
        /// </summary>
        [Test]
        public void BuildChoice_Ticket_ScoreClassNameIsFavorite()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(Favorite, _choice.Template.Score.scoreClassName);
        }

        /// <summary>
        /// The value of Template.ticketStatus is always empty when Ticket.Status = null.
        /// </summary>
        [Test]
        public void BuildChoice_Ticket_TicketStatusIsEmpty()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsEmpty(_choice.Template.ticketStatus);
        }

        /// <summary>
        /// The value of Template.ticketStatus is always void when Ticket.Status = void.
        /// </summary>
        [Test]
        public void BuildChoice_Ticket_HandicapIsNull()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNull(_choice.Template.Handicap.handicap);
        }
    }
}