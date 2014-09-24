namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Common.Enums;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Unit test for Choice1306: Match Total Game Over/Under.
    /// </summary>
    [TestFixture]
    public class Choice1306Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice1306();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.MatchTotalGamesOverUnder;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        /// <summary>
        /// If the value of ticket.BetTeam is 'o', the Template.betTeam will be 'Over'.
        /// </summary>
        [Test]
        public void BuildBetTeam_BetTeamIsCharacterO_BetTeamIsOver()
        {
            // Arrange
            _ticket.BetTeam = "o";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedBetTeam = "Over";
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        /// <summary>
        /// If the value of ticket.BetTeam is 'u', the Template.betTeam will be 'Under'.
        /// </summary>
        [Test]
        public void BuildBetTeam_BetTeamIsCharacterU_BetTeamIsUnder()
        {
            // Arrange
            _ticket.BetTeam = "u";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedBetTeam = "Under";
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }
    }
}