namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using System;
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
            _choice = new Choice1();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.Handicap;
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
            string expectedHomeTeamName = string.Format("{0} (N) ", _ticketHelper.GetTeamNameById(_ticket.HomeId));
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
        /// The value of Template.betTeam is always Template.Match.homeTeam when bet team of ticket contains character h.
        /// </summary>
        [Test]
        public void BuildChoice_BetTeamOfTicketContainsCharacterH_BetTeamIsHomeTeamName()
        {
            // Arrange
            _ticket.BetTeam = "text h";
            _ticketHelper.GetTeamNameById(Arg.Any<long>()).Returns<string>("Home team name");

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(_choice.Template.Match.homeTeam, _choice.Template.betTeam);
        }

        /// <summary>
        /// The value of Template.betTeam is always Template.Match.awayTeam when bet team of ticket is null.
        /// </summary>
        [Test]
        public void BuildChoice_BetTeamOfTicketIsNull_BetTeamIsAwayTeamName()
        {
            // Arrange
            _ticket.BetTeam = null;
            _ticketHelper.GetTeamNameById(Arg.Any<long>()).Returns<string>("Away team name");

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(_choice.Template.Match.awayTeam, _choice.Template.betTeam);
        }

        /// <summary>
        /// The value of Template.betTeam is always Template.Match.awayTeam when bet team of ticket is empty or not contains character h.
        /// </summary>
        [Test]
        public void BuildChoice_BetTeamOfTicketIsEmpty_BetTeamIsAwayTeamName()
        {
            // Arrange
            _ticket.BetTeam = string.Empty;
            _ticketHelper.GetTeamNameById(Arg.Any<long>()).Returns<string>("Away team name");

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(_choice.Template.Match.awayTeam, _choice.Template.betTeam);
        }

        /// <summary>
        /// The value of Template.betTeamClassName is always underdog when bet team of ticket contains character h and <![CDATA[Handicap2 > Handicap 1]]>.
        /// </summary>
        [Test]
        public void BuildChoice_BetTeamOfTicketContainsCharacterHAndHandicap2GreaterThanHandicap1_BetTeamClassNameIsUnderdog()
        {
            // Arrange
            _ticket.BetTeam = "text h";
            _ticket.Handicap1 = 1;
            _ticket.Handicap2 = 2;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(Underdog, _choice.Template.betTeamClassName);
        }

        /// <summary>
        /// The value of Template.betTeamClassName is always underdog when bet team of ticket contains character h and <![CDATA[Handicap2 = Handicap 1]]>.
        /// </summary>
        [Test]
        public void BuildChoice_BetTeamOfTicketContainsCharacterHAndHandicap2EqualHandicap1_BetTeamClassNameIsUnderdog()
        {
            // Arrange
            _ticket.BetTeam = "text h";
            _ticket.Handicap1 = 1;
            _ticket.Handicap2 = 1;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(Underdog, _choice.Template.betTeamClassName);
        }

        /// <summary>
        /// The value of Template.betTeamClassName is always favorite when bet team of ticket contains character h and <![CDATA[Handicap2 < Handicap 1]]>.
        /// </summary>
        [Test]
        public void BuildChoice_BetTeamOfTicketContainsCharacterHAndHandicap2LessThanHandicap1_BetTeamClassNameIsFavorite()
        {
            // Arrange
            _ticket.BetTeam = "text h";
            _ticket.Handicap1 = 2;
            _ticket.Handicap2 = 1;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(Favorite, _choice.Template.betTeamClassName);
        }

        /// <summary>
        /// The value of Template.betTeamClassName is always underdog when bet team of ticket not contains character h and <![CDATA[Handicap2 < Handicap 1]]>.
        /// </summary>
        [Test]
        public void BuildChoice_BetTeamOfTicketNotContainsCharacterHAndHandicap2LessThanHandicap1_BetTeamClassNameIsUnderdog()
        {
            // Arrange
            _ticket.BetTeam = string.Empty;
            _ticket.Handicap1 = 2;
            _ticket.Handicap2 = 1;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(Underdog, _choice.Template.betTeamClassName);
        }

        /// <summary>
        /// The value of Template.betTeamClassName is always underdog when bet team of ticket not contains character h and <![CDATA[Handicap2 = Handicap 1]]>.
        /// </summary>
        [Test]
        public void BuildChoice_BetTeamOfTicketNotContainsCharacterHAndHandicap2EqualHandicap1_BetTeamClassNameIsUnderdog()
        {
            // Arrange
            _ticket.BetTeam = string.Empty;
            _ticket.Handicap1 = 1;
            _ticket.Handicap2 = 1;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(Underdog, _choice.Template.betTeamClassName);
        }

        /// <summary>
        /// The value of Template.betTeamClassName is always favorite when bet team of ticket not contains character h and <![CDATA[Handicap2 > Handicap 1]]>.
        /// </summary>
        [Test]
        public void BuildChoice_BetTeamOfTicketNotContainsCharacterHAndHandicap2GreaterThanHandicap1_BetTeamClassNameIsFavorite()
        {
            // Arrange
            _ticket.BetTeam = string.Empty;
            _ticket.Handicap1 = 1;
            _ticket.Handicap2 = 2;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(Favorite, _choice.Template.betTeamClassName);
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
        /// The value of Template.Handicap.handicap is always handicap2 when bet team of ticket contains character h and <![CDATA[Handicap2 > Handicap1]]>.
        /// </summary>
        [Test]
        public void BuildChoice_BetTeamOfTicketContainsCharacterHAndHandicap2GreaterThanHandicap1_HandicapIsHandicap2()
        {
            // Arrange
            _ticket.BetTeam = "text h";
            _ticket.Handicap1 = 1;
            _ticket.Handicap2 = 2;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(_ticket.Handicap2.ToString(), _choice.Template.Handicap.handicap);
        }

        /// <summary>
        /// The value of Template.Handicap.handicap is always -handicap1 when bet team of ticket contains character h and <![CDATA[Handicap2 < Handicap1]]>.
        /// </summary>
        [Test]
        public void BuildChoice_BetTeamOfTicketContainsCharacterHAndHandicap2LessThanHandicap1_HandicapIsMinusHandicap1()
        {
            // Arrange
            _ticket.BetTeam = "text h";
            _ticket.Handicap1 = 2;
            _ticket.Handicap2 = 1;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedHandicap = string.Format("-{0}", _ticket.Handicap1);
            Assert.AreEqual(expectedHandicap, _choice.Template.Handicap.handicap);
        }

        /// <summary>
        /// The value of Template.ticketStatus is always empty when Ticket.StatusId = null.
        /// </summary>
        [Test]
        public void BuildChoice_StatusIdOfTicketIsNull_TicketStatusIsEmpty()
        {
            // Arrange
            _ticket.StatusId = null;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsEmpty(_choice.Template.ticketStatus);
        }

        /// <summary>
        /// The value of Template.ticketStatus is always void when Ticket.StatusId = 102 (Void status identifier).
        /// </summary>
        [Test]
        public void BuildChoice_StatusIdOfTicketIsVoid_TicketStatusIsVoid()
        {
            // Arrange
            _ticket.StatusId = VoidStatusId;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(VoidStatus, _choice.Template.ticketStatus);
        }

        /// <summary>
        /// The value of Template.ticketStatus is always empty when Ticket.Status = null.
        /// </summary>
        [Test]
        public void BuildChoice_StatusOfTicketIsNull_TicketStatusIsEmpty()
        {
            // Arrange
            _ticket.Status = null;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsEmpty(_choice.Template.ticketStatus);
        }

        /// <summary>
        /// The value of Template.ticketStatus is always void when Ticket.Status = void.
        /// </summary>
        [Test]
        public void BuildChoice_StatusOfTicketIsVoid_TicketStatusIsVoid()
        {
            // Arrange
            _ticket.Status = VoidStatus;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(VoidStatus, _choice.Template.ticketStatus);
        }

        /// <summary>
        /// The value of Template.Handicap.Odds is always null when TicketHelper.IsShowOddsWithChoice = false.
        /// </summary>
        [Test]
        public void BuildHandicapOdds_IsShowOddsWithChoiceOfTicketHelperIsFalse_HandicapOddsIsNull()
        {
            // Arrange
            _ticketHelper.IsShowOddsWithChoice = false;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNull(_choice.Template.Handicap.Odds);
        }

        /// <summary>
        /// The value of Template.Handicap.Odds is always Ticket.Odds when TicketHelper.IsShowOddsWithChoice = true.
        /// </summary>
        [Test]
        public void BuildHandicapOdds_IsShowOddsWithChoiceOfTicketHelperIsTrueAndOddsTypeTicketIsEmpty_HandicapOddsIsOdds()
        {
            // Arrange
            _ticketHelper.IsShowOddsWithChoice = true;
            _ticket.OddsType = string.Empty;
            _ticket.Odds = 1;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedOdds = _ticket.Odds.ToString();
            Assert.AreEqual(expectedOdds, _choice.Template.Handicap.Odds.odds);
        }

        /// <summary>
        /// The value of Template.Handicap.Odds is always null when TicketHelper.IsShowOddsWithChoice = true and OddsType is not BetOdds Or Odds = -1.
        /// </summary>
        [Test]
        public void BuildHandicapOdds_IsShowOddsWithChoiceOfTicketHelperIsTrue_HandicapOddsIsNull()
        {
            // Arrange
            _ticketHelper.IsShowOddsWithChoice = true;
            _ticket.OddsType = "1";
            _ticket.Odds = -1;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNull(_choice.Template.Handicap.Odds);
        }

        /// <summary>
        /// The value of Template.ScoreMapIcon.matchId is always Ticket.MatchId when score map icon be showed.
        /// </summary>
        [Test]
        public void BuildScoreMap_IsShowScoreMap_ScoreMapIconMatchIdIsMatchId()
        {
            // Arrange
            _ticket.MatchId = 1;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, true);

            // Assert
            Assert.AreEqual(_ticket.MatchId.ToString(), _choice.Template.ScoreMapIcon.matchId);
        }

        /// <summary>
        /// The value of Template.ScoreMapIcon.betTypeId is always Ticket.BetTypeId when score map icon be showed.
        /// </summary>
        [Test]
        public void BuildScoreMap_IsShowScoreMap_ScoreMapIconBetTypeIsBetTypeId()
        {
            // Arrange
            _ticket.BetTypeId = 1;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, true);

            // Assert
            Assert.AreEqual(_ticket.BetTypeId.ToString(), _choice.Template.ScoreMapIcon.betTypeId);
        }

        /// <summary>
        /// The value of Template.ScoreMapIcon.LiveIndicator is always Ticket.IsLive when score map icon be showed.
        /// </summary>
        [Test]
        public void BuildScoreMap_IsShowScoreMap_ScoreMapIconLiveIndicatorIsIsLive()
        {
            // Arrange
            _ticket.IsLive = true;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, true);

            // Assert
            string expectedLiveIndicator = Convert.ToInt32(_ticket.IsLive).ToString();
            Assert.AreEqual(expectedLiveIndicator, _choice.Template.ScoreMapIcon.liveindicator);
        }

        /// <summary>
        /// The value of Template.ScoreMapIcon is always null when IsNotShowScoreMap = false.
        /// </summary>
        [Test]
        public void BuildScoreMap_IsNotShowScoreMap_ScoreMapIconIsNull()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsNull(_choice.Template.ScoreMapIcon);
        }
    }
}