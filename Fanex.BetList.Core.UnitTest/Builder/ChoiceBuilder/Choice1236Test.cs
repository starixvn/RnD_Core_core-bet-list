namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Common.Enums;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Unit test for Choice1236 class.
    /// </summary>
    [TestFixture]
    public class Choice1236Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice1236();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.VT_TotalPoints;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        /// <summary>
        /// The value of Template.betTeam is always [BetTeam Points] when bet team of ticket is 1.
        /// </summary>
        [Test]
        public void BuildBetTeam_Always_SetBetTeamAreBetTeamAndSpaceAndPointsResource()
        {
            // Arrange
            _ticket.BetTeam = "bet team";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedBetTeam = string.Format("{0} {1}", _ticket.BetTeam, CoreBetList.points);
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }
    }
}