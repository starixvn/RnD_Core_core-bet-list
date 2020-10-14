namespace Fanex.BetList.Core.UnitTest.Builder.StatusBuilder
{
    using Core.Builder.StatusBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Status228Test
    {
        private IStatus _status;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _status = new Status228();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildStatusResult_StatusIsRefund_Hide()
        {
            // Arrange
            _ticket.Status = "Refund";

            // Act
            _status.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsFalse(_status.Template.StatusResult.Visible);
        }

        [Test]
        public void BuildStatusResult_StatusIdIsRefund_Hide()
        {
            // Arrange
            _ticket.StatusId = 103;

            // Act
            _status.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsFalse(_status.Template.StatusResult.Visible);
        }
    }
}