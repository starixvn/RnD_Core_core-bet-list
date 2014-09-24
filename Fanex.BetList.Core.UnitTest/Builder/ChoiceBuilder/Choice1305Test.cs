namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Common.Enums;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Unit test for Choice1305 class.
    /// </summary>
    [TestFixture]
    public class Choice1305Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice1305();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.MatchTotalGamesOddEven;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        /// <summary>
        /// If the value of ticket.BetTeam is 'o', the Template.betTeam will be 'Odd'.
        /// </summary>
        [Test]
        public void BuildBetTeam_BetTeamIsCharacterO_BetTeamIsOdd()
        {
            // Arrange
            _ticket.BetTeam = "o";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedBetTeam = "Odd";
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        /// <summary>
        /// If the value of ticket.BetTeam is 'e', the Template.betTeam will be 'Even'.
        /// </summary>
        [Test]
        public void BuildBetTeam_BetTeamIsCharacterE_BetTeamIsEven()
        {
            // Arrange
            _ticket.BetTeam = "e";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedBetTeam = "Even";
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }
    }
}