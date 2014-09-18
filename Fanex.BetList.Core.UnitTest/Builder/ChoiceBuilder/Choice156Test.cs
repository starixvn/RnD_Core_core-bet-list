namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.UnitTest.Common.Enums;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Unit test for Choice156: Set X Total Game Over/Under.
    /// </summary>
    [TestFixture]
    public class Choice156Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice156();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.Set_X_Total_Game_OU;
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