namespace Fanex.BetList.Core.UnitTest.Builder.StatusBuilder
{
    using Core.Builder.StatusBuilder;
    using Entities;
    using Fanex.BetList.Core.App_GlobalResources;
    using NSubstitute;
    using NUnit.Framework;

    public class Status38Tests
    {
        private IStatus _status;
        private ITicket _ticket;
        private ITicketHelper _ticketHelperStub;

        [SetUp]
        public void Setup()
        {
            _status = new Status38();
            _ticket = new Ticket();
            _ticketHelperStub = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildResult_Always_DisableDetectAbnormalBet()
        {
            // Arrange
            _ticket.Status = BetStatus.Void;
            _ticket.ExtraInfo = "<p1 ab=\"1\" />";

            // Act
            _status.Render(_ticket, _ticketHelperStub, null, true);

            // Assert
            Assert.AreEqual(CoreBetList.voided, _status.Template.result);
        }
    }
}