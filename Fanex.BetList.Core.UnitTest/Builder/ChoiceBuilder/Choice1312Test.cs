namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Common.Enums;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Unit test for Choice1312 class.
    /// </summary>
    [TestFixture]
    public class Choice1312Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice1312();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.SetXTotalGames2Way;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        /// <summary>
        /// The value of Template.BetType.betTypeName is always bet type name.
        /// </summary>
        [Test]
        public void BuildBetTeam_Always_SetBetTypeNameIsBetTypeName()
        {
            // Arrange
            _ticket.BetId = 1;
            _ticketHelper.GetBetTypeNameById(Arg.Any<int>(), Arg.Any<long>()).Returns("Bet type name");

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedBetTypeName = _ticketHelper.GetBetTypeNameById(_ticket.BetTypeId, _ticket.BetId);
            Assert.AreEqual(expectedBetTypeName, _choice.Template.BetType.betTypeName);
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