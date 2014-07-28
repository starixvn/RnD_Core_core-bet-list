namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Common.Enums;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Unit test for Choice1309 class.
    /// </summary>
    [TestFixture]
    public class Choice1309Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice1309();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.MatchHandicapGames3Way;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        /// <summary>
        /// The value of Template.betTeam is always home team name.
        /// </summary>
        [Test]
        public void BuildBetTeam_BetTeamIs1_BetTeamIsHomeTeamName()
        {
            // Arrange
            const string HOME_TEAM_NAME = "Home team name";
            _ticket.BetTeam = "1";
            _ticket.HomeId = 1;
            _ticketHelper.GetTeamNameById(Arg.Any<long>()).Returns<string>(HOME_TEAM_NAME);

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(_choice.Template.Match.homeTeam, _choice.Template.betTeam);
        }

        /// <summary>
        /// The value of Template.betTeamClassName is underdog.
        /// </summary>
        [Test]
        public void BuildBetTeamClassNameAndHandicap_BetTeamIs1AndHandicap2GreaterThanHandicap1_BetTeamClassNameIsUnderdog()
        {
            // Arrange
            _ticket.BetTeam = "1";
            _ticket.Handicap2 = 2;
            _ticket.Handicap1 = 1;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            const string CSS_CLASS_UNDERDOG = "underdog";
            Assert.AreEqual(CSS_CLASS_UNDERDOG, _choice.Template.betTeamClassName);
        }

        /// <summary>
        /// The value of Template.betTeamClassName is underdog.
        /// </summary>
        [Test]
        public void BuildBetTeamClassNameAndHandicap_BetTeamIs1AndHandicap2EqualHandicap1_BetTeamClassNameIsUnderdog()
        {
            // Arrange
            _ticket.BetTeam = "1";
            _ticket.Handicap2 = 1;
            _ticket.Handicap1 = 1;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            const string CSS_CLASS_UNDERDOG = "underdog";
            Assert.AreEqual(CSS_CLASS_UNDERDOG, _choice.Template.betTeamClassName);
        }

        /// <summary>
        /// The value of Template.betTeamClassName is underdog.
        /// </summary>
        [Test]
        public void BuildBetTeamClassNameAndHandicap_BetTeamIs1AndHandicap2LessThanHandicap1_BetTeamClassNameIsFavorite()
        {
            // Arrange
            _ticket.BetTeam = "1";
            _ticket.Handicap2 = 1;
            _ticket.Handicap1 = 2;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            const string CSS_CLASS_FAVORITE = "favorite";
            Assert.AreEqual(CSS_CLASS_FAVORITE, _choice.Template.betTeamClassName);
        }

        /// <summary>
        /// The value of Template.betTeamClassName is underdog.
        /// </summary>
        [Test]
        public void BuildBetTeamClassNameAndHandicap_BetTeamIs1AndHandicap2LessThanHandicap1_HandicapIsMinusHandicap1()
        {
            // Arrange
            _ticket.BetTeam = "1";
            _ticket.Handicap2 = 1;
            _ticket.Handicap1 = 2;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            var expectedHandicap = "-2";
            Assert.AreEqual(expectedHandicap, _choice.Template.Handicap.handicap);
        }

        /// <summary>
        /// The value of Template.betTeamClassName is underdog.
        /// </summary>
        [Test]
        public void BuildBetTeamClassNameAndHandicap_BetTeamIs1AndHandicap2GreaterThanHandicap1_HandicapIsHandicap2()
        {
            // Arrange
            _ticket.BetTeam = "1";
            _ticket.Handicap2 = 2;
            _ticket.Handicap1 = 1;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            var expectedHandicap = "2";
            Assert.AreEqual(expectedHandicap, _choice.Template.Handicap.handicap);
        }

        /// <summary>
        /// The value of Template.betTeam is always away team name.
        /// </summary>
        [Test]
        public void BuildBetTeam_BetTeamIs2_BetTeamIsAwayTeamName()
        {
            // Arrange
            const string AWAY_TEAM_NAME = "Away team name";
            _ticket.BetTeam = "2";
            _ticket.AwayId = 1;
            _ticketHelper.GetTeamNameById(Arg.Any<long>()).Returns<string>(AWAY_TEAM_NAME);

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(AWAY_TEAM_NAME, _choice.Template.betTeam);
        }

        /// <summary>
        /// The value of Template.betTeamClassName is favorite.
        /// </summary>
        [Test]
        public void BuildBetTeamClassNameAndHandicap_BetTeamIs2AndHandicap2GreaterThanHandicap1_BetTeamClassNameIsFavorite()
        {
            // Arrange
            _ticket.BetTeam = "2";
            _ticket.Handicap2 = 2;
            _ticket.Handicap1 = 1;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            const string CSS_CLASS_FAVORITE = "favorite";
            Assert.AreEqual(CSS_CLASS_FAVORITE, _choice.Template.betTeamClassName);
        }

        /// <summary>
        /// The value of Template.betTeamClassName is underdog.
        /// </summary>
        [Test]
        public void BuildBetTeamClassNameAndHandicap_BetTeamIs2AndHandicap2EqualHandicap1_BetTeamClassNameIsUnderdog()
        {
            // Arrange
            _ticket.BetTeam = "2";
            _ticket.Handicap2 = 1;
            _ticket.Handicap1 = 1;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            const string CSS_CLASS_UNDERDOG = "underdog";
            Assert.AreEqual(CSS_CLASS_UNDERDOG, _choice.Template.betTeamClassName);
        }

        /// <summary>
        /// The value of Template.betTeamClassName is underdog.
        /// </summary>
        [Test]
        public void BuildBetTeamClassNameAndHandicap_BetTeamIs2AndHandicap2LessThanHandicap1_BetTeamClassNameIsUnderdog()
        {
            // Arrange
            _ticket.BetTeam = "2";
            _ticket.Handicap2 = 1;
            _ticket.Handicap1 = 2;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            const string CSS_CLASS_UNDERDOG = "underdog";
            Assert.AreEqual(CSS_CLASS_UNDERDOG, _choice.Template.betTeamClassName);
        }

        /// <summary>
        /// The value of Template.betTeamClassName is underdog.
        /// </summary>
        [Test]
        public void BuildBetTeamClassNameAndHandicap_BetTeamIs2AndHandicap2LessThanHandicap1_HandicapIsHandicap1()
        {
            // Arrange
            _ticket.BetTeam = "2";
            _ticket.Handicap2 = 1;
            _ticket.Handicap1 = 2;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            var expectedHandicap = "2";
            Assert.AreEqual(expectedHandicap, _choice.Template.Handicap.handicap);
        }

        /// <summary>
        /// The value of Template.betTeamClassName is underdog.
        /// </summary>
        [Test]
        public void BuildBetTeamClassNameAndHandicap_BetTeamIs2AndHandicap2GreaterThanHandicap1_HandicapIsMinusHandicap2()
        {
            // Arrange
            _ticket.BetTeam = "2";
            _ticket.Handicap2 = 2;
            _ticket.Handicap1 = 1;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            var expectedHandicap = "-2";
            Assert.AreEqual(expectedHandicap, _choice.Template.Handicap.handicap);
        }

        /// <summary>
        /// The value of Template.betTeamClassName is underdog.
        /// </summary>
        [Test]
        public void BuildBetTeamClassNameAndHandicap_BetTeamNotIs1AndNotIs2_BetTeamClassNameIsUnderdog()
        {
            // Arrange
            _ticket.BetTeam = "bet team";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            const string CSS_CLASS_UNDERDOG = "underdog";
            Assert.AreEqual(CSS_CLASS_UNDERDOG, _choice.Template.betTeamClassName);
        }

        /// <summary>
        /// The value of Template.betTeamClassName is underdog.
        /// </summary>
        [Test]
        public void BuildBetTeamClassNameAndHandicap_BetTeamNotIs1AndNotIs2AndHandicap2GreaterThanHandicap1_BetTeamAreDrawResourceAndhResourceAndHandicap2()
        {
            // Arrange
            _ticket.BetTeam = "bet team";
            _ticket.Handicap2 = 2;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            var expectedBetTeam = string.Format("{0}&nbsp;({1}+2)", CoreBetList.lblDraw, CoreBetList.h);
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        /// <summary>
        /// The value of Template.betTeamClassName is underdog.
        /// </summary>
        [Test]
        public void BuildBetTeamClassNameAndHandicap_BetTeamNotIs1AndNotIs2AndHandicap1GreaterThanHandicap2_BetTeamAreDrawResourceAndaResourceAndHandicap1()
        {
            // Arrange
            _ticket.BetTeam = "bet team";
            _ticket.Handicap1 = 2;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            var expectedBetTeam = string.Format("{0}&nbsp;({1}+2)", CoreBetList.lblDraw, CoreBetList.a);
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        /// <summary>
        /// The value of Template.Handicap.handicap is always null.
        /// </summary>
        [Test]
        public void BuildBetTeamClassNameAndHandicap_BetTeamIsNot1AndIsNot2_SetHandicapIsNull()
        {
            // Arrange
            _ticket.BetTeam = "bet team";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNull(_choice.Template.Handicap.handicap);
        }

        /// <summary>
        /// The value of Template.BetType.betTypeName is always bet type name.
        /// </summary>
        [Test]
        public void BuildBetTeam_Always_SetBetTypeNameIsBetTypeName()
        {
            // Arrange
            _ticket.BetTypeId = 1;
            _ticket.BetId = 1;
            _ticketHelper.GetBetTypeNameById(Arg.Any<int>(), Arg.Any<long>()).Returns("Bet type name");

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedBetTypeName = _ticketHelper.GetBetTypeNameById(_ticket.BetTypeId, _ticket.BetId);
            Assert.AreEqual(expectedBetTypeName, _choice.Template.BetType.betTypeName);
        }
    }
}