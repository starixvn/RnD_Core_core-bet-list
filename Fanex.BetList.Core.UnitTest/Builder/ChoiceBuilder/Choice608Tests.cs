namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Common;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice608Tests
    {
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private IChoice _choice;

        [SetUp]
        public void Setup()
        {
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticket = TestData.CreateTicket(608);
            _choice = new Choice608();
        }

        [Test]
        public void BuildBetTeam_BetTeamIsDraw_ShowBetTeamIsDraw()
        {
            // Arrange
            _ticket.BetTeam = "d";
            _ticketHelper.GetResourceData("WMBasketball", "D").Returns("Draw");

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual("Draw", _choice.Template.betTeam);
        }
    }
}