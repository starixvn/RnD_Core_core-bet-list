namespace Fanex.BetList.VirtualSports.Tests.Builder.ChoiceBuilder
{
    using Core.App_GlobalResources;
    using Core.Builder.ChoiceBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice1236Tests
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice1236();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildBetTeam_Always_SetBetTeamAreBetTeamAndSpaceAndPointsResource()
        {
            // Arrange
            _ticket.BetTeam = "bet team";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            string expectedBetTeam = string.Format("{0} {1}", _ticket.BetTeam, CoreBetList.points);
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }
    }
}