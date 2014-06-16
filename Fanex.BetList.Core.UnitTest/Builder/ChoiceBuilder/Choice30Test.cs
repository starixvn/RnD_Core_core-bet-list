namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.UnitTest.Common.Enums;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Unit test for Choice30 class.
    /// </summary>
    [TestFixture]
    public class Choice30Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice30();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.FirstCorrectScore;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }
        
        /// <summary>
        /// The value of Template.betTeam is always bet team.
        /// </summary>
        [Test]
        public void BuildChoice_BetTeamOfTicketIs04_BetTeamIsBetTeamAndUp()
        {
            // Arrange
            _ticket.BetTeam = "0:4";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedBetTeam = string.Format("&nbsp;{0} UP", _ticket.BetTeam);
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        /// <summary>
        /// The value of Template.betTeam is always bet team.
        /// </summary>
        [Test]
        public void BuildChoice_BetTeamOfTicketIs40_BetTeamIsBetTeamAndUp()
        {
            // Arrange
            _ticket.BetTeam = "4:0";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedBetTeam = string.Format("&nbsp;{0} UP", _ticket.BetTeam);
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        /// <summary>
        /// The value of Template.betTeam is always bet team.
        /// </summary>
        [Test]
        public void BuildChoice_HomeIdOfTicketNotZero_BetTeamAreSpaceAndBetTeam()
        {
            // Arrange
            _ticket.BetTeam = "bet team";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedBetTeam = string.Format("&nbsp;{0}", _ticket.BetTeam);
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }
    }
}