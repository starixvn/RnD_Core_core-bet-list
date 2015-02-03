namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    ///  Unit test for Choice163.
    /// </summary>
    [TestFixture]
    public class Choice163Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice163();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        /// <summary>
        /// Builds the bet team_ bet team is h u_ template bet team is home team name slash under.
        /// </summary>
        /// <param name="betTeam">The bet team.</param>
        [TestCase("HU")]
        [TestCase("hu")]
        [TestCase("hU")]
        public void BuildBetTeam_BetTeamIsHU_TemplateBetTeamIsHomeTeamNameSlashUnder(string betTeam)
        {
            // Arrange
            const string HOME_TEAM_NAME = "Home team name";
            _ticket.HomeId = 1365;
            _ticket.BetTeam = betTeam;
            _ticketHelper.GetTeamNameById(_ticket.HomeId).Returns<string>(HOME_TEAM_NAME);

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            const string EXPECTED = "Home team name/Under";
            Assert.AreEqual(EXPECTED, _choice.Template.betTeam);
        }

        /// <summary>
        /// Builds the bet team_ bet team is h o_ template bet team is home team name slash over.
        /// </summary>
        /// <param name="betTeam">The bet team.</param>
        [TestCase("HO")]
        [TestCase("ho")]
        [TestCase("hO")]
        public void BuildBetTeam_BetTeamIsHO_TemplateBetTeamIsHomeTeamNameSlashOver(string betTeam)
        {
            // Arrange
            const string HOME_TEAM_NAME = "Home team name";
            _ticket.HomeId = 1365;
            _ticket.BetTeam = betTeam;
            _ticketHelper.GetTeamNameById(_ticket.HomeId).Returns<string>(HOME_TEAM_NAME);

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            const string EXPECTED = "Home team name/Over";
            Assert.AreEqual(EXPECTED, _choice.Template.betTeam);
        }

        /// <summary>
        /// Builds the bet team_ bet team is d u_ template bet team is draw slash under.
        /// </summary>
        /// <param name="betTeam">The bet team.</param>
        [TestCase("DU")]
        [TestCase("du")]
        [TestCase("dU")]
        public void BuildBetTeam_BetTeamIsDU_TemplateBetTeamIsDrawSlashUnder(string betTeam)
        {
            // Arrange
            _ticket.BetTeam = betTeam;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            const string EXPECTED = "Draw/Under";
            Assert.AreEqual(EXPECTED, _choice.Template.betTeam);
        }

        /// <summary>
        /// Builds the bet team_ bet team is d o_ template bet team is draw slash over.
        /// </summary>
        /// <param name="betTeam">The bet team.</param>
        [TestCase("DO")]
        [TestCase("do")]
        [TestCase("dO")]
        public void BuildBetTeam_BetTeamIsDO_TemplateBetTeamIsDrawSlashOver(string betTeam)
        {
            // Arrange
            _ticket.BetTeam = betTeam;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            const string EXPECTED = "Draw/Over";
            Assert.AreEqual(EXPECTED, _choice.Template.betTeam);
        }

        /// <summary>
        /// Builds the bet team_ bet team is a u_ template bet team is away team name slash under.
        /// </summary>
        /// <param name="betTeam">The bet team.</param>
        [TestCase("AU")]
        [TestCase("au")]
        [TestCase("aU")]
        public void BuildBetTeam_BetTeamIsAU_TemplateBetTeamIsAwayTeamNameSlashUnder(string betTeam)
        {
            // Arrange
            const string HOME_TEAM_NAME = "Away team name";
            _ticket.AwayId = 1365;
            _ticket.BetTeam = betTeam;
            _ticketHelper.GetTeamNameById(_ticket.AwayId).Returns<string>(HOME_TEAM_NAME);

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            const string EXPECTED = "Away team name/Under";
            Assert.AreEqual(EXPECTED, _choice.Template.betTeam);
        }

        /// <summary>
        /// Builds the bet team_ bet team is a o_ template bet team is away team name slash over.
        /// </summary>
        /// <param name="betTeam">The bet team.</param>
        [TestCase("AO")]
        [TestCase("ao")]
        [TestCase("aO")]
        public void BuildBetTeam_BetTeamIsAO_TemplateBetTeamIsAwayTeamNameSlashOver(string betTeam)
        {
            // Arrange
            const string HOME_TEAM_NAME = "Away team name";
            _ticket.AwayId = 1365;
            _ticket.BetTeam = betTeam;
            _ticketHelper.GetTeamNameById(_ticket.AwayId).Returns<string>(HOME_TEAM_NAME);

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            const string EXPECTED = "Away team name/Over";
            Assert.AreEqual(EXPECTED, _choice.Template.betTeam);
        }
    }
}
