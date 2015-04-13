namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    ///  Unit test for Choice160.
    /// </summary>
    [TestFixture]
    public class Choice160Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice160();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        /// <summary>
        /// Builds the name of the bet team_ bet team is1_ template bet team is home team.
        /// </summary>
        [Test]
        public void BuildBetTeam_BetTeamIs1_TemplateBetTeamIsHomeTeamName()
        {
            // Arrange
            const string HOME_TEAM_NAME = "Home team name";
            _ticket.HomeId = 1365;
            _ticket.BetTeam = "1";
            _ticketHelper.GetTeamNameById(_ticket.HomeId).Returns<string>(HOME_TEAM_NAME);

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(HOME_TEAM_NAME, _choice.Template.betTeam);
        }

        /// <summary>
        /// Builds the name of the bet team_ bet team is2_ template bet team is away team.
        /// </summary>
        [Test]
        public void BuildBetTeam_BetTeamIs2_TemplateBetTeamIsAwayTeamName()
        {
            // Arrange
            const string AWAY_TEAM_NAME = "Away team name";
            _ticket.AwayId = 1365;
            _ticket.BetTeam = "2";
            _ticketHelper.GetTeamNameById(_ticket.AwayId).Returns<string>(AWAY_TEAM_NAME);

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(AWAY_TEAM_NAME, _choice.Template.betTeam);
        }

        [TestCase("x")]
        [TestCase("X")]
        public void BuildBetTeam_BetTeamIsX_TemplateBetTeamIsNone(string betTeam)
        {
            // Arrange
            _ticket.BetTeam = betTeam;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(CoreBetList.none, _choice.Template.betTeam);
        }

        [TestCase("abc")]
        [TestCase("")]
        public void BuildBetTeam_BetTeamIsInvalid_TemplateBetTeamIsEmpty(string betTeam)
        {
            // Arrange
            _ticket.BetTeam = betTeam;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsEmpty(_choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetHandicapIsNull()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNull(_choice.Template.Handicap.handicap);
        }

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
