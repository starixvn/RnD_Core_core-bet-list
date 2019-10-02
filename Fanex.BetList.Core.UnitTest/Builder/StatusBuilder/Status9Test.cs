namespace Fanex.BetList.Core.UnitTest.Builder.StatusBuilder
{
    using Common;
    using Core.App_GlobalResources;
    using Core.App_GlobalResources;
    using Core.Builder.StatusBuilder;
    using Core.Constants;
    using Core.Templates;
    using Entities;
    using NUnit.Framework;
    using Stubs;

    public class Status9Test
    {
        private Status9 _status;
        private ITicket _ticket;
        private ITicketHelper _ticketHelperStub;

        [SetUp]
        public void Setup()
        {
            _status = new Status9();
            _ticket = TestData.CreateTicket(BetTypes.MixParlay);
            _ticketHelperStub = new TicketHelperStub();
        }

        [Test]
        public void Ticket_RefNoMixParlayOfStatusResultTemplateIsTicketRefNo()
        {
            // Act
            Status_Template template = _status.Render(_ticket, _ticketHelperStub, null, false);

            // Assert
            string expectedValue = _ticket.RefNo;
            Assert.AreEqual(expectedValue, template.StatusResult.refNo_MixParlay);
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