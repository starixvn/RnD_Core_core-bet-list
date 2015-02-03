namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    ///  Unit test for Choice178.
    /// </summary>
    [TestFixture]
    public class Choice178Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice178();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        /// <summary>
        /// Builds the bet team_ bet team_ template bet team.
        /// </summary>
        /// <param name="betTeam">The bet team.</param>
        /// <param name="expected">The expected.</param>
        [TestCase("O", "Over")]
        [TestCase("U", "Under")]
        [TestCase("o", "Over")]
        [TestCase("u", "Under")]
        [TestCase("abc", "Under")]
        public void BuildBetTeam_BetTeam_TemplateBetTeam(string betTeam, string expected)
        {
            // Arrange
            _ticket.BetTeam = betTeam;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(expected, _choice.Template.betTeam);
        }

        /// <summary>
        /// Builds the bet team class name and handicap_ bet team is o_ bet team class name is favorite.
        /// </summary>
        [Test]
        public void BuildBetTeamClassNameAndHandicap_BetTeamIsO_BetTeamClassNameIsFavorite()
        {
            // Arrange
            _ticket.BetTeam = "O";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            const string CSS_BETTEAM_CLASS = "favorite";
            Assert.AreEqual(CSS_BETTEAM_CLASS, _choice.Template.betTeamClassName);
        }

        /// <summary>
        /// Builds the bet team class name and handicap_ bet team is u_ bet team class name is underdog.
        /// </summary>
        [Test]
        public void BuildBetTeamClassNameAndHandicap_BetTeamIsU_BetTeamClassNameIsUnderdog()
        {
            // Arrange
            _ticket.BetTeam = "U";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            const string CSS_BETTEAM_CLASS = "underdog";
            Assert.AreEqual(CSS_BETTEAM_CLASS, _choice.Template.betTeamClassName);
        }
    }
}
