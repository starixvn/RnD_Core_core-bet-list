namespace Fanex.BetList.SabaClub.Tests.Builder.StatusBuilder
{
    using System;
    using Fanex.BetList.Core.Builder.StatusBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Status1612Tests
    {
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private IStatus _status;

        [SetUp]
        public void Setup()
        {
            _status = new Status1612();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildStatusResult_BetCheckIsEmpty_HideResultLink()
        {
            // Arrange
            _ticket.WinlostDate = DateTime.Today.AddDays(-11);

            // Act
            _status.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(_status.Template.StatusResult.ToString()));
        }

        [Test]
        public void BuildStatusResult_MoreThan10Day_HideResultLink()
        {
            // Arrange
            _ticket.BetCheck = "betcheck";
            _ticket.WinlostDate = DateTime.Today.AddDays(-11);

            // Act
            _status.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(_status.Template.StatusResult.ToString()));
        }

        [Test]
        public void BuildStatusResult_TicketToday_ShowResultLink()
        {
            // Arrange
            _ticket.BetCheck = "betcheck";
            _ticket.WinlostDate = DateTime.Today;
            _ticket.Status = "Won";

            // Act
            _status.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(_status.Template.StatusResult.ToString()));
        }

        [Test]
        public void BuildStatusResult_SettleTicket_ShowResultLink()
        {
            // Arrange
            _ticket.BetCheck = "betcheck";
            _ticket.WinlostDate = DateTime.Today;
            _ticket.Status = "Won";

            // Act
            _status.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(_status.Template.StatusResult.ToString()));
        }

        [Test]
        public void BuildStatusResult_SettleRefund_ShowResultLink()
        {
            // Arrange
            _ticket.BetCheck = "betcheck";
            _ticket.WinlostDate = DateTime.Today;
            _ticket.Status = "Refund";

            // Act
            _status.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(_status.Template.StatusResult.ToString()));
        }

        [Test]
        public void BuildStatusResult_SettleReject_HideResultLink()
        {
            // Arrange
            _ticket.BetCheck = "betcheck";
            _ticket.WinlostDate = DateTime.Today;
            _ticket.Status = "Reject";

            // Act
            _status.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(_status.Template.StatusResult.ToString()));
        }
    }
}
