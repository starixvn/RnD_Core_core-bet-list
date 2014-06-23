namespace Fanex.BetList.Core.UnitTest.Builder.StatusBuilder
{
    using Common;
    using Common.Enums;
    using Core.Builder.StatusBuilder;
    using Core.Templates;
    using Entities;
    using NUnit.Framework;
    using Stubs;

    public class Status1000Test
    {
        private Status1000 _status;
        private ITicket _ticket;
        private ITicketHelper _ticketHelperStub;

        [SetUp]
        public void Setup()
        {
            _status = new Status1000();
            _ticket = TestData.CreateTicket(BetTypes.P2P);
            _ticketHelperStub = new TicketHelperStub();
        }

        [Test]
        public void Ticket_RefNoOfTemplateStatusResultIsTicketBetCheck()
        {
            // Act
            Status_Template template = _status.Render(_ticket, _ticketHelperStub, null, false);

            // Assert
            string expectedValue = _ticket.BetCheck;
            Assert.AreEqual(expectedValue, template.StatusResult.refNo);
        }
    }
}