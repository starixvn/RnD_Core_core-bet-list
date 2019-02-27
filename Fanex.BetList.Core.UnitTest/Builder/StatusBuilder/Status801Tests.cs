namespace Fanex.BetList.Core.UnitTest.Builder.StatusBuilder
{
    using Common;
    using Common.Enums;
    using Core.Builder.StatusBuilder;
    using Entities;
    using Fanex.BetList.Core.App_GlobalResources;
    using NUnit.Framework;
    using Stubs;

    public class Status801Tests
    {
        private IStatus _status;
        private ITicket _ticket;
        private ITicketHelper _ticketHelperStub;

        [SetUp]
        public void Setup()
        {
            _status = new Status801();
            _ticket = TestData.CreateTicket(801);
            _ticketHelperStub = new TicketHelperStub();
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